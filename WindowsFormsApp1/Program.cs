using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]


        static void Main()
        {
            FtpWebRequest request;
            FtpWebResponse response;
            Stream reqStream;
            string assemblyVersion = "";
            string pathAssemblyFile = "https://www.annaland.com.ua/upUpdate/WindowsFormsApp1.exe";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string Loggin = "sa246943_alws";
            string Paswword = "mNC6Eix648hD";
            string ftpString = "ftp://sa246943.ftp.tools/";
             
            using (var wc = new WebClient())
            {
                assemblyVersion = Assembly.Load(wc.DownloadData(pathAssemblyFile)).GetName().Version.ToString();
            }
            string curenAssembly = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (curenAssembly != assemblyVersion)
            {
                DialogResult result = MessageBox.Show(
                        "Версія програми = " + curenAssembly + "\nЄ оновлена версія = " + assemblyVersion + "\n\nЧи бажаєте оновити",
                        "",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                switch (result)
                {
                    case DialogResult.Yes:
                        string strExeFilePath = Assembly.GetExecutingAssembly().Location;
                        string strWorkPath = Path.GetDirectoryName(strExeFilePath);
                        Process.Start(strWorkPath+ "\\UpdatePR.exe");
                        Application.Exit();
                        break;

                    case DialogResult.No:
                        Application.Run(new Home());
                        break;

                    case DialogResult.Cancel:
                        Application.Exit();
                        break;
                }
            }
            else {
                Application.Run(new Home());
            }

        }
    }
}
