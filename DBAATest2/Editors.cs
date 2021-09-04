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
    public partial class Editors : Form
    {
        public Editors()
        {
            InitializeComponent();
        }
        public string conString = "Data Source=DESKTOP-VTO56LQ;Initial Catalog=DBAA;Integrated Security=True";


        private void Editors_Load(object sender, EventArgs e)
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

                }
            }
        }

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
                    string q = "INSERT INTO Editors(NameEditors, City, Adress)values('" + txtName.Text.ToString() + "', '" + txtCity.Text.ToString() + "', '" + txtAdres.Text.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Was added successfuly!");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "DELETE FROM Editors WHERE IDEditors = @ID";
                    string type = ((DataRowView)lstbox.SelectedItem).Row[0].ToString();
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(type));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Was deleted successfuly!");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = "UPDATE Editors set NameEditors=@NameEditors, City=@City, Adress=@Adress WHERE IDEditors = @ID";
                    string type = ((DataRowView)lstbox.SelectedItem).Row[0].ToString();
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(type));
                    cmd.Parameters.AddWithValue("@NameEditors", txtName.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@Adress", txtAdres.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Was updated successfuly!");
                }
            }
        }
    }
}
