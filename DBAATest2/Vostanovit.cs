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
    public partial class Vostanovit : Form
    {
        public Vostanovit()
        {
            InitializeComponent();
        }
        public string conString = "Data Source=DESKTOP-VTO56LQ;Initial Catalog=DBAA;Integrated Security=True";

        private void Vostanovit_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "Select [IDAuthor], [LastName] from Author WHERE isFired>=1 ";
                    SqlCommand cmd = new SqlCommand(q, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    lstbox.DataSource = dt;
                    lstbox.DisplayMember = "LastName";
                    lstbox.ValueMember = "IDAuthor";
                
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
                        string q = "DELETE FROM Author WHERE IDAuthor = @ID";
                        string type = ((DataRowView)lstbox.SelectedItem).Row[0].ToString();
                        SqlCommand cmd = new SqlCommand(q, con);
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(type));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Was deleted successfuly!");

                    }
                }
            }
            else {
                return;
            }
        }

        private void btnUnDelete_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Вы уверены что хотите востановить запись?", "Востановить", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        string q = "update Author set isFired=0 where IDAuthor=@ID";
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
    }
}
