using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Interface
{
    public partial class Create_Workout : Form
    {
        public Create_Workout()
        {
            InitializeComponent();
            LoadData();
        }
        static int counter = 1;
        static int backcounter = 0;
        static int workoutid = 0;
        private void Create_Workout_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadData()
        {
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "select e.exid, e.name as ExerciseName , m.name as MachineName, e.muscle_target from Exercise e join Machine m on e.macid = m.macid";
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
        private void button1_Click(object sender, EventArgs e)
        {
            //add
            backcounter = 1;
            try
            {
                
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                //string goal = comboBox1.Text;
                //string exp = comboBox2.Text;
               
                
                string exi = textBox1.Text;
                int exi_s = int.Parse(exi);
                string s = textBox3.Text;
                int s_s = int.Parse(s);
                string r = textBox4.Text;
                int r_s = int.Parse(r);
                string ri = textBox5.Text;
                int ri_s = int.Parse(ri);
                string day = comboBox1.Text;


                if (/*!string.IsNullOrEmpty(goal) && !string.IsNullOrEmpty(exp) &&*/ !string.IsNullOrEmpty(exi) && !string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(r) && !string.IsNullOrEmpty(ri) && !string.IsNullOrEmpty(day))
                {
                    string query = "SELECT COUNT(*) FROM Exercise WHERE exid = @exid";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@exid", exi);

                    int count = (int)cmd.ExecuteScalar();
                   
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (count > 0)
                    {
                        MessageBox.Show("Exercise Added");
                        


                        string query3 = "Insert into ContainTable_M values ('" + gb.workoutid + "', '" + exi + "',  '" + s_s + "', '" + r_s + "', '" + ri_s + "', '" + day + "' )";
                        SqlCommand cmd3 = new SqlCommand(query3, conn);
                        cmd3.ExecuteNonQuery();
                        cmd3.Dispose();
                        this.Hide();
                        Create_Workout mainPage = new Create_Workout();
                        mainPage.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Invalid Exercise ID");
                    }
                }
                else
                {
                    MessageBox.Show("No attributes can be left empty");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //done
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                //string goal = comboBox1.Text;
                //string exp = comboBox2.Text;


                string exi = textBox1.Text;
                int exi_s = int.Parse(exi);
                string s = textBox3.Text;
                int s_s = int.Parse(s);
                string r = textBox4.Text;
                int r_s = int.Parse(r);
                string ri = textBox5.Text;
                int ri_s = int.Parse(ri);
                string day = comboBox1.Text;

                if (!string.IsNullOrEmpty(exi) && !string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(r) && !string.IsNullOrEmpty(ri) && !string.IsNullOrEmpty(day))
                {
                    string query = "SELECT COUNT(*) FROM Exercise WHERE exid = @exid";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@exid", exi);

                    int count = (int)cmd.ExecuteScalar();
                   // int workoutid2 = 0;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (count > 0)
                    {
                        MessageBox.Show("Exercise Added");
                       


                        string query3 = "Insert into ContainTable_M values ('" + gb.workoutid + "', '" + exi + "',  '" + s_s + "', '" + r_s + "', '" + ri_s + "', '" + day + "' )";
                        SqlCommand cmd3 = new SqlCommand(query3, conn);
                        cmd3.ExecuteNonQuery();
                        cmd3.Dispose();

                        this.Hide();
                        MessageBox.Show("Workout Plan Created");
                        mainpage mainPage = new mainpage();
                        mainPage.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Exercise ID");
                    }
                }
                else
                {
                    MessageBox.Show("No attributes can be left empty");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //RestInterval
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //Reps
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //Sets
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Exercise
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Experience
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Goal
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

       

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (backcounter == 0) {

                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();
                MessageBox.Show("The inserted goal and experience level is deleted");

                string query2 = "Delete FROM WorkoutPlan_M WHERE wpid = (SELECT MAX(wpid) FROM WorkOutPlan_M);";
                SqlCommand cmd2 = new SqlCommand(query2, conn);

                cmd2.ExecuteNonQuery();
                cmd2.Dispose();

            }



            this.Hide();

            intermediate_WO Form = new intermediate_WO();
            Form.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            addexercise Form = new addexercise();
            Form.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
