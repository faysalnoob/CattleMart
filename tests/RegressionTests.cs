using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CattleCart;
class RegressionTests
{
    static int checks;
    static void Assert(bool ok,string label) { if(!ok) throw new Exception(label); Console.WriteLine("PASS: " + label); checks++; }
    static object Scalar(string sql)
    { using(var c=Class1.GetConnection()) using(var cmd=new SqlCommand(sql,c)) { c.Open(); return cmd.ExecuteScalar(); } }
    static void Exec(string sql) { Scalar(sql); }
    [STAThread] static int Main()
    {
        string db="CattleCart_Test_"+Guid.NewGuid().ToString("N");
        Environment.SetEnvironmentVariable("CATTLECART_CONNECTION_STRING",@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog="+db+";Integrated Security=True;Connect Timeout=15");
        try
        {
            Class1.InitializeDatabase(); Class1.InitializeDatabase();
            Assert(Convert.ToInt32(Scalar("SELECT COUNT(*) FROM sys.tables WHERE name IN ('Admin','Farmers','Customers','Cattle','Orders','Reports')"))==6,"fresh database schema and repeated setup");
            string hash=Passwords.Hash("Test123!");
            Assert(Passwords.Verify("Test123!",hash)&&!Passwords.Verify("wrong",hash)&&hash!=Passwords.Hash("Test123!"),"salted password hash and wrong-password rejection");
            Assert(Passwords.Verify("legacy","legacy")&&!Passwords.Verify("wrong","legacy"),"existing database password compatibility");
            Assert(AppSupport.IsPhone("01712345678")&&!AppSupport.IsPhone("01")&&!AppSupport.IsPhone("0171234567x"),"phone validation");
            Assert(AppSupport.IsName("Md. Rahim Uddin")&&!AppSupport.IsName("123"),"names with spaces");
            Assert(AppSupport.DecodeImage(DBNull.Value)==null&&AppSupport.DecodeImage(new byte[0])==null&&AppSupport.DecodeImage(new byte[]{1,2,3})==null,"missing and invalid images");
            byte[] bytes; using(var image=new Bitmap(8,8)) using(var ms=new MemoryStream()) { image.Save(ms,System.Drawing.Imaging.ImageFormat.Png); bytes=ms.ToArray(); }
            using(var image=AppSupport.DecodeImage(bytes)) Assert(image!=null&&image.Width==8,"binary image survives disposed source stream");
            Exec("INSERT INTO Farmers(Name,Email,Contact,Address,Password) VALUES ('Farmer','f@example.com','01712345678','Dhaka','test'); INSERT INTO Customers(Name,Email,Contact,Address,Password) VALUES ('Customer','c@example.com','01812345678','Dhaka','test'); INSERT INTO Cattle(Breed,Weight,Age,Price,TypeAnimal,FarmerContact) VALUES ('First',100,2,60000,'Cow','01712345678'),('Selected',120,3,70000,'Cow','01712345678'),('Goat',20,1,10000,'Goat','01912345678');");
            bool duplicate=false; try { Exec("INSERT INTO Customers(Name,Email,Contact,Address,Password) VALUES ('Duplicate','d@example.com','01812345678','Dhaka','test')"); } catch(SqlException) { duplicate=true; }
            Assert(duplicate,"duplicate account rejected");
            using(var con=Class1.GetConnection()) using(var cmd=new SqlCommand("UPDATE Cattle SET ImageURL=@image WHERE CattleID=2",con)) { cmd.Parameters.Add("@image",SqlDbType.VarBinary,-1).Value=bytes; con.Open();cmd.ExecuteNonQuery(); }
            Session.Role="Customer";Session.Contact="01812345678";Session.Name="Customer";Session.CattleId=2;
            using(var view=new ViewForm {CattleId=2})
            {
                typeof(ViewForm).GetMethod("ViewForm_Load",BindingFlags.NonPublic|BindingFlags.Instance).Invoke(view,new object[]{view,EventArgs.Empty});
                var breed=(TextBox)typeof(ViewForm).GetField("textBreed",BindingFlags.NonPublic|BindingFlags.Instance).GetValue(view);
                Assert(breed.Text=="Selected"&&breed.ReadOnly,"detail screen displays chosen ID rather than first animal");
            }
            int id=Checkout.PlaceDemoOrder("bKash (demo)");
            Assert(id>0&&Convert.ToInt32(Scalar("SELECT COUNT(*) FROM Orders WHERE CattleID=2 AND AnimalPrice=70000 AND DeliveryCharge=5000 AND CustomerContact='01812345678'"))==1,"checkout persists correct animal, customer and amounts");
            Assert(Convert.ToBoolean(Scalar("SELECT IsSold FROM Cattle WHERE CattleID=2")),"ordered animal removed from availability");
            bool sold=false;try { Checkout.PlaceDemoOrder("Nagad (demo)"); } catch(SqlException) { sold=true; }
            Assert(sold&&Convert.ToInt32(Scalar("SELECT COUNT(*) FROM Orders"))==1,"duplicate checkout rolls back without duplicate order");
            Session.Role="Farmer";Session.Contact="01712345678";Session.Name="Farmer";
            Assert(AppSupport.Orders(true).Rows.Count==1,"farmer order list ownership filter");
            Session.Contact="01912345678";Assert(AppSupport.Orders(true).Rows.Count==0,"other farmer cannot see order");
            Assert(AppSupport.Orders(false).Rows.Count==1,"admin order list");
            Session.Contact="01712345678";
            using(var f=new Form6()) using(var orders=new Form8()) using(var admin=new Form18()) using(var farmers=new Form19()) using(var all=new Form20()) using(var adminOrders=new Form21()) using(var reports=new Form22()) using(var cows=new Form11()) using(var goats=new Form12()) using(var sheep=new Form13()) using(var camels=new Form14())
            {
                Assert(true,"all database-backed list screens construct without exceptions");
                var cowGrid=(DataGridView)typeof(Form11).GetField("dataGridView1",BindingFlags.NonPublic|BindingFlags.Instance).GetValue(cows);
                Assert(cowGrid.Rows.Count==1 && Convert.ToInt32(cowGrid.Rows[0].Cells["CattleID"].Value)==1,"category excludes sold animals and other animal types");
            }
            using(var success=new Form31())
            {
                var button=(Button)typeof(Form31).GetField("button1",BindingFlags.NonPublic|BindingFlags.Instance).GetValue(success);
                Assert(success.ClientRectangle.Contains(button.Bounds),"confirmation button stays inside window");
            }
            Exec("INSERT INTO Reports(ReportText) VALUES (N'Test report')"); Assert(Convert.ToInt32(Scalar("SELECT COUNT(*) FROM Reports"))==1,"reports persist");
            Session.Clear(); bool unauth=false;try { Checkout.PlaceDemoOrder("Card (demo)"); } catch(InvalidOperationException) { unauth=true; }
            Assert(unauth,"checkout rejects missing customer session");
            Console.WriteLine("PASSED "+checks+" regression checks.");return 0;
        }
        catch(Exception ex) { Console.Error.WriteLine(ex);return 1; }
        finally
        {
            SqlConnection.ClearAllPools();
            using(var c=new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True"))
            using(var cmd=new SqlCommand("ALTER DATABASE ["+db+"] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE ["+db+"]",c)) { c.Open();cmd.ExecuteNonQuery(); }
        }
    }
}
