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
    public partial class RescheduleSessions : Form
    {
        public RescheduleSessions()
        {
            InitializeComponent();
            LoadData();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)  //sesson id
        {

        }

        private void button3_Click(object sender, EventArgs e)   //reschedule button
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string date = maskedTextBox1.Text;
                string stime = comboBox2.Text;
                string sdur = comboBox3.Text;
                string sid = textBox3.Text;
                int time = int.Parse(stime);
                int dur = int.Parse(sdur);
                int id = int.Parse(sid);

                if (!string.IsNullOrEmpty(sid))
                {
                    string query = "update SessionTable set date = @date, time = @time, duration = @dur where sessionid = @id and '" + gb.T_un + "' = trainer_username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@dur", dur);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Session rescheduled successfully!");

                        this.Hide();
                        RescheduleSessions form = new RescheduleSessions();
                        form.ShowDialog();
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

        private void button1_Click(object sender, EventArgs e)  //back button
        {
            this.Hide();
            TrainerSessions form = new TrainerSessions();
            form.ShowDialog();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)  //time
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)  //duration
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) //date
        {

        }
        private void LoadData()
        {
            try
            {
                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT * FROM SessionTable where '" + gb.T_un + "' = trainer_username and status = 'booked'";
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

        private void RescheduleSessions_Load(object sender, EventArgs e)
        {

        }
    }
}
