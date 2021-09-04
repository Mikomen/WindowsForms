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
    public partial class Author : Form
    {
        public Author()
        {
            InitializeComponent();
        }
        public string conString = "Data Source=DESKTOP-VTO56LQ;Initial Catalog=DBAA;Integrated Security=True";


        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "INSERT INTO Author(FirstName, LastName, Genre)values('" + txtFirstName.Text.ToString() + "', '" + txtLastName.Text.ToString() + "', '" + comboBox1.SelectedValue.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Was added successfuly!");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Вы уверены что хотите удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        /*string q = "DELETE FROM Author WHERE IDAuthor = @ID";
                        string type = ((DataRowView)lstbox.SelectedItem).Row[0].ToString();
                        SqlCommand cmd = new SqlCommand(q, con);
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(type));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Was deleted successfuly!");*/
                        string q = "update Author set isFired=1 where IDAuthor=@ID";
                        string type = ((DataRowView)lstbox.SelectedItem).Row[0].ToString();
                        SqlCommand cmd = new SqlCommand(q, con);
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(type));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Was hidden successfuly!");


                    }
                }
            }
            else {
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "UPDATE Author set FirstName=@FirstName, LastName=@LastName WHERE IDAuthor = @ID";
                    string type = ((DataRowView)lstbox.SelectedItem).Row[0].ToString();
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(type));
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Was updated successfuly!");
                }
            }
        }

        private void Author_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "Select [IDAuthor], [LastName] from Author WHERE isFired<1 ";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    lstbox.DataSource = dt;
                    lstbox.DisplayMember = "LastName";
                    lstbox.ValueMember = "IDAuthor";

                    //=====================================================

                    string sql3 = "SELECT DISTINCT [Name],[IDGenre] FROM [dbo].[Genre] ORDER BY  IDGenre ";

                    SqlDataAdapter dat3 = new SqlDataAdapter(sql3, con);
                    DataTable dt3 = new DataTable();
                    dat3.Fill(dt3);
                    //dataGridView1.DataSource = dt3;
                    comboBox1.DataSource = dt3;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "IDGenre";


                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Genre();
            form.ShowDialog();
        }
    }
}
