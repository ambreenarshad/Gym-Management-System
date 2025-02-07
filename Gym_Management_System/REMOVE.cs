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
    public partial class REMOVE : Form
    {
        public REMOVE()
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
                    string query = "SELECT username, status FROM MemberTable where status ='pending' and gymid='" + gb.g_id + "'";
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

        private void button1_Click(object sender, EventArgs e) //approve
        {
            string username = textBox1.Text;

            if (!string.IsNullOrEmpty(username))
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                    //SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        string query = "UPDATE MemberTable SET status = 'accepted' WHERE username = @username and gymid='" + gb.g_id + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@username", username);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            //transaction.Commit();
                            MessageBox.Show("Member accepted successfully!");
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Member not found!");
                            //transaction.Rollback();
                        }
                    }
                    catch (Exception ex)
                    {
                        //transaction.Rollback();
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Memberusername cannot be empty!");
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
                        string ownerQuery = "DELETE FROM MemberTable WHERE username = @username and gymid='" + gb.g_id + "'";
                        SqlCommand ownerCmd = new SqlCommand(ownerQuery, conn);
                        ownerCmd.Parameters.AddWithValue("@username", username);
                        ownerCmd.ExecuteNonQuery();

                        // transaction.Commit();
                        int rowsAffected = ownerCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            //transaction.Commit();
                            MessageBox.Show("Member rejected successfully!");
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Member not found!");
                            //transaction.Rollback();
                        }
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        MessageBox.Show("Error while updating database: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Memberusername cannot be empty!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            welcome owner = new welcome();
            owner.ShowDialog();
            owner.ShowDialog();
        }

        private void REMOVE_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
