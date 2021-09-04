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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string conString = "Data Source=DESKTOP-VTO56LQ;Initial Catalog=DBAA;Integrated Security=True";
        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "Select [IDFormat], [Type] from Format ";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    lstbox.DataSource = dt;
                    lstbox.DisplayMember = "Type";
                    lstbox.ValueMember = "IDFormat";
                    
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
                    string q = "INSERT INTO Format(Type)values('" + txtType.Text.ToString() + "')";
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
                        string q = "DELETE FROM Format WHERE IDFormat = @ID";
                        string type = ((DataRowView)lstbox.SelectedItem).Row[0].ToString();
                        SqlCommand cmd = new SqlCommand(q, con);
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(type));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Was deleted successfuly!");
                    }
                }
            }
            else
            {
                return;
            }
        }

        
        private void lstbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "UPDATE Format set Type=@Type WHERE IDFormat = @ID";
                    string type = ((DataRowView)lstbox.SelectedItem).Row[0].ToString();
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(type));
                    cmd.Parameters.AddWithValue("@Type", txtType.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Was updated successfuly!");
                }
            }

            }

        private void lstbox_SelectedValueChanged(object sender, EventArgs e)
        {
            string type = ((DataRowView)lstbox.SelectedItem).Row[1].ToString();
            txtType.Text = type;
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "Select [IDFormat], [Type] from Format ";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    lstbox.DataSource = dt;
                    lstbox.DisplayMember = "Type";
                    lstbox.ValueMember = "IDFormat";

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Editors();
            form.ShowDialog();
        }
    }
}
