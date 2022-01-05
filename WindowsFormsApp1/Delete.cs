﻿using System;
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
    public partial class Delete : Form
    {
        public Delete()
        {
            InitializeComponent();
        }

        string HadImg;
        string HadImgPath;

        string Name, Model, Year, Type, Working_hours, Power, Mass, Text, State;

        int Price, FildType, IDM, Sale, id, DeleteId , DeleteIdBUT;

        SQLiteConnection ObjConnection;
        public SQLiteCommand ObjCommand;
        public SQLiteDataAdapter ObjDataAdapter;
        DataSet dataSet = new DataSet();
        ImageList img = new ImageList();

        string[] ImgName;
        string[] ImgNamePaths;

        private void button5_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button4.Visible = false;

            if (dataGridView1.SelectedCells[0].RowIndex < 2)
            {
                try
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    Name = Convert.ToString(selectedRow.Cells["Name"].Value);
                    Model = Convert.ToString(selectedRow.Cells["Model"].Value);
                    Year = Convert.ToString(selectedRow.Cells["Year"].Value);
                    Type = Convert.ToString(selectedRow.Cells["Type"].Value);
                    Working_hours = Convert.ToString(selectedRow.Cells["Working_hours"].Value);
                    Power = Convert.ToString(selectedRow.Cells["Power"].Value);
                    Mass = Convert.ToString(selectedRow.Cells["Mass"].Value);
                    Text = Convert.ToString(selectedRow.Cells["Text"].Value);
                    State = Convert.ToString(selectedRow.Cells["State"].Value);
                    Price = int.Parse(Convert.ToString(selectedRow.Cells["Price"].Value));
                    FildType = int.Parse(Convert.ToString(selectedRow.Cells["FildType"].Value));
                    IDM = int.Parse(Convert.ToString(selectedRow.Cells["IDM"].Value));
                    Sale = int.Parse(Convert.ToString(selectedRow.Cells["Sale"].Value));
                    id = int.Parse(Convert.ToString(selectedRow.Cells["id"].Value));
                }
                catch (FormatException eror) {
                    MessageBox.Show(eror.ToString());
                }
                
                


                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                dataSet.Clear();
                try {
                ObjConnection = new SQLiteConnection("Data Source=ALDB.db;");
                ObjCommand = new SQLiteCommand("INSERT OR REPLACE INTO Machinery (Name, Model, Year, Type, Working_hours, Power, Mass, Text, State, Price, FildType, IDM, Sale, id) VALUES('" + Name + "','" + Model + "','" + Year + "','" + Type + "','" + Working_hours + "','" + Power + "','" + Mass + "','" + Text + "','" + State + "','" + Price + "','" + FildType + "','" + IDM + "',' 0','" + id + "')", ObjConnection);
                ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                }
                catch(SQLiteException eror) {
                    MessageBox.Show("Виникла Помилка\n Не використовуйте Апостроф чи подвійний Апостроф\n Якщо вони потрібні поставте перед ними символ «\\»");
                }


              
                button2.Visible = true;
                button3.Visible = true;
            
                button5.Visible = false;
            }
        }



        private void Delete_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0 && button5.Visible== false)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string cellValue = Convert.ToString(selectedRow.Cells["id"].Value);
                string cellValue2 = Convert.ToString(selectedRow.Cells["FildType"].Value);

                DeleteId = int.Parse(cellValue);
                int DeleteIdFildType = int.Parse(cellValue2);

                switch (DeleteIdFildType) { 
                    case 1:
                        System.Diagnostics.Process.Start("http://www.annaland.com.ua/SelectedMorT.php?techW=M&id='" + DeleteId + "'#Back");
                        break;
                    case 2:
                        System.Diagnostics.Process.Start("http://www.annaland.com.ua/SelectedMorT.php?techW=S&id='" + DeleteId + "'#Back");
                        break;

                    case 3:
                        System.Diagnostics.Process.Start("http://www.annaland.com.ua/SelectedMorT.php?techW=Z&id='" + DeleteId + "'#Back");
                        break;
                    case 4:
                        System.Diagnostics.Process.Start("http://www.annaland.com.ua/SelectedMorT.php?techW=T&typeW=P&id='" + DeleteId + "'#Back");
                        break;
                    case 5:
                        System.Diagnostics.Process.Start("http://www.annaland.com.ua/SelectedMorT.php?techW=T&typeW=G&id='" + DeleteId + "'#Back");
                        break;
                    case 6:
                        System.Diagnostics.Process.Start("http://www.annaland.com.ua/SelectedMorT.php?techW=T&typeW=PS&id='" + DeleteId + "'#Back");
                        break;
                    case 7:
                        System.Diagnostics.Process.Start("http://www.annaland.com.ua/SelectedMorT.php?techW=T&typeW=I&id='" + DeleteId + "'#Back");
                        break;
                    case 8:
                        System.Diagnostics.Process.Start("http://www.annaland.com.ua/SelectedMorT.php?techW=T&typeW=O&id='" + DeleteId + "'#Back");
                        break;
                }


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = true;

            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string cellValue = Convert.ToString(selectedRow.Cells["id"].Value);
                DeleteId = int.Parse(cellValue);
            }

            dataGridView1.DataSource = null;
            dataGridView1.Refresh();

            dataSet.Clear();
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            
            ObjConnection = new SQLiteConnection("Data Source=ALDB.db;");
            ObjCommand = new SQLiteCommand("SELECT * FROM Machinery where id = '"+ DeleteId + "'", ObjConnection);
            ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
            ObjDataAdapter.Fill(dataSet, "Machinery");
            DataTable AllTr = dataSet.Tables["Machinery"];
            dataGridView1.DataSource = AllTr;
        
            
            DeleteIdBUT = 1;
            if (dataGridView1.SelectedColumns.Count == 7) {

                EditDB edit = new EditDB();
                edit.Show();


            }
            

        }

   
        private void Delete_Load(object sender, EventArgs e)
        {
            /*  FtpWebRequest request =(FtpWebRequest)WebRequest.Create("ftp://sa246943_ftp@sa246943.ftp.tools:21/annaland.test/www/ALDB.db");
            request.Credentials = new NetworkCredential("sa246943_ftp", "Y35rcK1Z5h");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            using (Stream ftpStream = request.GetResponse().GetResponseStream())
            using (Stream fileStream = File.Create(@"C:\DOOOOOO\ALDB.db"))
            {
                ftpStream.CopyTo(fileStream);
            }*/


            /*  FtpWebRequest request =(FtpWebRequest)WebRequest.Create("ftp://sa246943_ftp@sa246943.ftp.tools:21/annaland.test/www/ALDB.db");
          request.Credentials = new NetworkCredential("sa246943_ftp", "Y35rcK1Z5h");
          request.Method = WebRequestMethods.Ftp.DownloadFile;

          using (Stream ftpStream = request.GetResponse().GetResponseStream())
          using (Stream fileStream = File.Create(@"C:\DOOOOOO\ALDB.db"))
          {
              ftpStream.CopyTo(fileStream);
          }*/

         







        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataSet.Clear();
            ObjConnection = new SQLiteConnection("Data Source=ALDB.db;");
            ObjCommand = new SQLiteCommand("SELECT Name,Model,Year,Price,id,FildType FROM Technic ORDER BY Name ASC", ObjConnection);
            ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
            ObjDataAdapter.Fill(dataSet, "Technic");
            DataTable AllTr = dataSet.Tables["Technic"];
            dataGridView1.DataSource = AllTr;
            DeleteIdBUT = 2;
        }

        private void button2_Click(object sender, EventArgs e)

        {
            button1.Visible = true;
            button4.Visible = true;
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataSet.Clear();
            ObjConnection = new SQLiteConnection("Data Source=ALDB.db;");
            ObjCommand = new SQLiteCommand("SELECT Name,Model,Year,Price,id,FildType FROM Machinery ORDER BY Name ASC", ObjConnection);
            ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
            ObjDataAdapter.Fill(dataSet, "Machinery");
            DataTable AllTr = dataSet.Tables["Machinery"];
            dataGridView1.DataSource = AllTr;
            DeleteIdBUT = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button4.Visible = false;
            ObjConnection = new SQLiteConnection("Data Source=ALDB.db;");
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string cellValue = Convert.ToString(selectedRow.Cells["id"].Value);
                DeleteId = int.Parse(cellValue);
            }
            if (DeleteIdBUT == 1) {
                ObjCommand = new SQLiteCommand("DELETE from Machinery WHERE id ='" + DeleteId + "'", ObjConnection);
                ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                ObjCommand = new SQLiteCommand("DELETE FROM HadImgPath WHERE id ='" + DeleteId + "'", ObjConnection);
                ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                ObjCommand = new SQLiteCommand("DELETE FROM MachinesImg WHERE id ='" + DeleteId + "'", ObjConnection);
                ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                ObjConnection = new SQLiteConnection("Data Source=ALDB.db;");
                this.Refresh();

            }
            else if(DeleteIdBUT == 2) {
                ObjCommand = new SQLiteCommand("DELETE FROM Technic WHERE id ='" + DeleteId + "'", ObjConnection);
                ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                ObjCommand = new SQLiteCommand("DELETE FROM HadImgPathT WHERE id ='" + DeleteId + "'", ObjConnection);
                ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                ObjCommand = new SQLiteCommand("DELETE FROM TechnicImg WHERE id ='" + DeleteId + "'", ObjConnection);
                ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                ObjConnection = new SQLiteConnection("Data Source=ALDB.db;");
                this.Refresh();

            }


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
    }
}
