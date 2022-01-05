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

        SQLiteConnection ObjConnection = new SQLiteConnection("Data Source=ALDB.db;");
        public SQLiteCommand ObjCommand;
        public SQLiteDataAdapter ObjDataAdapter;
        DataSet dataSet = new DataSet();


        string HadImg;
        string HadImgPath;

        string Name, Model, Year, Type, Working_hours, Power, Mass, Text, State;
        int Price, FildType, IDM, Sale, id;

        string[] ImgName;
        string[] ImgNamePaths;
        ImageList img = new ImageList();








        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.Columns.Add("CONTENT IMGES",150);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            this.progressBar1.Maximum = 100;

            /*  FtpWebRequest request =(FtpWebRequest)WebRequest.Create("ftp://sa246943_ftp@sa246943.ftp.tools:21/annaland.test/www/ALDB.db");
               request.Credentials = new NetworkCredential("sa246943_ftp", "Y35rcK1Z5h");
               request.Method = WebRequestMethods.Ftp.DownloadFile;

               using (Stream ftpStream = request.GetResponse().GetResponseStream())
               using (Stream fileStream = File.Create(@"C:\DOOOOOO\ALDB.db"))
               {
                   ftpStream.CopyTo(fileStream);
               }*/

        }

        private FtpWebRequest CreateFtpWebRequest(string ftpDirectoryPath, string userName, string password, bool keepAlive = false)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpDirectoryPath));

            //Set proxy to null. Under current configuration if this option is not set then the proxy that is used will get an html response from the web content gateway (firewall monitoring system)
            request.Proxy = null;

            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = keepAlive;

            request.Credentials = new NetworkCredential(userName, password);

            return request;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Name = textBox1.Text;Model = textBox2.Text;Year = textBox3.Text;Type = textBox4.Text;Working_hours = textBox5.Text;Power = textBox6.Text;Mass = textBox7.Text;
            Text = textBox8.Text;State = comboBox2.Text;Price = int.Parse(textBox10.Text);FildType = comboBox1.SelectedIndex +1;

            ObjCommand = new SQLiteCommand("SELECT id FROM Machinery ORDER BY id DESC LIMIT 1", ObjConnection);
            ObjCommand.CommandType = CommandType.Text;ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
            ObjDataAdapter.Fill(dataSet, "id");
            string A = dataSet.Tables["id"].Rows[0][0].ToString();
            id = 1 + int.Parse(A);
            

            dataSet.Clear();
            ObjCommand = new SQLiteCommand("SELECT ID FROM NameofFirm where Name like'"+ Name + "'", ObjConnection);
            ObjCommand.CommandType = CommandType.Text;ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
            ObjDataAdapter.Fill(dataSet, "ID");

            if (dataSet.Tables["ID"].Rows.Count != 0)
            {
                IDM = int.Parse(dataSet.Tables["ID"].Rows[0][0].ToString());
            }
            else {
                dataSet.Clear();
                ObjCommand = new SQLiteCommand("SELECT ID FROM NameofFirm ORDER BY ID DESC LIMIT 1", ObjConnection);
                ObjCommand.CommandType = CommandType.Text;ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                ObjDataAdapter.Fill(dataSet, "ID");

                int ID, idv = 0;

                ID = 1 + int.Parse(dataSet.Tables["ID"].Rows[0][0].ToString());
                ObjCommand = new SQLiteCommand("INSERT INTO NameofFirm VALUES('" + Name + "','" + ID + "','" + idv + "')", ObjConnection);
                ObjCommand.Connection.Open();ObjCommand.ExecuteNonQuery();ObjCommand.Connection.Close();

                IDM = ID;
            }

            ObjCommand = new SQLiteCommand("INSERT INTO Machinery VALUES('" + Name + "','" + Model + "','" + Year + "','" + Type + "','" + Working_hours + "','" + Power + "','" + Mass + "','" + Text + "','" + State + "','" + Price + "','" + FildType + "','" + IDM + "',' 0','" + id + "')", ObjConnection);
            ObjCommand.Connection.Open();ObjCommand.ExecuteNonQuery();ObjCommand.Connection.Close();
            progressBar1.Value = 30;

            try
            {

                 FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://sa246943_ftp@sa246943.ftp.tools:21/annaland.test/www/img/HadImg/"+HadImg);
                 req.UseBinary = true;
                 req.Method = WebRequestMethods.Ftp.UploadFile;
                 req.Credentials = new NetworkCredential("sa246943_ftp", "Y35rcK1Z5h");
                // StreamReader rdr = new StreamReader(sFileName);
                 byte[] fileData = File.ReadAllBytes(HadImgPath);

                // rdr.Close();
                 req.ContentLength = fileData.Length;
                 Stream reqStream = req.GetRequestStream();
                 reqStream.Write(fileData, 0, fileData.Length);
                 reqStream.Close();

                string webimgpath = "/img/HadImg/" + HadImg;

                dataSet.Clear();
                ObjCommand = new SQLiteCommand("SELECT id FROM HadImgPath ORDER BY id DESC LIMIT 1", ObjConnection);
                ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                ObjDataAdapter.Fill(dataSet, "id");

                int id = 1 + int.Parse(dataSet.Tables["id"].Rows[0][0].ToString());

                ObjCommand = new SQLiteCommand("INSERT INTO HadImgPath VALUES('" + webimgpath + "','" + id + "','" + HadImg + "','0')", ObjConnection);
                ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                progressBar1.Value = 60;
                for (int i = 0; i < ImgName.Length;i++) {
                    
                    req = (FtpWebRequest)WebRequest.Create("ftp://sa246943_ftp@sa246943.ftp.tools:21/annaland.test/www/img/imges/" + ImgName[i]);
                    req.UseBinary = true;
                    req.Method = WebRequestMethods.Ftp.UploadFile;
                    req.Credentials = new NetworkCredential("sa246943_ftp", "Y35rcK1Z5h");
                    // StreamReader rdr = new StreamReader(sFileName);
                    fileData = File.ReadAllBytes(ImgNamePaths[i]);
                    // rdr.Close();
                    req.ContentLength = fileData.Length;
                    reqStream = req.GetRequestStream();
                    reqStream.Write(fileData, 0, fileData.Length);
                    reqStream.Close();

                    webimgpath = "/img/imges/" + ImgName[i];

                    dataSet.Clear();
                    ObjCommand = new SQLiteCommand("SELECT id FROM MachinesImg ORDER BY id DESC LIMIT 1", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "id");

                    id = 1 + int.Parse(dataSet.Tables["id"].Rows[0][0].ToString());

                    ObjCommand = new SQLiteCommand("INSERT INTO MachinesImg VALUES('" + webimgpath + "','" + id + "','" + ImgName[i] + "','0')", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                }
                progressBar1.Value = 90;

                /*   FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://sa246943_ftp@sa246943.ftp.tools:21/annaland.test/www/");

                   request.Method = WebRequestMethods.Ftp.UploadFile;
                   request.Credentials = new NetworkCredential("sa246943_ftp", "Y35rcK1Z5h");
                   // Copy the contents of the file to the request stream.  
                   StreamReader sourceStream = new StreamReader(sFileName);
                   byte[] fileContents = File.ReadAllBytes(sFileName);
                   sourceStream.Close();
                   request.ContentLength = fileContents.Length;
                   Stream requestStream = request.GetRequestStream();
                   requestStream.Write(fileContents, 0, fileContents.Length);
                   requestStream.Close();
                   FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                   label1.Text = ("Upload File Complete, status {0}" + response.StatusDescription).ToString();

                   response.Close();*/
            }
            catch (WebException et)
            {
              /*  label1.Text = et.Message.ToString();
                String status = ((FtpWebResponse)et.Response).StatusDescription;
                label2.Text = status;*/
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message.ToString();
            }
            textBox1.Text = "";textBox2.Text = "";textBox3.Text = "";textBox4.Text = "";textBox5.Text = "";textBox6.Text = "";textBox7.Text = "";textBox8.Text = "";comboBox1.Text = "";comboBox2.Text = "";textBox10.Text = "";listView1.Clear();
            progressBar1.Value = 100;


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
