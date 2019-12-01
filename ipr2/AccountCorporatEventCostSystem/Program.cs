using System;
using System.Windows.Forms;

namespace AccountCorporatEventCostSystem
{
    static class Program
    {
        private readonly static string DbPath = "C:\\Users\\KirilL\\Work\\Projects\\VS\\C#\\Margarita\\ipr2\\AccountCorporatEventCostSystem\\db.mdb";
        public readonly static string ConnectionString = $"Driver={{Microsoft Access Driver (*.mdb)}};Dbq={DbPath};";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
