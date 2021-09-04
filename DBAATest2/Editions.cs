using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DBAATest2
{
    public partial class Editions : Form
    {
        public Editions()
        {
            InitializeComponent();
        }
        public string conString = "Data Source=DESKTOP-VTO56LQ;Initial Catalog=DBAA;Integrated Security=True";

        private void Editions_Load(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "Select [IDEditors], [NameEditors] from Editors ";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    lstbox.DataSource = dt;
                    lstbox.DisplayMember = "NameEditors";
                    lstbox.ValueMember = "IDEditors";

                    //=====================================================

                    string sql3 = "SELECT DISTINCT [NameEditors],[IDEditors] FROM [dbo].[Editors] ORDER BY  IDEditors ";

                    SqlDataAdapter dat3 = new SqlDataAdapter(sql3, con);
                    DataTable dt3 = new DataTable();
                    dat3.Fill(dt3);
                    //dataGridView1.DataSource = dt3;
                    comboBox1.DataSource = dt3;
                    comboBox1.DisplayMember = "NameEditors";
                    comboBox1.ValueMember = "IDEditors";

                    //=====================================================

                    string sql4 = "SELECT DISTINCT [Name],[IDBook] FROM [dbo].[Book] ORDER BY  IDBook ";
                    SqlDataAdapter dat4 = new SqlDataAdapter(sql4, con);
                    DataTable dt4 = new DataTable();
                    dat4.Fill(dt4);
                    comboBox2.DataSource = dt4;
                    comboBox2.DisplayMember = "Name";
                    comboBox2.ValueMember = "IDBook";

                    //=====================================================

                    string sql5 = "SELECT DISTINCT [Type],[IDFormat] FROM [dbo].[Format] ORDER BY  IDFormat ";
                    SqlDataAdapter dat5 = new SqlDataAdapter(sql5, con);
                    DataTable dt5 = new DataTable();
                    dat5.Fill(dt5);
                    comboBox3.DataSource = dt5;
                    comboBox3.DisplayMember = "Type";
                    comboBox3.ValueMember = "IDFormat";


                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "INSERT INTO Editions(NumEditor, NumBook, NumFormat)values('" + comboBox1.SelectedValue.ToString() + "', '" + comboBox2.SelectedValue.ToString() + "', '" + comboBox3.SelectedValue.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Was added successfuly!");
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Editors();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            form.ShowDialog();
        }
    }
}
