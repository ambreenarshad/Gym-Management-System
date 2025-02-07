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
    public partial class Sessions : Form
    {
        public Sessions()
        {
            InitializeComponent();
            LoadData();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  //backbutton
        {
            this.Hide();

            TrainerSessions Page = new TrainerSessions();
            Page.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)  //accept button
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string sid = textBox1.Text;
                int id = int.Parse(sid);

                if (!string.IsNullOrEmpty(sid))
                {
                    string query = "update SessionTable set status = 'booked' where sessionid = @id and '" + gb.T_un + "' = trainer_username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Session accepted successfully!");

                        this.Hide();
                        Sessions sessionsform = new Sessions();
                        sessionsform.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Session id not found!");
                    }
                }
                else
                {
                    MessageBox.Show("Session id cannot be empty!");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e) //session id
        {

        }

        private void button2_Click(object sender, EventArgs e) //reject button
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string sid = textBox1.Text;
                int id = int.Parse(sid);

                if (!string.IsNullOrEmpty(sid))
                {
                    string query = "delete from SessionTable where sessionid = @id and '" + gb.T_un + "' = trainer_username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Session rejected!");

                        this.Hide();
                        Sessions sessionsform = new Sessions();
                        sessionsform.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Session id not found!");
                    }
                }
                else
                {
                    MessageBox.Show("Session id cannot be empty!");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT * FROM SessionTable where '" + gb.T_un + "' = trainer_username and status = 'pending'";
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

        private void NewSessions_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
