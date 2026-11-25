using System;
using System.Windows.Forms;
namespace CattleCart
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false);
            try { Class1.InitializeDatabase(); }
            catch (Exception ex)
            {
                MessageBox.Show("Database setup failed. Install SQL Server Express LocalDB or edit CattleCart.exe.config to use your SQL Server.\n\n" + ex.Message,
                    "CattleCart setup", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            new intropage().Show(); Application.Run();
        }
    }
}
