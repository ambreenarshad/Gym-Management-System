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
    public partial class TrainerLogin : Form
    {
        public TrainerLogin()
        {
            InitializeComponent();
        }

        private void TrainerLogin_Load(object sender, EventArgs e)
        {

        }
       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
          
            try
            {
                gb.check = 0;
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                gb.T_un = textBox1.Text;
                gb.T_pass = textBox2.Text;

                if (!string.IsNullOrEmpty(gb.T_un) && !string.IsNullOrEmpty(gb.T_pass))
                {
                    string query = "SELECT COUNT(*) FROM Trainer WHERE username = @username AND password = @password AND status = 'accepted'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", gb.T_un);
                    cmd.Parameters.AddWithValue("@password", gb.T_pass);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        this.Hide();

                        Trainer mainPage = new Trainer();
                        mainPage.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password");
                    }
                }
                else
                {
                    MessageBox.Show("Username or password cannot be empty");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            trainerSignup signUpForm = new trainerSignup();
            signUpForm.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            Main_Page Form = new Main_Page();
            Form.ShowDialog();
        }
    }
}
