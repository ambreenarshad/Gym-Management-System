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

namespace User_Interface
{
    public partial class TrainerGymRegistrationForm : Form
    {
        static int counter = 1;
        public TrainerGymRegistrationForm()
        {
            InitializeComponent();
            LoadData();
        }
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)  //register button
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string id = textBox3.Text;
                int gymid = int.Parse(id);

                if (!string.IsNullOrEmpty(id))
                {
                    string query = "SELECT COUNT(*) FROM Gym WHERE gymid = @gymid";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@gymid", gymid);

                    int count = (int)cmd.ExecuteScalar();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (count > 0)
                    {
                        MessageBox.Show("Registration request sent");
                        if (counter == 1)
                        {
                            string query2 = "Insert into Trainer values ('" + gb.T_un + "','" + gb.T_pass + "', '" + gb.T_fname + "', '" + gb.T_lname + "', '" + gb.T_dob + "'," +
                                "'" + gb.T_gender + "', '" + gb.T_email + "', '" + gb.T_qualification + "', '" + gb.T_experience + "', '" + gb.T_specialityarea + "', 'pending')";
                            SqlCommand cmd2 = new SqlCommand(query2, conn);
                            cmd2.ExecuteNonQuery();
                            cmd2.Dispose();
                            counter++;
                        }
                        string query3 = "Insert into TrainerGym values ('" + gymid + "', '" + gb.T_un + "', 'pending')";
                        SqlCommand cmd3 = new SqlCommand(query3, conn);
                        cmd3.ExecuteNonQuery();
                        cmd3.Dispose();

                        this.Hide();
                        TrainerGymRegistrationForm mainPage = new TrainerGymRegistrationForm();
                        mainPage.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid gymid");
                    }
                }
                else
                {
                    MessageBox.Show("Gymid cannot be empty");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)  //back button
        {
            this.Hide();

            trainerSignup mainPage = new trainerSignup();
            mainPage.ShowDialog();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)  //gym id
        {

        }

        private void LoadData()
        {
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT gymid, gym_name FROM Gym";
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

        private void button2_Click(object sender, EventArgs e)  //done button
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string id = textBox3.Text;
                int gymid = int.Parse(id);

                if (!string.IsNullOrEmpty(id))
                {
                    string query = "SELECT COUNT(*) FROM Gym WHERE gymid = @gymid";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@gymid", gymid);

                    int count = (int)cmd.ExecuteScalar();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (count > 0)
                    {
                        MessageBox.Show("Registration request sent");
                        if (counter == 1)
                        {
                            string query2 = "Insert into Trainer values ('" + gb.T_un + "','" + gb.T_pass + "', '" + gb.T_fname + "', '" + gb.T_lname + "', '" + gb.T_dob + "'," +
                                "'" + gb.T_gender + "', '" + gb.T_email + "', '" + gb.T_qualification + "', '" + gb.T_experience + "', '" + gb.T_specialityarea + "', 'pending')";
                            SqlCommand cmd2 = new SqlCommand(query2, conn);
                            cmd2.ExecuteNonQuery();
                            cmd2.Dispose();
                            counter++;
                        }
                        string query3 = "Insert into TrainerGym values ('" + gymid + "', '" + gb.T_un + "', 'pending')";
                        SqlCommand cmd3 = new SqlCommand(query3, conn);
                        cmd3.ExecuteNonQuery();
                        cmd3.Dispose();

                        this.Hide();
                        Main_Page mainPage = new Main_Page();
                        mainPage.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid gymid");
                    }
                }
                else
                {
                    MessageBox.Show("Gymid cannot be empty");
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
