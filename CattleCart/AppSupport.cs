using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
namespace CattleCart
{
    public static class Session
    {
        public static string Name { get; set; }
        public static string Contact { get; set; }
        public static string Role { get; set; }
        public static int CattleId { get; set; }
        public static int OrderId { get; set; }
        public static void Clear() { Name = Contact = Role = null; CattleId = OrderId = 0; }
    }
    public class AppForm : Form
    {
        protected override void OnFormClosed(FormClosedEventArgs e)
        { base.OnFormClosed(e); if (Application.OpenForms.Count == 0) Application.ExitThread(); }
    }
    public static class AppSupport
    {
        public static bool IsPhone(string value)
        { return value != null && value.Length == 11 && value.StartsWith("01") && value.All(c => c >= '0' && c <= '9'); }
        public static bool IsName(string value)
        { return !string.IsNullOrWhiteSpace(value) && value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '.' || c == '-' || c == '\''); }
        public static Image DecodeImage(object value)
        {
            if (value == null || value == DBNull.Value) return null;
            try
            {
                var bytes = value as byte[];
                if (bytes != null)
                {
                    if (bytes.Length == 0) return null;
                    using (var ms = new MemoryStream(bytes)) using (var img = Image.FromStream(ms)) return new Bitmap(img);
                }
                string path = Convert.ToString(value);
                if (File.Exists(path)) using (var img = Image.FromFile(path)) return new Bitmap(img);
            }
            catch (ArgumentException) { }
            catch (IOException) { }
            return null;
        }
        public static void ConfigureGrid(DataGridView grid)
        {
            grid.ReadOnly = true; grid.AllowUserToAddRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect; grid.MultiSelect = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (grid.Columns.Contains("Password")) grid.Columns["Password"].Visible = false;
            if (grid.Columns.Contains("ImageURL")) grid.Columns["ImageURL"].Visible = false;
        }
        public static void ShowSelected(Form current, DataGridView grid, bool buy = false)
        {
            if (grid.CurrentRow == null || grid.CurrentRow.IsNewRow) { MessageBox.Show("Please select an animal first."); return; }
            int id = Convert.ToInt32(grid.CurrentRow.Cells["CattleID"].Value);
            Session.CattleId = id;
            if (buy) new Form27().Show(); else new ViewForm { CattleId = id }.Show();
            current.Close();
        }
        public static DataTable Orders(bool farmerOnly)
        {
            using (var con = Class1.GetConnection())
            using (var da = new SqlDataAdapter("SELECT OrderID,CattleID,Breed,AnimalPrice,DeliveryCharge,CustomerContact,FarmerContact,PaymentMethod,Status,CreatedAt FROM Orders" +
                (farmerOnly ? " WHERE FarmerContact=@contact" : "") + " ORDER BY OrderID DESC", con))
            { if (farmerOnly) da.SelectCommand.Parameters.AddWithValue("@contact", Session.Contact ?? ""); var dt = new DataTable(); da.Fill(dt); return dt; }
        }
    }
    public static class Passwords
    {
        public static string Hash(string password)
        {
            byte[] salt = new byte[16]; using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            using (var derive = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
                return "PBKDF2$100000$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(derive.GetBytes(32));
        }
        public static bool Verify(string password, string stored)
        {
            if (!stored.StartsWith("PBKDF2$")) return password == stored; // Existing project databases.
            try
            {
                var parts = stored.Split('$');
                using (var derive = new Rfc2898DeriveBytes(password, Convert.FromBase64String(parts[2]), int.Parse(parts[1]), HashAlgorithmName.SHA256))
                {
                    var actual = derive.GetBytes(32); var expected = Convert.FromBase64String(parts[3]);
                    if (expected.Length != actual.Length) return false;
                    int diff = 0; for (int i = 0; i < actual.Length; i++) diff |= actual[i] ^ expected[i]; return diff == 0;
                }
            }
            catch (FormatException) { return false; }
            catch (IndexOutOfRangeException) { return false; }
        }
    }
    public static class Checkout
    {
        public static int PlaceDemoOrder(string method)
        {
            if (Session.Role != "Customer" || !AppSupport.IsPhone(Session.Contact) || Session.CattleId <= 0)
                throw new InvalidOperationException("Please log in as a customer and select an animal first.");
            using (var con = Class1.GetConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    using (var cmd = new SqlCommand(@"INSERT INTO Orders(CattleID,CustomerContact,FarmerContact,Breed,AnimalPrice,DeliveryCharge,PaymentMethod,Status)
SELECT CattleID,@contact,FarmerContact,Breed,Price,5000,@method,'Demo confirmed'
FROM Cattle WITH (UPDLOCK,HOLDLOCK) WHERE CattleID=@id AND IsSold=0;
IF @@ROWCOUNT=0 THROW 50001,'This animal is no longer available.',1;
SELECT CAST(SCOPE_IDENTITY() AS int);", con, tx))
                    {
                        cmd.Parameters.AddWithValue("@contact", Session.Contact);
                        cmd.Parameters.AddWithValue("@method", method);
                        cmd.Parameters.AddWithValue("@id", Session.CattleId);
                        int id = (int)cmd.ExecuteScalar();
                        using (var update = new SqlCommand("UPDATE Cattle SET IsSold=1 WHERE CattleID=@id", con, tx))
                        { update.Parameters.AddWithValue("@id", Session.CattleId); update.ExecuteNonQuery(); }
                        tx.Commit(); Session.OrderId = id; return id;
                    }
                }
            }
        }
        public static void Complete(Form current, string method)
        {
            try { PlaceDemoOrder(method); new Form31().Show(); current.Close(); }
            catch (Exception ex) { MessageBox.Show("Could not save order: " + ex.Message); }
        }
    }
}
