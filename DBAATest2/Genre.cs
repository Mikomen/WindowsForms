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
    public partial class Genre : Form
    {
        public Genre()
        {
            InitializeComponent();
        }
        public string conString = "Data Source=DESKTOP-VTO56LQ;Initial Catalog=DBAA;Integrated Security=True";
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "INSERT INTO Genre(Name)values('" + txtName.Text.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Was added successfuly!");
                }
            }
        }

        private void Genre_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "Select [IDGenre], [Name] from Genre ";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    lstbox.DataSource = dt;
                    lstbox.DisplayMember = "Name";
                    lstbox.ValueMember = "IDGenre";

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
                        string q = "DELETE FROM Editions WHERE NumBook in (Select IDBook From Book Where Genre = @ID )";
                        string type = ((DataRowView)lstbox.SelectedItem).Row[0].ToString();
                        SqlCommand cmd = new SqlCommand(q, con);
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(type));
                        cmd.ExecuteNonQuery();
                        q = "DELETE FROM Book WHERE Genre = @ID";
                        cmd.CommandText = q;
                        cmd.ExecuteNonQuery();
                        /*q = "DELETE FROM Author WHERE Genre = @ID";
                        cmd.CommandText = q;
                        cmd.ExecuteNonQuery();*/
                        q = "DELETE FROM Genre WHERE IDGenre = @ID";
                        cmd.CommandText = q;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Was deleted successfuly!");

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
                    string q = "UPDATE Genre set Name=@Name WHERE IDGenre = @ID";
                    string type = ((DataRowView)lstbox.SelectedItem).Row[0].ToString();
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(type));
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Was updated successfuly!");
                }
            }
        }
    }
}
