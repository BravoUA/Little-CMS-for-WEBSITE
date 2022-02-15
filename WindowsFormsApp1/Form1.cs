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
using System.Data.SQLite;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string puth;


         public string Login;
         public string Paswword;
         public string Host;

        string HadImg;
        string HadImgPath;

        string Name, Model, Year, Type, Working_hours, Power, Mass, Text, State;
        int Price, FildType, IDM, Sale, id;

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0) {
                label5.Visible = true; label6.Visible = true; label7.Visible = true; label8.Visible = true; label9.Visible = true; label10.Visible = true; label11.Visible = true; label12.Visible = true; label13.Visible = true; label14.Visible = true; label15.Visible = true;
                textBox1.Visible = true; textBox2.Visible = true; textBox3.Visible = true; textBox4.Visible = true; textBox5.Visible = true; textBox6.Visible = true; textBox7.Visible = true; textBox8.Visible = true; textBox10.Visible = true;
                comboBox1.Visible = true;comboBox2.Visible = true;
            } else if (comboBox3.SelectedIndex == 1) {
                label5.Visible = true; label6.Visible = true; label7.Visible = true; label8.Visible = true; label9.Visible = false; label10.Visible = false; label11.Visible = true; label12.Visible = true; label13.Visible = true; label14.Visible = true; label15.Visible = true;
                textBox1.Visible = true; textBox2.Visible = true; textBox3.Visible = true; textBox4.Visible = true; textBox5.Visible = false; textBox6.Visible = false; textBox7.Visible = true; textBox8.Visible = true; textBox10.Visible = true;
                comboBox1.Visible = true; comboBox2.Visible = true;
            }
        }

        string[] ImgName;
        string[] ImgNamePaths;
        ImageList img = new ImageList();

        static string dbPath = "C:\\AnnalandBD\\ALDB.db";
        ftpConnect ftpConnect;
        dbConnect dbConnect = new dbConnect(dbPath);

        private void Form1_Load(object sender, EventArgs e)
        {
            ftpConnect = new ftpConnect(Login, Paswword, Host);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            listView1.View = View.Details;
            listView1.Columns.Add("CONTENT IMGES",150);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            this.progressBar1.Maximum = 100;
   
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> content = new List<string>();
            if (comboBox3.SelectedIndex == 0) {
                content.Add(textBox1.Text);     //Nmae
                content.Add(textBox2.Text);     //Model
                content.Add(textBox3.Text);     //Year
                content.Add(textBox4.Text);     //Type
                content.Add(textBox5.Text);     //Working_hours
                content.Add(textBox6.Text);     //Power
                content.Add(textBox7.Text);     //Mass
                content.Add(textBox8.Text);     //Text
                content.Add(comboBox2.Text);    //State
                content.Add(textBox10.Text);    //Price
                content.Add((comboBox1.SelectedIndex + 1).ToString());  //FildType

                List<string> IMGSPath = new List<string>();
                string[] webimgpath = {"/img/HadImg/" + HadImg ,  HadImg  };
                    for (int i = 0; i < ImgName.Length; i++)
                    {
                        IMGSPath.Add("/img/imges/" + ImgName[i]);
                    }
                dbConnect.CreateNew(1, content,webimgpath,IMGSPath, ImgName);

                string[] webimgpath2 = { HadImgPath, HadImg };

              //  ftpConnect.UploadFilesIMG(webimgpath2, ImgNamePaths, ImgName);

                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = ""; comboBox1.Text = ""; comboBox2.Text = ""; textBox10.Text = ""; listView1.Clear();
                MessageBox.Show("upload is complete");
                this.Close();
            } else if (comboBox3.SelectedIndex == 1) {

                content.Add(textBox1.Text);     //Nmae
                content.Add(textBox2.Text);     //Model
                content.Add(textBox3.Text);     //Year
                content.Add(textBox4.Text);     //Type
                content.Add(textBox7.Text);     //Mass
                content.Add(textBox8.Text);     //Text
                content.Add(comboBox2.Text);    //State
                content.Add(textBox10.Text);    //Price
                content.Add((comboBox1.SelectedIndex + 1).ToString());  //FildType

                List<string> IMGSPath = new List<string>();
                string[] webimgpath = { "/img/HadImg/" + HadImg, HadImg };
                for (int i = 0; i < ImgName.Length; i++)
                {
                    IMGSPath.Add("/img/imges/" + ImgName[i]);
                }
                dbConnect.CreateNew(2, content, webimgpath, IMGSPath, ImgName);

                string[] webimgpath2 = { HadImgPath, HadImg };
                ftpConnect.UploadFilesIMG(webimgpath2, ImgNamePaths, ImgName);

                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = ""; comboBox1.Text = ""; comboBox2.Text = ""; textBox10.Text = ""; listView1.Clear();
                progressBar1.Value = 100;

                MessageBox.Show("upload is complete");
                this.Close();
            }
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("IMG Resolution mast be 330x270");
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;

            choofdlog.Multiselect = true;
            
            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                HadImgPath = choofdlog.FileName;
                HadImg = choofdlog.SafeFileName;
                label2.Text = HadImg;
                pictureBox1.Image = Image.FromFile(HadImgPath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            }


        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;

            
            img.ImageSize = new Size(100,80);

            choofdlog.Multiselect = true;
      
            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                ImgNamePaths = choofdlog.FileNames;
                ImgName = choofdlog.SafeFileNames;
                foreach (string i in ImgNamePaths) 
                { 
                    img.Images.Add(Image.FromFile(i));
                }
                listView1.SmallImageList = img;
                for (int a=0;a< ImgNamePaths.Length;a++) {
                   
                    listView1.Items.Add(ImgNamePaths[a], a);
                }
            }
        }
    }
}
