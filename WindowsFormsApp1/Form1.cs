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

        SQLiteConnection ObjConnection;
        public SQLiteCommand ObjCommand;
        public SQLiteDataAdapter ObjDataAdapter;
        DataSet dataSet = new DataSet();


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

        string dbPath = "C:\\AnnalandBD\\ALDB.db";
      

        private void Form1_Load(object sender, EventArgs e)
        {


            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            listView1.View = View.Details;
            listView1.Columns.Add("CONTENT IMGES",150);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            this.progressBar1.Maximum = 100;

          
            

            
            string connString = string.Format("Data Source={0}", dbPath);
            ObjConnection = new SQLiteConnection(connString);
            
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox3.SelectedIndex == 0) {
                Name = textBox1.Text; Model = textBox2.Text; Year = textBox3.Text; Type = textBox4.Text; Working_hours = textBox5.Text; Power = textBox6.Text; Mass = textBox7.Text;
                Text = textBox8.Text; State = comboBox2.Text; Price = int.Parse(textBox10.Text); FildType = comboBox1.SelectedIndex + 1;

                ObjCommand = new SQLiteCommand("SELECT id FROM Machinery ORDER BY id DESC LIMIT 1", ObjConnection);
                ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                ObjDataAdapter.Fill(dataSet, "id");
                string A = dataSet.Tables["id"].Rows[0][0].ToString();
                id = 1 + int.Parse(A);


                dataSet.Clear();
                ObjCommand = new SQLiteCommand("SELECT ID FROM NameofFirm where Name like'" + Name + "'", ObjConnection);
                ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                ObjDataAdapter.Fill(dataSet, "ID");

                if (dataSet.Tables["ID"].Rows.Count != 0)
                {
                    IDM = int.Parse(dataSet.Tables["ID"].Rows[0][0].ToString());
                }
                else
                {
                    dataSet.Clear();
                    ObjCommand = new SQLiteCommand("SELECT ID FROM NameofFirm ORDER BY ID DESC LIMIT 1", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "ID");

                    int ID, idv = 0;

                    ID = 1 + int.Parse(dataSet.Tables["ID"].Rows[0][0].ToString());
                    ObjCommand = new SQLiteCommand("INSERT INTO NameofFirm VALUES('" + Name + "','" + ID + "','" + idv + "')", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();

                    IDM = ID;
                }

                ObjCommand = new SQLiteCommand("INSERT INTO Machinery VALUES('" + Name + "','" + Model + "','" + Year + "','" + Type + "','" + Working_hours + "','" + Power + "','" + Mass + "','" + Text + "','" + State + "','" + Price + "','" + FildType + "','" + IDM + "',' 0','" + id + "')", ObjConnection);
                ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                progressBar1.Value = 30;

                try
                {

                    FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://sa246943.ftp.tools/img/HadImg/" + HadImg);
                    req.UseBinary = true;
                    req.Method = WebRequestMethods.Ftp.UploadFile;
                    req.Credentials = new NetworkCredential("sa246943_alws", "mNC6Eix648hD");
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
                    for (int i = 0; i < ImgName.Length; i++)
                    {

                        req = (FtpWebRequest)WebRequest.Create("ftp://sa246943.ftp.tools/img/imges/" + ImgName[i]);
                        req.UseBinary = true;
                        req.Method = WebRequestMethods.Ftp.UploadFile;
                        req.Credentials = new NetworkCredential("sa246943_alws", "mNC6Eix648hD");
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

                        dataSet.Clear();
                        ObjCommand = new SQLiteCommand("SELECT idimg FROM MachinesImg ORDER BY idimg DESC LIMIT 1", ObjConnection);
                        ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                        ObjDataAdapter.Fill(dataSet, "idimg");

                        int idimg = 1 + int.Parse(dataSet.Tables["idimg"].Rows[0][0].ToString());


                        ObjCommand = new SQLiteCommand("INSERT INTO MachinesImg VALUES('" + webimgpath + "','" + idimg + "','" + id + "','" + ImgName[i] + "','0')", ObjConnection);
                        ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                    }
                    progressBar1.Value = 90;


                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://sa246943.ftp.tools/" + "ALDB.db");
                    request.UseBinary = true;
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential("sa246943_alws", "mNC6Eix648hD");
                    // StreamReader rdr = new StreamReader(sFileName);
                    fileData = File.ReadAllBytes(dbPath);

                    // rdr.Close();
                    request.ContentLength = fileData.Length;
                    reqStream = request.GetRequestStream();
                    reqStream.Write(fileData, 0, fileData.Length);
                    reqStream.Close();


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
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = ""; comboBox1.Text = ""; comboBox2.Text = ""; textBox10.Text = ""; listView1.Clear();
                progressBar1.Value = 100;
                ObjConnection.Close();


                MessageBox.Show("upload is complete");
                this.Close();
            } else if (comboBox3.SelectedIndex == 1) {
                Name = textBox1.Text; Model = textBox2.Text; Year = textBox3.Text; Type = textBox4.Text; Mass = textBox7.Text;
                Text = textBox8.Text; State = comboBox2.Text; Price = int.Parse(textBox10.Text); FildType = comboBox1.SelectedIndex + 1;

                ObjCommand = new SQLiteCommand("SELECT id FROM Technic ORDER BY id DESC LIMIT 1", ObjConnection);
                ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                ObjDataAdapter.Fill(dataSet, "id");
                string A = dataSet.Tables["id"].Rows[0][0].ToString();
                id = 1 + int.Parse(A);


                dataSet.Clear();
                ObjCommand = new SQLiteCommand("SELECT ID FROM NameofFirm where Name like'" + Name + "'", ObjConnection);
                ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                ObjDataAdapter.Fill(dataSet, "ID");

                if (dataSet.Tables["ID"].Rows.Count != 0)
                {
                    IDM = int.Parse(dataSet.Tables["ID"].Rows[0][0].ToString());
                }
                else
                {
                    dataSet.Clear();
                    ObjCommand = new SQLiteCommand("SELECT ID FROM NameofFirm ORDER BY ID DESC LIMIT 1", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "ID");

                    int ID, idv = 0;

                    ID = 1 + int.Parse(dataSet.Tables["ID"].Rows[0][0].ToString());
                    ObjCommand = new SQLiteCommand("INSERT INTO NameofFirm VALUES('" + Name + "','" + ID + "','" + idv + "')", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();

                    IDM = ID;
                }

                ObjCommand = new SQLiteCommand("INSERT INTO Technic VALUES('" + Name + "','" + Model + "','" + Year + "','" + Type + "','" + Text + "','" + Mass + "','" + State + "','" + Price + "','" + FildType + "','" + IDM + "',' 0','" + id + "')", ObjConnection);
                ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                progressBar1.Value = 30;

                try
                {

                    FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://sa246943.ftp.tools/img/HadImg/" + HadImg);
                    req.UseBinary = true;
                    req.Method = WebRequestMethods.Ftp.UploadFile;
                    req.Credentials = new NetworkCredential("sa246943_alws", "mNC6Eix648hD");
                    // StreamReader rdr = new StreamReader(sFileName);
                    byte[] fileData = File.ReadAllBytes(HadImgPath);

                    // rdr.Close();
                    req.ContentLength = fileData.Length;
                    Stream reqStream = req.GetRequestStream();
                    reqStream.Write(fileData, 0, fileData.Length);
                    reqStream.Close();

                    string webimgpath = "/img/HadImg/" + HadImg;

                    dataSet.Clear();
                    ObjCommand = new SQLiteCommand("SELECT id FROM HadImgPathT ORDER BY id DESC LIMIT 1", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "id");

                    int id = 1 + int.Parse(dataSet.Tables["id"].Rows[0][0].ToString());

                    ObjCommand = new SQLiteCommand("INSERT INTO HadImgPathT VALUES('" + webimgpath + "','" + id + "','" + HadImg + "','0')", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                    progressBar1.Value = 60;
                    for (int i = 0; i < ImgName.Length; i++)
                    {

                        req = (FtpWebRequest)WebRequest.Create("ftp://sa246943.ftp.tools/img/imges/" + ImgName[i]);
                        req.UseBinary = true;
                        req.Method = WebRequestMethods.Ftp.UploadFile;
                        req.Credentials = new NetworkCredential("sa246943_alws", "mNC6Eix648hD");
                        // StreamReader rdr = new StreamReader(sFileName);
                        fileData = File.ReadAllBytes(ImgNamePaths[i]);
                        // rdr.Close();
                        req.ContentLength = fileData.Length;
                        reqStream = req.GetRequestStream();
                        reqStream.Write(fileData, 0, fileData.Length);
                        reqStream.Close();

                        webimgpath = "/img/imges/" + ImgName[i];

                        dataSet.Clear();
                        ObjCommand = new SQLiteCommand("SELECT id FROM TechnicImg ORDER BY id DESC LIMIT 1", ObjConnection);
                        ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                        ObjDataAdapter.Fill(dataSet, "id");

                        id = 1 + int.Parse(dataSet.Tables["id"].Rows[0][0].ToString());

                        dataSet.Clear();
                        ObjCommand = new SQLiteCommand("SELECT idimg FROM TechnicImg ORDER BY IdImg DESC LIMIT 1", ObjConnection);
                        ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                        ObjDataAdapter.Fill(dataSet, "IdImg");

                        int idimg = 1 + int.Parse(dataSet.Tables["IdImg"].Rows[0][0].ToString());


                        ObjCommand = new SQLiteCommand("INSERT INTO TechnicImg VALUES('" + webimgpath + "','" + idimg + "','" + id + "','" + ImgName[i] + "','0')", ObjConnection);
                        ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                    }
                    progressBar1.Value = 90;


                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://sa246943.ftp.tools/" + "ALDB.db");
                    request.UseBinary = true;
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential("sa246943_alws", "mNC6Eix648hD");
                    // StreamReader rdr = new StreamReader(sFileName);
                    fileData = File.ReadAllBytes(dbPath);

                    // rdr.Close();
                    request.ContentLength = fileData.Length;
                    reqStream = request.GetRequestStream();
                    reqStream.Write(fileData, 0, fileData.Length);
                    reqStream.Close();


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
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = ""; comboBox1.Text = ""; comboBox2.Text = ""; textBox10.Text = ""; listView1.Clear();
                progressBar1.Value = 100;
                ObjConnection.Close();


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
