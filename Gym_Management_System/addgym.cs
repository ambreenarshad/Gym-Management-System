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
    public partial class addgym : Form
    {
        private void EnsureAdminExists()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
            {
                conn.Open();

                // Check if the predefined admin 'AHA' exists
                string checkAdmin = "SELECT COUNT(*) FROM AdminTable WHERE username = 'AHA'";
                SqlCommand checkCmd = new SqlCommand(checkAdmin, conn);
                int exists = (int)checkCmd.ExecuteScalar();

                if (exists == 0)
                {
                    // Insert the predefined admin 'AHA' if not exists
                    string insertAdmin = "INSERT INTO AdminTable (username, password) VALUES ('AHA', 'YourSecurePassword');";
                    SqlCommand insertCmd = new SqlCommand(insertAdmin, conn);
                    insertCmd.ExecuteNonQuery();
                }
            }
        }
        public addgym()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT username, status FROM GymOwner where status ='pending'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e) //approve
        {
            EnsureAdminExists();  // Ensure the admin exists before proceeding

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();

                    string username = textBox1.Text;

                    if (!string.IsNullOrEmpty(username))
                    {
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            // Approve the GymOwner
                            string query = "UPDATE GymOwner SET status = 'accepted' WHERE username = @username";
                            SqlCommand cmd = new SqlCommand(query, conn, transaction);
                            cmd.Parameters.AddWithValue("@username", username);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Insert gym details into the Gym table and set status to 'accepted'
                                string gymQuery = "INSERT INTO Gym (gym_name, go_username, admin_username, status) VALUES (@gym_name, @username, 'AHA', 'accepted');";
                                SqlCommand gymCmd = new SqlCommand(gymQuery, conn, transaction);
                                gymCmd.Parameters.AddWithValue("@gym_name", "Example Gym Name");
                                gymCmd.Parameters.AddWithValue("@username", username);

                                gymCmd.ExecuteNonQuery();

                                transaction.Commit();
                                MessageBox.Show("Gym owner approved and gym details set to accepted!");

                                this.Hide();
                                addgym sessionsform = new addgym();
                                sessionsform.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Owner id not found!");
                                transaction.Rollback();
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("An error occurred: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Owner id cannot be empty!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }




        private void button3_Click(object sender, EventArgs e) //reject
        {
            try
            {
                string username = textBox1.Text;

                if (!string.IsNullOrEmpty(username))
                {
                    using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                    {
                        conn.Open();

                        // Start a transaction
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            // Remove the GymOwner
                            string ownerQuery = "DELETE FROM GymOwner WHERE username = @username";
                            SqlCommand ownerCmd = new SqlCommand(ownerQuery, conn, transaction);
                            ownerCmd.Parameters.AddWithValue("@username", username);

                            int ownerAffected = ownerCmd.ExecuteNonQuery();

                            if (ownerAffected > 0)
                            {
                                // Delete associated gym records
                                string gymQuery = "DELETE FROM Gym WHERE go_username = @username";
                                SqlCommand gymCmd = new SqlCommand(gymQuery, conn, transaction);
                                gymCmd.Parameters.AddWithValue("@username", username);

                                gymCmd.ExecuteNonQuery();

                                // Optionally, handle trainers and members
                                string trainerQuery = "UPDATE TrainerGym SET status = 'inactive' WHERE gymid IN (SELECT gymid FROM Gym WHERE go_username = @username)";
                                SqlCommand trainerCmd = new SqlCommand(trainerQuery, conn, transaction);
                                trainerCmd.Parameters.AddWithValue("@username", username);
                                trainerCmd.ExecuteNonQuery();

                                // Commit the transaction
                                transaction.Commit();
                                MessageBox.Show("Gym owner and associated gym details removed successfully!");

                                // Refresh the form or update UI as necessary
                                this.Hide();
                                addgym sessionsform = new addgym();
                                sessionsform.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Owner id not found!");
                                transaction.Rollback();
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error while updating database: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Owner id cannot be empty!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            this.Hide();
            adminmain owner = new adminmain();
            owner.ShowDialog();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
