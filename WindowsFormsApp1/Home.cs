using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        string dbPath = "C:\\AnnalandBD\\ALDB.db";
        string path = "C:\\AnnalandBD";

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Delete Del = new Delete();
            Del.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {
           
            if (!Directory.Exists(path)) { DirectoryInfo di = Directory.CreateDirectory(path); }
            if (!File.Exists(dbPath))
            {
                try { 
                FtpWebRequest request =
                                         (FtpWebRequest)WebRequest.Create("ftp://sa246943.ftp.tools/ALDB.db");
                request.Credentials = new NetworkCredential("sa246943_alws", "mNC6Eix648hD");
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                    using (Stream ftpStream = response.GetResponseStream())
                using (Stream fileStream = File.Create(@"C:\AnnalandBD\ALDB.db"))
                {
                    ftpStream.CopyTo(fileStream);
                }
            }
                catch (WebException ex) {
                    String status = ((FtpWebResponse)ex.Response).StatusDescription;
                    MessageBox.Show(status);
                    Application.Exit();
                 
                }

            }
            
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            string path1 = @"C:\\AnnalandBD\\ALDB.db";
            string path2 = @"C:\\AnnalandBD\\oldDB\\ALDB.db";
           
            try
            {
                if (File.Exists(path2))
                {
                    File.Delete(path2);
                    File.Move(path1, path2);
                }
                else {
                    File.Move(path1, path2);
                }
                    

            }
            catch (IOException er)
            {
                MessageBox.Show(er.Message.ToString());
            }
        }
    }
}
