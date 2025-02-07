using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace User_Interface
{
    public partial class Feedback : Form
    {
        public Feedback()
        {
            InitializeComponent();
            LoadData();
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
                  
                    string query = "SELECT s.trainer_username, t.Fname, t.qualifications FROM SessionTable s JOIN Trainer t ON s.trainer_username = t.username WHERE s.member_username = @member_username AND s.status = 'booked'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@member_username", gb.mun);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string tun = textBox1.Text;
                string ra = comboBox1.Text;
               

                if (!string.IsNullOrEmpty(tun) && !string.IsNullOrEmpty(ra))
                {

                    string query = "SELECT COUNT(*) FROM SessionTable s JOIN Trainer t ON s.trainer_username = t.username WHERE s.member_username = @member_username AND s.status = 'booked' and trainer_username=@trainer_username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@member_username", gb.mun);
                    cmd.Parameters.AddWithValue("@trainer_username", tun);
                    int count = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (count > 0)
                    {


                        MessageBox.Show("Thanks for the feedback");
                        string queryCheckFeedback = "SELECT COUNT(*) FROM feedback WHERE member_username = @member_username AND trainer_username = @trainer_username";
                        SqlCommand cmdCheckFeedback = new SqlCommand(queryCheckFeedback, conn);
                        cmdCheckFeedback.Parameters.AddWithValue("@member_username", gb.mun);
                        cmdCheckFeedback.Parameters.AddWithValue("@trainer_username", tun);
                        int feedbackCount = (int)cmdCheckFeedback.ExecuteScalar();

                        if (feedbackCount > 0)
                        {
                            // Feedback exists, update the existing feedback
                            string updateQuery = "UPDATE feedback SET stars = @rating WHERE member_username = @member_username AND trainer_username = @trainer_username";
                            SqlCommand cmdUpdateFeedback = new SqlCommand(updateQuery, conn);
                            cmdUpdateFeedback.Parameters.AddWithValue("@rating", ra);
                            cmdUpdateFeedback.Parameters.AddWithValue("@member_username", gb.mun);
                            cmdUpdateFeedback.Parameters.AddWithValue("@trainer_username", tun);
                            cmdUpdateFeedback.ExecuteNonQuery();
                            cmdUpdateFeedback.Dispose();
                        }
                        else
                        {
                            // Feedback does not exist, insert a new entry
                            string insertQuery = "INSERT INTO feedback (stars, member_username, trainer_username) VALUES (@rating, @member_username, @trainer_username)";
                            SqlCommand cmdInsertFeedback = new SqlCommand(insertQuery, conn);
                            cmdInsertFeedback.Parameters.AddWithValue("@rating", ra);
                            cmdInsertFeedback.Parameters.AddWithValue("@member_username", gb.mun);
                            cmdInsertFeedback.Parameters.AddWithValue("@trainer_username", tun);
                            cmdInsertFeedback.ExecuteNonQuery();
                            cmdInsertFeedback.Dispose();
                        }
                        //string query2 = "Insert into feedback values ('" + ra + "','" + gb.mun + "', '" + tun + "')";
                        //SqlCommand cmd2 = new SqlCommand(query2, conn);
                        //cmd2.ExecuteNonQuery();
                        //cmd2.Dispose();

                        this.Hide();

                        mainpage form = new mainpage();
                        form.ShowDialog();
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
    }
}
