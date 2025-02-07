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
    public partial class addtrainer : Form
    {
        public addtrainer()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
            {
                conn.Open();
                string query = "SELECT tg.trainer_username, tg.status FROM Trainer t join TrainerGym tg " +
                        "on t.username=tg.trainer_username where tg.status='pending' and tg.gymid='" + gb.g_id + "'";
                //string query = "SELECT username, status FROM Trainer WHERE status = 'pending'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button2_Click(object sender, EventArgs e) //approve
        {
            string username = textBox1.Text;

            if (!string.IsNullOrEmpty(username))
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                   // SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Check the current status first
                        string checkStatusQuery = "SELECT count(tg.trainer_username) FROM Trainer t join TrainerGym tg " +
                        "on t.username=tg.trainer_username where tg.status='pending' and tg.gymid='" + gb.g_id + "' and t.username = @username";
                        // string checkStatusQuery = "SELECT status FROM Trainer WHERE username = @username";
                        SqlCommand cmd = new SqlCommand(checkStatusQuery, conn);
                        cmd.Parameters.AddWithValue("@username", username);
                        int rowsAffected = (int)cmd.ExecuteScalar();
                        if (rowsAffected > 0)
                        {
                            string updateQuery2 = "UPDATE TrainerGym SET status = 'accepted' WHERE trainer_username = @tusername";
                            SqlCommand cmd2 = new SqlCommand(updateQuery2, conn);
                            cmd2.Parameters.AddWithValue("@tusername", username);
                            int count=cmd2.ExecuteNonQuery();
                            if (count > 0)
                            {
                                MessageBox.Show("Trainer accepted successfully!");
                                LoadData();
                                string updateQuery3 = "UPDATE Trainer SET status = 'accepted' WHERE username = @ttusername";
                                SqlCommand cmd3 = new SqlCommand(updateQuery3, conn);
                                cmd3.Parameters.AddWithValue("@ttusername", username);
                                int count2=cmd3.ExecuteNonQuery();
                                if(count2<=0)
                                    MessageBox.Show("No changes were made");
                            }
                            else
                                MessageBox.Show("No changes made");
                          //  transaction.Commit();
                            
                        }
                        else
                        {
                            MessageBox.Show("Trainer not found!");
                          //  transaction.Rollback();
                        }
                        
                        //string query = "UPDATE Trainer SET status = 'accepted' WHERE username = @username";
                        
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Trainer username cannot be empty!");
            }
        }

        private void button3_Click(object sender, EventArgs e) //reject
        {
            string username = textBox1.Text;

            if (!string.IsNullOrEmpty(username))
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                   // SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Check the current status first
                        string checkStatusQuery = "SELECT count(tg.trainer_username) FROM Trainer t join TrainerGym tg " +
                        "on t.username=tg.trainer_username where tg.status='pending' and tg.gymid='" + gb.g_id + "' and t.username = @username";
                        // string checkStatusQuery = "SELECT status FROM Trainer WHERE username = @username";
                        SqlCommand cmd = new SqlCommand(checkStatusQuery, conn);
                        cmd.Parameters.AddWithValue("@username", username);

                        int rowsAffected = (int)cmd.ExecuteScalar();
                        if (rowsAffected > 0)
                        {
                            string traQuery = "DELETE FROM TrainerGym WHERE trainer_username = @tusername and gymid='" + gb.g_id + "'";
                            SqlCommand gymCmd = new SqlCommand(traQuery, conn);
                            gymCmd.Parameters.AddWithValue("@tusername", username);
                            // gymCmd.ExecuteNonQuery();
                            int count=gymCmd.ExecuteNonQuery();
                            if (count>0)
                            {
                                MessageBox.Show("Trainer and associated records removed successfully!");
                                LoadData();
                            }
                            else
                                MessageBox.Show("No changes made");

                            string query2 = "select count(*) from TrainerGym where trainer_username=@ttusername";
                            SqlCommand cmd2 = new SqlCommand(query2, conn);
                            cmd2.Parameters.AddWithValue("@ttusername", username);
                            int rowsAffected1 = (int)cmd2.ExecuteScalar();
                            if (rowsAffected1 == 0)
                            {
                                string ownerQuery = "DELETE FROM Trainer WHERE username = @t_username";
                                SqlCommand ownerCmd = new SqlCommand(ownerQuery, conn);
                                ownerCmd.Parameters.AddWithValue("@t_username", username);
                                int count2 = ownerCmd.ExecuteNonQuery();
                                if (count2 <= 0)
                                    MessageBox.Show("No changes were made");
                            }
                           // ownerCmd.ExecuteNonQuery();
                            //transaction.Commit();
                        }
                        else
                            MessageBox.Show("Trainer username cannot be empty!");
                    }
                    catch (Exception ex)
                    {
                        //transaction.Rollback();
                        MessageBox.Show("Error while updating database: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Trainer username cannot be empty!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            welcome owner = new welcome();
            owner.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void addtrainer_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
