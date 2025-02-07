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
    public partial class revokemember : Form
    {
        public revokemember()
        {
            InitializeComponent();
            LoadData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Back to Welcome Screen
            this.Hide();
            welcome owner = new welcome();
            owner.ShowDialog();
        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                    string query = "SELECT username, status FROM MemberTable where status in('accepted', 'revoke') and gymid='" + gb.g_id + "'";
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
                        string checkStatusQuery = "SELECT status FROM MemberTable WHERE username = @username and gymid='" + gb.g_id + "' and status in('accepted', 'revoke')";
                        SqlCommand statusCmd = new SqlCommand(checkStatusQuery, conn);
                        statusCmd.Parameters.AddWithValue("@username", username);
                        var currentStatus = statusCmd.ExecuteScalar()?.ToString();

                        if (currentStatus != null)
                        {
                            if (currentStatus != newStatus)
                            {
                                string updateQuery = "UPDATE MemberTable SET status = @newStatus WHERE username = @username";
                                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                                cmd.Parameters.AddWithValue("@username", username);
                                cmd.Parameters.AddWithValue("@newStatus", newStatus);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show($"Member status updated to {newStatus} successfully!");
                                    LoadData(); // Reload the data to reflect the change
                                }
                                else
                                {
                                    MessageBox.Show("No changes were made to the trainer status.");
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Member is already {newStatus}.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Memberusername not found!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Memberusername cannot be empty!");
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
            UpdateTrainerStatus("revoke");
        }

        private void revokemember_Load(object sender, EventArgs e)
        {

        }
    }
}
