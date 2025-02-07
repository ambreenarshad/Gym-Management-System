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
    public partial class Trainer_Feedback : Form
    {
        public Trainer_Feedback()
        {
            InitializeComponent();
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            Trainer mainPage = new Trainer();
            mainPage.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                gb.T_gym = textBox1.Text;
                gb.T_gymid = int.Parse(gb.T_gym);

                if (!string.IsNullOrEmpty(gb.T_gym))
                {

                    string query = "SELECT count(*) FROM feedback f JOIN TrainerGym t ON f.trainer_username = t.trainer_username " +
                                "WHERE status = 'accepted' AND t.gymid = @gymid AND f.trainer_username = @username";

                    SqlCommand cmd2 = new SqlCommand(query, conn);
                    cmd2.Parameters.AddWithValue("@gymid", gb.T_gymid);
                    cmd2.Parameters.AddWithValue("@username", gb.T_un);

                    int count = (int)cmd2.ExecuteScalar();
                    if (count > 0)
                    {
                        this.Hide();

                        TrainerGymFeedback form = new TrainerGymFeedback();
                        form.ShowDialog();
                    }

                    else
                        MessageBox.Show("Feedback not found!");
                    cmd2.Dispose();
                }
                else
                {
                    MessageBox.Show("Enter gym id please!");
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
                    string query = "SELECT f.trainer_username, g.gym_name, t.gymid, avg(f.stars) as AverageRating FROM feedback f JOIN TrainerGym t " +
                            "ON f.trainer_username = t.trainer_username JOIN Gym g ON t.gymid = g.gymid WHERE t.status = 'accepted' AND '" + gb.T_un + "' = t.trainer_username " +
                            "GROUP BY f.trainer_username, g.gym_name, t.gymid";
                    //SqlCommand cmd2 = new SqlCommand(query, conn);
                    //SqlDataReader read = cmd2.ExecuteReader();
                    //if (read.HasRows)
                    //{
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0]; // Bind the DataGridView to the DataTable
                    //}

                    //else
                    //    MessageBox.Show("Feedback not found!");
                    //cmd2.Dispose();
                    //read.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
       }
}
