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
    public partial class MemberGymRegistrationForm : Form
    {
        public MemberGymRegistrationForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Register Form = new Register();
            Form.ShowDialog();
            // Add your button1 click event handling code here
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Add your button3 click event handling code here

           
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();
                string id = textBox3.Text;
                int gymid = int.Parse(id);


                if (!string.IsNullOrEmpty(id))
                {
                    string query = "SELECT COUNT(*) FROM Gym WHERE gymid = @gymid";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@gymid", gymid);

                    int count = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (count > 0)
                    {

                        MessageBox.Show("Registration request sent");
                        string query2 = "Insert into MemberTable values ('" + gb.musername + "','" + gb.mpassword + "', '" + gb.mfname + "', '" + gb.mlname + "', '" + gb.mdob + "'," +
                    "'" + gb.mgender + "', '" + gb.memail + "', '" + gymid + "', 'pending' , '" + gb.membershiptype + "' , '" + gb.duration + "')";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.ExecuteNonQuery();
                        cmd2.Dispose();

                        this.Hide();

                        Main_Page mainPage = new Main_Page();
                        mainPage.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid gymid");
                    }
                }
                else
                {

                    MessageBox.Show("Gymid cannot be cannot be empty");
                }

                conn.Close();



            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


        }

        private void label11_Click(object sender, EventArgs e)
        {
            // Add your label11 click event handling code here
        }

        private void MemberGymRegistrationForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadData() {
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT gymid, gym_name FROM Gym";
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
