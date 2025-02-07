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
    public partial class loginowner : Form
    {
        public loginowner()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;";
            gb.g_un = textBox1.Text.Trim();
            gb.g_pass = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(gb.g_un) || string.IsNullOrEmpty(gb.g_pass))
            {
                MessageBox.Show("Username or password cannot be empty");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Updated query to retrieve gymid along with verifying the gym owner
                    string query = @" SELECT Gym.gymid FROM GymOwner INNER JOIN Gym ON Gym.go_username = GymOwner.username 
                        WHERE GymOwner.username = @username AND GymOwner.password = @password AND GymOwner.status = 'accepted';";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", gb.g_un);
                        cmd.Parameters.AddWithValue("@password", gb.g_pass);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            this.Hide(); // Hide the login form
                            gb.g_id = (int)result; // Get the gym ID
                            welcome mainPage = new welcome(); // Pass gymId to the main page
                            mainPage.ShowDialog(); // Show the welcome form as a modal dialog
                            this.Close(); // Close the login form after the modal dialog closes
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password or gym not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ownersignup owner = new ownersignup();
            owner.ShowDialog();

        }
        
        private void loginowner_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();

            Main_Page signinForm = new Main_Page();
            signinForm.ShowDialog();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
