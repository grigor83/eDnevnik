using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvidencijaIzostanaka
{
    internal static class Program
    {
        public static readonly string APP_NAME = "e-Dnevnik";
        public static readonly List<string> SCHOOL_YEARS = new List<string> { "2020/21", "2021/22", "2022/23", "2023/24" };
        public static readonly List<int> CLASSES = new List<int> { 1,2,3,4 };
        public static readonly List<string> DEPARTMENTS = new List<string> { "a", "b", "c", "d" };
        public static readonly List<string> GRADES = new List<string> { "1", "2", "3", "4", "5" };
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainForm = new MainForm();
            mainForm.Text = APP_NAME;
            Application.Run(mainForm);
        }
    }
}
