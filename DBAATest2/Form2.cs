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
using System.Configuration;

namespace DBAATest2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string conString = "Data Source=DESKTOP-VTO56LQ;Initial Catalog=DBAA;Integrated Security=True";



        
        public void cmbload()
        {
            /*SqlConnection con = new SqlConnection("Data Source=DESKTOP-VTO56LQ;Initial Catalog=DBAA;Integrated Security=True");
            con.Open();
            //string sqlquery = "Select * From [dbo].[Author]";
            string sql3 = "SELECT DISTINCT [LastName],[IDAuthor] FROM [dbo].[Author] ORDER BY  IDAuthor ";

            SqlDataAdapter dat3 = new SqlDataAdapter(sql3, con);
            System.Data.DataTable dt3 = new System.Data.DataTable();
            dat3.Fill(dt3);
            comboBox1.DataSource = dt3;
            comboBox1.DisplayMember = "LastName";
            comboBox1.ValueMember = "IDAuthor";
            con.Close();
            comboBox1.Text = "";

            if (comboBox1.SelectedValue.ToString() != null)
            {
                string sqlquery = "SELECT DISTINCT [Name],[IDGenre] FROM [dbo].[Genre] ORDER BY IDGenre";
                //SqlCommand sqlcom = new SqlCommand(sqlquery, con);
                SqlDataAdapter sda = new SqlDataAdapter(sqlquery, con);
                //sqlcom.Parameters.AddWithValue("@IDAuthor", comboBox1.SelectedValue.ToString());
                DataTable dt = new DataTable();
                sda.Fill(dt);
                comboBox2.ValueMember = "IDGenre";
                comboBox2.DisplayMember = "Name";
                comboBox2.DataSource = dt;
            }*/
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-VTO56LQ;Initial Catalog=DBAA;Integrated Security=True");
            con.Open();
            //string sqlquery = "Select * From [dbo].[Author]";
            string sql3 = "SELECT DISTINCT [LastName],[IDAuthor] FROM [dbo].[Author] ";

            if (comboBox2.Text != "")
            {
                sql3 += $"WHERE Genre = {comboBox2.SelectedValue}";
            }

            SqlDataAdapter dat3 = new SqlDataAdapter(sql3, con);
            DataTable dt3 = new DataTable();
            dat3.Fill(dt3);
            //dataGridView1.DataSource = dt3;
            comboBox1.DataSource = dt3;
            comboBox1.DisplayMember = "LastName";
            comboBox1.ValueMember = "IDAuthor";
            con.Close();
            comboBox1.Text = "";

            

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = @"Select Book.[Name], [LastName], Genre.[Name], [NameEditors], [City], [Adress], [Type] From Book
                                  join Author on Author.IDAuthor = Book.Author
                                  join Genre on Genre.IDGenre = Book.Genre
                                  right Join Editions on Editions.NumBook = Book.IDBook
                                  join Editors on Editors.IDEditors = Editions.NumEditor
                                  join [Format] on Editions.NumFormat = [Format].IDFormat";



                    SqlCommand cmd = new SqlCommand(q, con);
                    /*SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;*/


                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]);
                        
                    }

                    reader.Close();

                    /*comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";*/

                                        
                    cmbload();



                    string sql3 = "SELECT DISTINCT [Name],[IDGenre] FROM [dbo].[Genre] ORDER BY  IDGenre ";

                    SqlDataAdapter dat3 = new SqlDataAdapter(sql3, con);
                    DataTable dt3 = new DataTable();
                    dat3.Fill(dt3);
                    //dataGridView1.DataSource = dt3;
                    comboBox2.DataSource = dt3;
                    comboBox2.DisplayMember = "Name";
                    comboBox2.ValueMember = "IDGenre";

                }

            }

            comboBox2.SelectedIndex = -1;
            comboBox2.SelectedIndexChanged += delegate (object obj, EventArgs ya)
            {
                cmbload();

            };

            
        }

        

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = @"Select Book.[Name], [LastName], Genre.[Name], [NameEditors], [City], [Adress], [Type] From Book
                                  join Author on Author.IDAuthor = Book.Author
                                  join Genre on Genre.IDGenre = Book.Genre
                                  right Join Editions on Editions.NumBook = Book.IDBook
                                  join Editors on Editors.IDEditors = Editions.NumEditor
                                  join [Format] on Editions.NumFormat = [Format].IDFormat WHERE Book.IDBook > 0 ";

                    if (comboBox1.SelectedIndex != -1) {
                        q = q + $"and Book.Author = {comboBox1.SelectedValue} ";
                    }

                    if (comboBox2.SelectedIndex != -1)
                    {
                        q = q + $"and Book.Genre = {comboBox2.SelectedValue} ";
                    }

                    SqlCommand cmd = new SqlCommand(q, con);
                    


                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]);

                    }


                    /*comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";*/


                    


                }

            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbload();
        }

        private void btnSbros_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    string q = @"Select Book.[Name], [LastName], Genre.[Name], [NameEditors], [City], [Adress], [Type] From Book
                                  join Author on Author.IDAuthor = Book.Author
                                  join Genre on Genre.IDGenre = Book.Genre
                                  right Join Editions on Editions.NumBook = Book.IDBook
                                  join Editors on Editors.IDEditors = Editions.NumEditor
                                  join [Format] on Editions.NumFormat = [Format].IDFormat";

                    SqlCommand cmd = new SqlCommand(q, con);
                    
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(dataGridView1.Rows.Count + 1, reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]);

                    }


                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";


                    cmbload();


                }

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var app = new Microsoft.Office.Interop.Excel.Application
            {
                DisplayAlerts = false
            };
            string path = "template.xlsx";
            var workbook = app.Workbooks.Open(System.IO.Path.
              Combine(Application.StartupPath, path));
            var worksheet = workbook.ActiveSheet as Microsoft.Office.
              Interop.Excel.Worksheet;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; ++j)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1[j, i].Value.ToString();
                }
            }
            app.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

