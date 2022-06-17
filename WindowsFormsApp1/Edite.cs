using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Edite : Form
    {
        public Edite()
        {
            InitializeComponent();
        }
        static string dbPath = "C:\\AnnalandBD\\ALDB.db";
        dbConnect DB = new dbConnect(dbPath);
        public DataTable AllTr;
        public DataSet DS = new DataSet();
        public int Categories;
        int ID;
        string IDM, IDT, SALE;
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> content = new List<string>();
            if (Categories == 1)
            {
                content.Add(NamePos.Text);     //Nmae
                content.Add(Model.Text);     //Model
                content.Add(Year.Text);     //Year
                content.Add(Type.Text);     //Type
                content.Add(Working_hours.Text);     //Working_hours
                content.Add(Power.Text);     //Power
                content.Add(Mass.Text);     //Mass
                content.Add(TextInfo.Text);     //Text
                content.Add(comboBox2.Text);    //State
                content.Add(Price.Text);    //Price
                content.Add((comboBox1.SelectedIndex + 1).ToString());  //FildType
                content.Add(IDM);
                content.Add(SALE);
                content.Add(ID.ToString());


                DB.insertToDB(Categories, ID, content);       
                MessageBox.Show("upload is complete");
                this.Close();
            }
            if (Categories == 2)
            {
                content.Add(NamePos.Text);     //Nmae
                content.Add(Model.Text);     //Model
                content.Add(Year.Text);     //Year
                content.Add(Type.Text);     //Type
                content.Add(TextInfo.Text);     //Text
                content.Add(Mass.Text);     //Mass
                content.Add(comboBox2.Text);    //State
                content.Add(Price.Text);    //Price
                content.Add((comboBox1.SelectedIndex + 1).ToString());  //FildType
                content.Add(IDT);
                content.Add(SALE);
                content.Add(ID.ToString());


                DB.insertToDB(Categories, ID, content);
                MessageBox.Show("upload is complete");
                this.Close();
            }
        }

        private void Edite_Load(object sender, EventArgs e)
        {
            if (Categories == 1)
            {
                ID = int.Parse(DS.Tables["Machinery"].Rows[0][4].ToString());
                IDM = DS.Tables["Machinery"].Rows[0][12].ToString();
                SALE = DS.Tables["Machinery"].Rows[0][13].ToString();
                NamePos.Text = DS.Tables["Machinery"].Rows[0][0].ToString();
                Model.Text = DS.Tables["Machinery"].Rows[0][1].ToString();
                Year.Text = DS.Tables["Machinery"].Rows[0][2].ToString();
                Type.Text = DS.Tables["Machinery"].Rows[0][6].ToString();
                Working_hours.Text = DS.Tables["Machinery"].Rows[0][7].ToString();
                Power.Text = DS.Tables["Machinery"].Rows[0][8].ToString();
                Mass.Text = DS.Tables["Machinery"].Rows[0][9].ToString();
                comboBox2.Text = DS.Tables["Machinery"].Rows[0][11].ToString();
                Price.Text = DS.Tables["Machinery"].Rows[0][3].ToString();
                comboBox1.SelectedIndex = int.Parse(DS.Tables["Machinery"].Rows[0][5].ToString()) - 1;
                TextInfo.Text = DS.Tables["Machinery"].Rows[0][10].ToString();
            }
            if (Categories == 2)
            {
                label10.Visible = false;
                label9.Visible = false;
                Working_hours.Visible = false;
                Power.Visible = false;  
                NamePos.Text =  DS.Tables["Technic"].Rows[0][0].ToString();
                Model.Text =    DS.Tables["Technic"].Rows[0][1].ToString();
                Year.Text =     DS.Tables["Technic"].Rows[0][2].ToString();
                Type.Text =     DS.Tables["Technic"].Rows[0][6].ToString();
                TextInfo.Text = DS.Tables["Technic"].Rows[0][7].ToString();
                Mass.Text =     DS.Tables["Technic"].Rows[0][8].ToString();
               
                Price.Text =    DS.Tables["Technic"].Rows[0][3].ToString();
                comboBox2.Text =DS.Tables["Technic"].Rows[0][9].ToString();
                IDT =           DS.Tables["Technic"].Rows[0][10].ToString();
                comboBox1.SelectedIndex = int.Parse(DS.Tables["Technic"].Rows[0][5].ToString()) - 1;
                SALE = DS.Tables["Technic"].Rows[0][11].ToString();
                ID = int.Parse( DS.Tables["Technic"].Rows[0][4].ToString());
              
                

            }


        }
    }
}
