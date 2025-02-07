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
    public partial class RevokeMembership : Form
    {
        public RevokeMembership()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            string connectionString = "Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT gym_name, gymid FROM Gym WHERE status = 'accepted'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string gym_name = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(gym_name))
            {
                MessageBox.Show("Gym name cannot be empty!");
                return;
            }

            string connectionString = "Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Update the Gym status to 'revoke'
                        string gymUpdateQuery = "UPDATE Gym SET status = 'revoke' WHERE gym_name = @gym_name;";
                        SqlCommand gymCmd = new SqlCommand(gymUpdateQuery, conn, transaction);
                        gymCmd.Parameters.AddWithValue("@gym_name", gym_name);
                        int rowsAffected = gymCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Retrieve gymid for further revocation
                            string gymIdQuery = "SELECT gymid FROM Gym WHERE gym_name = @gym_name";
                            SqlCommand gymIdCmd = new SqlCommand(gymIdQuery, conn, transaction);
                            gymIdCmd.Parameters.AddWithValue("@gym_name", gym_name);
                            object gymIdResult = gymIdCmd.ExecuteScalar();

                            if (gymIdResult != null)
                            {
                                int gymId = (int)gymIdResult;

                                // Revoke the GymOwner
                                string ownerUpdateQuery = "UPDATE GymOwner SET status = 'revoke' WHERE username = (SELECT go_username FROM Gym WHERE gymid = @gymid)";
                                SqlCommand ownerCmd = new SqlCommand(ownerUpdateQuery, conn, transaction);
                                ownerCmd.Parameters.AddWithValue("@gymid", gymId);
                                ownerCmd.ExecuteNonQuery();

                                // Revoke trainers associated with this gym
                                string trainerRevokeQuery = "UPDATE TrainerGym SET status = 'revoke' WHERE gymid = @gymid";
                                SqlCommand trainerCmd = new SqlCommand(trainerRevokeQuery, conn, transaction);
                                trainerCmd.Parameters.AddWithValue("@gymid", gymId);
                                trainerCmd.ExecuteNonQuery();

                                // Revoke members associated with this gym
                                string memberRevokeQuery = "UPDATE MemberTable SET status = 'revoke' WHERE gymid = @gymid";
                                SqlCommand memberCmd = new SqlCommand(memberRevokeQuery, conn, transaction);
                                memberCmd.Parameters.AddWithValue("@gymid", gymId);
                                memberCmd.ExecuteNonQuery();

                                // Commit the transaction
                                transaction.Commit();
                                MessageBox.Show("Gym, gym owner, trainers, and members revoked successfully!");
                            }
                            else
                            {
                                MessageBox.Show("Gym ID not found!");
                                transaction.Rollback();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Gym not found!");
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminmain owner = new adminmain();
            owner.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
