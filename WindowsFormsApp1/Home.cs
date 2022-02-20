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
using WindowsFormsApp1;

namespace WindowsFormsApp1
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        string login;
        string paswword;
        string host;

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Login = login;
            form.Paswword = paswword;
            form.Host = host;
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Delete Del = new Delete();
            Del.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            string path1 = @"C:\\AnnalandBD\\ALDB.db";
            try
            {
                if (File.Exists(path1))
                {
                    File.Delete(path1);
                }
            }
            catch (IOException er)
            {
                MessageBox.Show(er.Message.ToString());
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            string path1 = @"C:\AnnalandBD\ALDB.db";
            string path2 = @"C:\AnnalandBD\oldDB\ALDB.db";
            
            try
            {
                if (File.Exists(path2))
                {
                    File.Delete(path2);
                    File.Move(path1, path2);
                }
                else if(File.Exists(path1)) {
                    
                    File.Move(path1, path2);
                }
            }
            catch (IOException er) { 
            
                MessageBox.Show(er.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            login = Login.Text;
            paswword = Paswword.Text;
            host = "ftp://"+ Host.Text+"//";
            ftpConnect ConectFTP = new ftpConnect(login, paswword, host);
            ConectFTP.LoadDB();
            if (ConectFTP.stateOfConection)
            {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                Login.Visible = false;
                Paswword.Visible = false;
                Host.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                button4.Visible = true;
            }
           


        }

        private void button4_Click(object sender, EventArgs e)
        {
            ftpConnect ConectFTP = new ftpConnect(login, paswword, host);
            ConectFTP.UploadFileDB();
        }
    }
}
