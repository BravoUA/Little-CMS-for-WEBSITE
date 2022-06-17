using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class dbConnect
    {
        private SQLiteConnection ObjConnection;
        private SQLiteCommand ObjCommand;
        private SQLiteDataAdapter ObjDataAdapter;
        public DataSet dataSet = new DataSet();
        private string dbPath = "";
        public dbConnect(string dbPath)
        {
            this.dbPath = dbPath;
        }
        public void getCategories(int Categories)
        {
            string connString = string.Format("Data Source={0}", dbPath);
            using (ObjConnection = new SQLiteConnection(connString))
            {
                if (Categories == 1)
                {
                    dataSet.Clear();
                    dataSet.Dispose();
                    

                    ObjCommand = new SQLiteCommand("SELECT Name,Model,Year,Price,id,FildType FROM Machinery ORDER BY id DESC", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text;
                    ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "Machinery");
                    ObjCommand.Dispose();


                }
                else if (Categories == 2)
                {
                    dataSet.Clear();
                    dataSet.Dispose();
                    ObjCommand = new SQLiteCommand("SELECT Name,Model,Year,Price,id,FildType FROM Technic ORDER BY id DESC", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "Technic");
                    ObjCommand.Dispose();
                }
            }

        }
        public void editeDB(int Categories, int ID)
        {

            string connString = string.Format("Data Source={0}", dbPath);
            using (ObjConnection = new SQLiteConnection(connString))
            {


                if (Categories == 1)
                {
                    dataSet.Clear();
                    ObjCommand = new SQLiteCommand("SELECT * FROM Machinery where id = '" + ID + "'", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "Machinery");
                    dataSet.DataSetName = "Machinery";
                    ObjCommand.Dispose();
                }
                else if (Categories == 2)
                {
                    dataSet.Clear();
                    ObjCommand = new SQLiteCommand("SELECT * FROM Technic where id = '" + ID + "'", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "Technic");
                    dataSet.DataSetName = "Technic";
                    ObjCommand.Dispose();
                }
            }
        }
        public void deleteFromDB(int Categories, int ID)
        {
            string connString = string.Format("Data Source={0}", dbPath);
            using (ObjConnection = new SQLiteConnection(connString))
            {
                if (Categories == 1)
                {
                    ObjCommand = new SQLiteCommand("DELETE from Machinery WHERE id = '" + ID + "'", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close(); ObjCommand.Dispose();
                    ObjCommand = new SQLiteCommand("DELETE FROM HadImgPath WHERE id ='" + ID + "'", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close(); ObjCommand.Dispose();
                    ObjCommand = new SQLiteCommand("DELETE FROM MachinesImg WHERE id ='" + ID + "'", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close(); ObjCommand.Dispose();

                }
                else if (Categories == 2)
                {
                    ObjCommand = new SQLiteCommand("DELETE FROM Technic WHERE id ='" + ID + "'", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close(); ObjCommand.Dispose();
                    ObjCommand = new SQLiteCommand("DELETE FROM HadImgPathT WHERE id ='" + ID + "'", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close(); ObjCommand.Dispose();
                    ObjCommand = new SQLiteCommand("DELETE FROM TechnicImg WHERE id ='" + ID + "'", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close(); ObjCommand.Dispose();

                }
            }
            
        }
        public void insertToDB(int Categories, int ID, List<string> info)
        {

            string connString = string.Format("Data Source={0}", dbPath);
            using (ObjConnection = new SQLiteConnection(connString))
            {
                if (Categories == 1)
                {
                    ObjCommand = new SQLiteCommand("INSERT OR REPLACE INTO Machinery (Name, Model, Year, Type, Working_hours, Power, Mass, Text, State, Price, FildType, IDM, Sale, id) VALUES('" + info[0] + "','" + info[1] + "','" + info[2] + "','" + info[3] + "','" + info[4] + "','" + info[5] + "','" + info[6] + "','" + info[7] + "','" + info[8] + "','" + info[9] + "','" + info[10] + "','" + info[11] + "'," + info[12] + "," + info[13] + ")", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close(); ObjCommand.Dispose();

                }
                else if (Categories == 2)
                {
                    ObjCommand = new SQLiteCommand("INSERT OR REPLACE INTO Technic (Name, Model, Year, Type, Mass, Text, State, Price, FildType, IDT, Sale, id) VALUES('" + info[0] + "','" + info[1] + "','" + info[2] + "','" + info[3] + "','" + info[4] + "','" + info[5] + "','" + info[6] + "','" + info[7] + "','" + info[8] + "','" + info[9] + "'," + info[10] + ",'" + info[11] + "')", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close(); ObjCommand.Dispose();
                }
            }

        }
        public void CreateNew(int Categories, List<string> content, string[] HadImgPath_and_Name, List<string> ImgsPath, string[] ImgsName)
        {


            int IDM, id;
            string connString = string.Format("Data Source={0}", dbPath);
            using (ObjConnection = new SQLiteConnection(connString))
            {
                if (Categories == 1)
                {
                    ObjCommand = new SQLiteCommand("SELECT id FROM Machinery ORDER BY id DESC LIMIT 1", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "id");


                    id = 1 + int.Parse(dataSet.Tables["id"].Rows[0][0].ToString());


                    dataSet.Clear();
                    ObjCommand = new SQLiteCommand("SELECT ID FROM NameofFirm where Name like'" + content[0] + "'", ObjConnection);
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
                        ObjCommand = new SQLiteCommand("INSERT INTO NameofFirm VALUES('" + content[0] + "','" + ID + "','" + idv + "')", ObjConnection);
                        ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();

                        IDM = ID;
                    }

                    ObjCommand = new SQLiteCommand("INSERT INTO Machinery VALUES('" + content[0] + "','" + content[1] + "','" + content[2] + "','" + content[3] + "','" + content[4] + "','" + content[5] + "','" + content[6] + "','" + content[7] + "','" + content[8] + "','" + int.Parse(content[9]) + "','" + int.Parse(content[10]) + "','" + IDM + "',' 0','" + id + "')", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();


                    dataSet.Clear();
                    ObjCommand = new SQLiteCommand("SELECT id FROM HadImgPath ORDER BY id DESC LIMIT 1", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "id");
                    id = 1 + int.Parse(dataSet.Tables["id"].Rows[0][0].ToString());
                    ObjCommand = new SQLiteCommand("INSERT INTO HadImgPath VALUES('" + HadImgPath_and_Name[0] + "','" + id + "','" + HadImgPath_and_Name[1] + "','0')", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();


                    dataSet.Clear();
                    ObjCommand = new SQLiteCommand("SELECT idimg FROM MachinesImg ORDER BY id DESC LIMIT 1", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "idimg");
                    int idimg = 1 + int.Parse(dataSet.Tables["idimg"].Rows[0][0].ToString());

                    for (int i = 0; i < ImgsName.Length; i++)
                    {
                        ObjCommand = new SQLiteCommand("INSERT INTO MachinesImg VALUES('" + ImgsPath[i] + "','" + idimg + "','" + id + "','" + ImgsName[i] + "','0')", ObjConnection);
                        ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                        idimg++;
                        id++;
                    }

                    ObjCommand.Dispose();
                }
                else if (Categories == 2)
                {
                    ObjCommand = new SQLiteCommand("SELECT id FROM Technic ORDER BY id DESC LIMIT 1", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "id");
                    string A = dataSet.Tables["id"].Rows[0][0].ToString();
                    id = 1 + int.Parse(A);


                    dataSet.Clear();
                    ObjCommand = new SQLiteCommand("SELECT ID FROM NameofFirm where Name like'" + content[0] + "'", ObjConnection);
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
                        ObjCommand = new SQLiteCommand("INSERT INTO NameofFirm VALUES('" + content[0] + "','" + ID + "','" + idv + "')", ObjConnection);
                        ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();

                        IDM = ID;
                    }

                    ObjCommand = new SQLiteCommand("INSERT INTO Technic VALUES('" + content[0] + "','" + content[1] + "','" + content[2] + "','" + content[3] + "','" + content[6] + "','" + content[7] + "','" + content[8] + "','" + int.Parse(content[9]) + "','" + int.Parse(content[10]) + "','" + IDM + "',' 0','" + id + "')", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();


                    ObjCommand = new SQLiteCommand("INSERT INTO HadImgPathT VALUES('" + HadImgPath_and_Name[0] + "','" + id + "','" + HadImgPath_and_Name[1] + "','0')", ObjConnection);
                    ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();


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
                    for (int i = 0; i < ImgsName.Length; i++)
                    {
                        ObjCommand = new SQLiteCommand("INSERT INTO TechnicImg VALUES('" + ImgsPath + "','" + idimg + "','" + id + "','" + ImgsName[i] + "','0')", ObjConnection);
                        ObjCommand.Connection.Open(); ObjCommand.ExecuteNonQuery(); ObjCommand.Connection.Close();
                        id++;
                        idimg++;
                    }

                    ObjCommand.Dispose();
                }
                
                ObjCommand.Dispose();
            }
        }

        public void FindByName(string Name, int Categories) {

            string connString = string.Format("Data Source={0}", dbPath);
            using (ObjConnection = new SQLiteConnection(connString))
            {
                if (Categories == 1)
                {
                    dataSet.Clear();

                    ObjCommand = new SQLiteCommand("SELECT Name,Model,Year,Price,id,FildType FROM Machinery where Name like '" + Name + "%'  ORDER BY id DESC", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text;
                    ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "Machinery");
                    ObjCommand.Dispose();


                }
                else if (Categories == 2)
                {
                    dataSet.Clear();
                    ObjCommand = new SQLiteCommand("SELECT Name,Model,Year,Price,id,FildType FROM Technic where Name like '" + Name + "%' ORDER BY id DESC", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "Technic");
                    ObjCommand.Dispose();
                }
            }
        }
        public void FindByType(string Type, int Categories)
        {

            string connString = string.Format("Data Source={0}", dbPath);
            using (ObjConnection = new SQLiteConnection(connString))
            {
                if (Categories == 1)
                {
                    dataSet.Clear();
                    int TypeT = 1 + int.Parse(Type);
                    ObjCommand = new SQLiteCommand("SELECT Name,Model,Year,Price,id,FildType FROM Machinery where FildType like '" + TypeT + "%'  ORDER BY id DESC", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text;
                    ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "Machinery");
                    ObjCommand.Dispose();


                }
                else if (Categories == 2)
                {
                    dataSet.Clear();
                    int TypeT = 4 + int.Parse(Type);
                    ObjCommand = new SQLiteCommand("SELECT Name,Model,Year,Price,id,FildType FROM Technic where FildType like '" + TypeT + "%' ORDER BY id DESC", ObjConnection);
                    ObjCommand.CommandType = CommandType.Text; ObjDataAdapter = new SQLiteDataAdapter(ObjCommand);
                    ObjDataAdapter.Fill(dataSet, "Technic");
                    ObjCommand.Dispose();
                }
            }
        }
    }
}
