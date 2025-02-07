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
    public partial class adminlogin : Form
    {
        public adminlogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                gb.a_un = textBox1.Text;
                gb.a_pass = textBox2.Text;

                if (!string.IsNullOrEmpty(gb.a_un) && !string.IsNullOrEmpty(gb.a_pass))
                {
                    string query = "SELECT COUNT(*) FROM AdminTable WHERE username = @username AND password = @password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", gb.a_un);
                    cmd.Parameters.AddWithValue("@password", gb.a_pass);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        this.Hide();

                        adminmain mainPage = new adminmain();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_Page form = new Main_Page();
            form.ShowDialog();
        }
    }
}
