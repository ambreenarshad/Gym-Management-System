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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace User_Interface
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                gb.mfname = textBox1.Text;
                gb.mlname = textBox3.Text;
                gb.mdob = maskedTextBox1.Text;
                gb.mgender = comboBox1.Text;
                gb.memail = textBox4.Text;
                gb.musername = textBox5.Text;
                gb.mpassword = textBox6.Text;
                gb.membershiptype = comboBox2.Text;
                string dur = comboBox3.Text;
                gb.duration = int.Parse(dur);



                string reenter = textBox7.Text;


                if (!string.IsNullOrEmpty(gb.mfname) && !string.IsNullOrEmpty(gb.mlname) && !string.IsNullOrEmpty(gb.mdob) && !string.IsNullOrEmpty(gb.mgender) && !string.IsNullOrEmpty(gb.memail) && !string.IsNullOrEmpty(gb.musername) && !string.IsNullOrEmpty(gb.mpassword) && !string.IsNullOrEmpty(reenter) && !string.IsNullOrEmpty(gb.membershiptype) && !string.IsNullOrEmpty(dur))
                {
                    

                        string query = "SELECT COUNT(*) FROM MemberTable WHERE username = @username";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@username", gb.musername);


                        int count = (int)cmd.ExecuteScalar();

                        string query2 = "SELECT COUNT(*) FROM MemberTable WHERE email = @email";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);

                        cmd.Parameters.AddWithValue("@email", gb.memail);

                        int count2 = (int)cmd.ExecuteScalar();
                        if(count > 0 || count2 > 0 || gb.mpassword != reenter) {

                        if (count2 > 0)
                        {
                            MessageBox.Show("Email Account is already in use. Please sign up with some other account");

                        }
                        if (count > 0)
                        {

                            MessageBox.Show("Username already exists. Please enter a unique username");


                        }
                        if (gb.mpassword != reenter)
                        {

                            MessageBox.Show("Passwords don't match");

                        }
                        }
                       

                        else
                            {
                                this.Hide();
                                MemberGymRegistrationForm mainPage = new MemberGymRegistrationForm();
                                mainPage.ShowDialog();
                            }

                   
                }
                else
                {
                    MessageBox.Show("Every Attribute must be filled");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

           Form1  Form = new Form1();
            Form.ShowDialog();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //firstname
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //last name
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            //date
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //email
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //username
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //password
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //renter_password
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_Page form = new Main_Page();
            form.ShowDialog();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
