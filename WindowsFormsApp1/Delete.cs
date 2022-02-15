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
using WindowsFormsApp1;

namespace WindowsFormsApp1
{
    public partial class Delete : Form
    {
        public Delete()
        {
            InitializeComponent();
        }

    
        DataSet dataSet = new DataSet();
        ImageList img = new ImageList();
        dbConnect DBCON = new dbConnect(dbPath);
        string[] ImgName;
        string[] ImgNamePaths;
        static string dbPath = "C:\\AnnalandBD\\ALDB.db";
        string HadImg;
        string HadImgPath;
        int Categories = 0;

        string Name, Model, Year, Type, Working_hours, Power, Mass, Text, State;

        int Price, FildType, IDM, Sale, id, DeleteId , DeleteIdBUT;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            DBCON.FindByName(textBox1.Text, Categories);
            if (Categories == 1)
            {
                DataTable AllTr = DBCON.dataSet.Tables["Machinery"];
                dataGridView1.DataSource = AllTr;
            }
            else if (Categories == 2)
            {
                DataTable AllTr = DBCON.dataSet.Tables["Technic"];
                dataGridView1.DataSource = AllTr;
            }
            

        }

     
        




        private void button5_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button4.Visible = false;
            List<string> listChenges = new List<string>();

            if (Categories == 1)
            {
                if (dataGridView1.SelectedCells[0].RowIndex < 2)
                {
                    try
                    {
                        int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Name"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Model"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Year"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Type"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Working_hours"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Power"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Mass"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Text"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["State"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Price"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["FildType"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["IDM"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Sale"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["id"].Value));

                    }
                    catch (FormatException eror)
                    {
                        MessageBox.Show(eror.ToString());
                    }

                    dataGridView1.DataSource = null;
                    dataGridView1.Refresh();
                    dataSet.Clear();
                    try
                    {

                        DBCON.insertToDB(Categories, DeleteId, listChenges);
                        dataGridView1.ReadOnly = true;
                        dataGridView1.DataSource = null;
                        dataGridView1.Refresh();
                    }
                    catch (SQLiteException eror)
                    {
                        MessageBox.Show("Виникла Помилка\n Не використовуйте Апостроф чи подвійний Апостроф\n Якщо вони потрібні поставте перед ними символ «\\»");
                    }



                    button2.Visible = true;
                    button3.Visible = true;

                    button5.Visible = false;
                }
            }
            else if (Categories == 2)
            {
                if (dataGridView1.SelectedCells[0].RowIndex < 2)
                {
                    try
                    {
                        int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Name"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Model"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Year"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Type"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Mass"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Text"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["State"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Price"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["FildType"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["IDT"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["Sale"].Value));
                        listChenges.Add(Convert.ToString(selectedRow.Cells["id"].Value));

                    }
                    catch (FormatException eror)
                    {
                        MessageBox.Show(eror.ToString());
                    }

                    dataGridView1.DataSource = null;
                    dataGridView1.Refresh();
                    dataSet.Clear();
                    try
                    {

                        DBCON.insertToDB(Categories, DeleteId, listChenges);
                        dataGridView1.ReadOnly = true;
                        dataGridView1.DataSource = null;
                        dataGridView1.Refresh();
                    }
                    catch (SQLiteException eror)
                    {
                        MessageBox.Show("Виникла Помилка\n Не використовуйте Апостроф чи подвійний Апостроф\n Якщо вони потрібні поставте перед ними символ «\\»");
                    }



                    button2.Visible = true;
                    button3.Visible = true;

                    button5.Visible = false;
                }
            }

           
          
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

            
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;

            DBCON.editeDB(Categories, DeleteId);

            if (Categories == 1)
            {
                DataTable AllTr = DBCON.dataSet.Tables["Machinery"];
                dataGridView1.DataSource = AllTr;
            }
            else if (Categories == 2)
            {
                DataTable AllTr = DBCON.dataSet.Tables["Technic"];
                dataGridView1.DataSource = AllTr;
            }
            
        


        }

        private void Delete_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button4.Visible = true;
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            DBCON.getCategories(2);
            DataTable AllTr = DBCON.dataSet.Tables["Technic"];
            dataGridView1.DataSource = AllTr;       
            Categories = 2;
        }

        private void button2_Click(object sender, EventArgs e)

        {
            button1.Visible = true;
            button4.Visible = true;
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            DBCON.getCategories(1);
            DataTable AllTr = DBCON.dataSet.Tables["Machinery"];
            dataGridView1.DataSource = AllTr;          
            Categories = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button4.Visible = false;
           
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string cellValue = Convert.ToString(selectedRow.Cells["id"].Value);
                DeleteId = int.Parse(cellValue);
            }
            if (Categories == 1) {
                DBCON.deleteFromDB(Categories, DeleteId);
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                this.Refresh();
            }
            else if(Categories == 2) {
                DBCON.deleteFromDB(Categories, DeleteId);
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                this.Refresh();
            }
        }

    }
}
