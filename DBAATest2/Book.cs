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
    public partial class Book : Form
    {
        public Book()
        {
            InitializeComponent();
        }
        public string conString = "Data Source=DESKTOP-VTO56LQ;Initial Catalog=DBAA;Integrated Security=True";

        private void Book_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "Select [IDBook], [Name] from Book ";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    lstbox.DataSource = dt;
                    lstbox.DisplayMember = "Name";
                    lstbox.ValueMember = "IDBook";

                    //=====================================================

                    string sql3 = "SELECT DISTINCT [Name],[IDGenre] FROM [dbo].[Genre] ORDER BY  IDGenre ";

                    SqlDataAdapter dat3 = new SqlDataAdapter(sql3, con);
                    DataTable dt3 = new DataTable();
                    dat3.Fill(dt3);
                    //dataGridView1.DataSource = dt3;
                    comboBox1.DataSource = dt3;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "IDGenre";

                    string sql4 = "SELECT DISTINCT [LastName],[IDAuthor] FROM [dbo].[Author] ORDER BY  IDAuthor ";
                    SqlDataAdapter dat4 = new SqlDataAdapter(sql4, con);
                    DataTable dt4 = new DataTable();
                    dat4.Fill(dt4);
                    comboBox2.DataSource = dt4;
                    comboBox2.DisplayMember = "LastName";
                    comboBox2.ValueMember = "IDAuthor";


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
                    string q = "INSERT INTO Book(Name, Author, Genre)values('" + txtName.Text.ToString() + "', '" + comboBox2.SelectedValue.ToString() + "', '" + comboBox1.SelectedValue.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Was added successfuly!");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
