using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Interface
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                    string query = "SELECT tg.trainer_username, tg.status FROM Trainer t join TrainerGym tg " +
                        "on t.username=tg.trainer_username where tg.status in('accepted', 'revoke') and tg.gymid='" + gb.g_id + "'";
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

        private void UpdateTrainerStatus(string newStatus)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                    string username = textBox1.Text.Trim();

                    if (!string.IsNullOrEmpty(username))
                    {
                        // Check the current status first
                        string checkStatusQuery = "SELECT tg.status FROM Trainer t join TrainerGym tg " +
                        "on t.username=tg.trainer_username where tg.status in('accepted', 'revoke') and tg.gymid='" + gb.g_id + "' and t.username = @username";
                       // string checkStatusQuery = "SELECT status FROM Trainer WHERE username = @username";
                        SqlCommand statusCmd = new SqlCommand(checkStatusQuery, conn);
                        statusCmd.Parameters.AddWithValue("@username", username);
                        var currentStatus = statusCmd.ExecuteScalar()?.ToString();

                        if (currentStatus != null)
                        {
                            if (currentStatus != newStatus)
                            {
                                string updateQuery = "UPDATE TrainerGym SET status = @newStatus WHERE trainer_username = @username and gymid = '" + gb.g_id + "'";
                                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                                cmd.Parameters.AddWithValue("@username", username);
                                cmd.Parameters.AddWithValue("@newStatus", newStatus);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show($"Trainer status updated to {newStatus} successfully!");
                                    LoadData(); // Reload the data to reflect the change

                                    if (newStatus == "accepted")
                                    {
                                        string query = "UPDATE Trainer SET status = @newStatus WHERE trainer_username = @tusername";
                                        SqlCommand cmd1 = new SqlCommand(query, conn);
                                        cmd1.Parameters.AddWithValue("@tusername", username);
                                    }
                                    else if (newStatus == "revoke")
                                    {
                                        string query2 = "select count(*) from TrainerGym where status='accepted' and trainer_username=@tusername";
                                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                                        cmd2.Parameters.AddWithValue("@tusername", username);
                                        int rowsAffected1 = cmd2.ExecuteNonQuery();
                                        if (rowsAffected1 == 1)
                                        {
                                            string query = "UPDATE Trainer SET status = @newStatus WHERE trainer_username = @ttusername";
                                            SqlCommand cmd1 = new SqlCommand(query, conn);
                                            cmd1.Parameters.AddWithValue("@ttusername", username);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No changes were made to the trainer status.");
                                }   
                                
                            }
                            else
                            {
                                MessageBox.Show($"Trainer is already {newStatus}.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Trainer ID not found!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Trainer ID cannot be empty!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Accept Button
            UpdateTrainerStatus("accepted");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Reject Button
            UpdateTrainerStatus("revoke");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Back to Welcome Screen
            this.Hide();
            welcome owner = new welcome();
            owner.ShowDialog();
        }

        private void Add_Load(object sender, EventArgs e)
        {

        }
    }
}
