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
    public partial class intermediateDP : Form
    {
        public intermediateDP()
        {
            InitializeComponent();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            mainpage Form = new mainpage();
            Form.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //purpose
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //type
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //noofmeals
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // next
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string purp = comboBox1.Text;
                string ty = comboBox2.Text;
                string n = textBox7.Text;
                int nom = int.Parse(n);


                gb.noofmealz = nom;


                if (!string.IsNullOrEmpty(purp) && !string.IsNullOrEmpty(ty) && !string.IsNullOrEmpty(n))
                {

                    

                    string query2 = "Insert into DietPlan_M values ('" + purp + "','" + nom + "','" + ty + "', '" + gb.mun + "');SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);

                    gb.dietplanid = Convert.ToInt32(cmd2.ExecuteScalar());

                    //  cmd2.ExecuteNonQuery();
                    cmd2.Dispose();




                    this.Hide();
                    Dietplan mainPage = new Dietplan();
                    mainPage.ShowDialog();

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

        private void intermediateDP_Load(object sender, EventArgs e)
        {

        }
    }
}
