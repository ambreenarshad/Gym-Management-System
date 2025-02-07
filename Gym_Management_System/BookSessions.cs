using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace User_Interface
{
    public partial class BookSessions : Form
    {
        public BookSessions()
        {
            InitializeComponent();
            LoadData();
        }

        private void BookSessions_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
          
               
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainpage Form = new mainpage();
            Form.ShowDialog();
        }
        private void LoadData()
        {
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT username, Fname, qualifications,experience FROM Trainer where status = 'accepted'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0]; // Bind the DataGridView to the DataTable
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
             try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string date = maskedTextBox1.Text;
                string time = comboBox1.Text;
                string dur = comboBox3.Text;
                string type = comboBox2.Text;
                string tun = textBox3.Text;

                if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(time)&&!string.IsNullOrEmpty(dur)&& !string.IsNullOrEmpty(type))
                {
                    string query = "SELECT COUNT(*) FROM Trainer WHERE username = @username and status='accepted'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", tun);

                    int count = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (count > 0)
                    {

                        MessageBox.Show("Booking request sent");
                        string query2 = "Insert into SessionTable values ('" + date + "','" + time + "', '" + dur + "', '" + type + "', '" + gb.mun + "', '" + tun + "', 'pending')";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.ExecuteNonQuery();
                        cmd2.Dispose();

                        this.Hide();

                        mainpage mainPage = new mainpage();
                        mainPage.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Trainer's Username");
                    }
                }
                else
                {

                    MessageBox.Show("No attribute can be left empty");
                }

                conn.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_3(object sender, EventArgs e)
        {

        }
    }
}
