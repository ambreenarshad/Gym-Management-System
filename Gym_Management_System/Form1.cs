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
    

    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            Register RegisterForm = new Register();
            RegisterForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

          

            //this.Hide();

            //mainpage mainpageForm = new mainpage();
            //mainpageForm.ShowDialog();

            try
            {
                gb.check = 1;
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                 gb.mun = textBox1.Text;
                 gb.mpass = textBox2.Text;

                if (!string.IsNullOrEmpty(gb.mun) && !string.IsNullOrEmpty(gb.mpass))
                {
                    string query = "SELECT COUNT(*) FROM MemberTable WHERE username = @username AND password = @password AND status = 'accepted'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", gb.mun);
                    cmd.Parameters.AddWithValue("@password", gb.mpass);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {

                        this.Hide();
                        mainpage mainPage = new mainpage();
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

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_Page mainPage = new Main_Page();
            mainPage.ShowDialog();
        }
    }
}
