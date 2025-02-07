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
    public partial class ownersignup : Form
    {
        public ownersignup()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            loginowner owner = new loginowner();
            owner.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                gb.g_un = textBox2.Text;
                gb.g_pass = textBox8.Text;
                gb.g_fname = textBox3.Text;
                gb.g_lname = textBox7.Text;
                gb.g_email = textBox5.Text;
                gb.g_dob = textBox4.Text;
                gb.g_gender = comboBox1.Text;
                gb.g_gym_name = textBox1.Text;
                if (gb.g_pass != textBox9.Text)
                {
                    MessageBox.Show("Passwords do not match. Please re-enter your password.");
                    return; // Exit the button click event if passwords don't match
                }
                if (!string.IsNullOrEmpty(gb.g_un) && !string.IsNullOrEmpty(gb.g_pass) && !string.IsNullOrEmpty(gb.g_lname) && !string.IsNullOrEmpty(gb.g_dob) && !string.IsNullOrEmpty(gb.g_gym_name) && !string.IsNullOrEmpty(gb.g_fname) && !string.IsNullOrEmpty(gb.g_email) && !string.IsNullOrEmpty(gb.g_gender) && !string.IsNullOrEmpty(gb.g_gym_name))
                {

                    string query = "SELECT COUNT(*) FROM GymOwner WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", gb.g_un);

                    int count = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (count == 0)
                    {

                        MessageBox.Show("Owner request sent to admin");
                        string query2 = "Insert into GymOwner values ('" + gb.g_un + "','" + gb.g_pass + "', '" + gb.g_fname + "', '" + gb.g_lname + "', '" + gb.g_dob + "', '" + gb.g_gender + "', '" + gb.g_email + "','pending' )";
                        string query3 = "Insert into Gym values ('" + gb.g_gym_name + "','" + gb.g_un + "', '" + gb.a_un + "','pending' )";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.ExecuteNonQuery();
                        cmd2.Dispose();

                        this.Hide();

                        Main_Page mainPage = new Main_Page();
                        mainPage.ShowDialog();
                    }

                    else
                    {
                        MessageBox.Show("Invalid owner's Username");
                    }
                }
                else
                {

                    MessageBox.Show("No attribute can be left empty");
                }

                conn.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }




        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

       
        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

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

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
         
        private void ownersignup_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            Main_Page signinForm = new Main_Page();
            signinForm.ShowDialog();
        }
    }
}
