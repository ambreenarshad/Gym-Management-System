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
    public partial class trainerSignup : Form
    {
        public trainerSignup()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)   //firstname
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)   //lastname
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)  //gender
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)  //email
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)  //username
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)  //password
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)  //re-enter password
        {

        }

        private void button1_Click(object sender, EventArgs e)  //next button
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                gb.T_fname = textBox1.Text;
                gb.T_lname = textBox2.Text;
                gb.T_email = textBox3.Text;
                gb.T_un = textBox4.Text;
                gb.T_pass = textBox5.Text;
                string Rpass = textBox6.Text;
                gb.T_gender = comboBox2.Text;
                gb.T_experience = textBox7.Text;
                gb.T_qualification = comboBox3.Text;
                gb.T_specialityarea = comboBox1.Text;
                gb.T_dob = maskedTextBox1.Text;

                if (!string.IsNullOrEmpty(gb.T_un) && !string.IsNullOrEmpty(gb.T_pass) && !string.IsNullOrEmpty(gb.T_fname) && !string.IsNullOrEmpty(gb.T_lname) &&
                    !string.IsNullOrEmpty(gb.T_email) && !string.IsNullOrEmpty(gb.T_gender) && !string.IsNullOrEmpty(gb.T_gender) && !string.IsNullOrEmpty(Rpass) &&
                    !string.IsNullOrEmpty(gb.T_qualification) && !string.IsNullOrEmpty(gb.T_specialityarea) && !string.IsNullOrEmpty(gb.T_experience) && !string.IsNullOrEmpty(gb.T_dob))
                {
                    string query = "SELECT COUNT(*) FROM Trainer WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", gb.T_un);
                    int count = (int)cmd.ExecuteScalar();

                    string query2 = "SELECT COUNT(*) FROM Trainer WHERE email = @email";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    cmd2.Parameters.AddWithValue("@email", gb.T_email);
                    int count2 = (int)cmd2.ExecuteScalar();

                    if (count > 0 || count2 > 0 || gb.T_pass != Rpass)
                    {
                        if (count > 0)
                            MessageBox.Show("Username already exists. Please enter a unique username.");
                        if (count2 > 0)
                            MessageBox.Show("Email Account is already in use. Please sign up with some other account");
                        if (gb.T_pass != Rpass)
                            MessageBox.Show("Passwords don't match.");
                    }
                    else
                    {
                        this.Hide();
            
                        TrainerGymRegistrationForm mainPage = new TrainerGymRegistrationForm();
                        mainPage.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Every attribute must be filled.");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)  //login link
        {
            this.Hide();

            TrainerLogin signinForm = new TrainerLogin();
            signinForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) //back button
        {
            this.Hide();

            Main_Page signinForm = new Main_Page();
            signinForm.ShowDialog();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)  //date
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)  //qualifications
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)  //speciality areas
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e) //experience
        {

        }

        private void trainerSignup_Load(object sender, EventArgs e)
        {

        }


    }
}
