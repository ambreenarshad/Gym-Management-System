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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace User_Interface
{
    public partial class intermediate_WO : Form
    {
        public intermediate_WO()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            


            this.Hide();

            mainpage Form = new mainpage();
            Form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //add to insert in the workout table and store the wpid in a global variable

           

            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string goal = comboBox1.Text;
                string exp = comboBox2.Text;


               


                if (!string.IsNullOrEmpty(goal) && !string.IsNullOrEmpty(exp) )
                {
                   
                      
                       
                            string query2 = "Insert into WorkoutPlan_M values ('" + goal + "','" + exp + "', '" + gb.mun + "');SELECT SCOPE_IDENTITY();";
                            SqlCommand cmd2 = new SqlCommand(query2, conn);

                            gb.workoutid = Convert.ToInt32(cmd2.ExecuteScalar());

                          //  cmd2.ExecuteNonQuery();
                            cmd2.Dispose();
                           

                       

                        this.Hide();
                        Create_Workout mainPage = new Create_Workout();
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //exp
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //goal
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void intermediate_WO_Load(object sender, EventArgs e)
        {

        }
    }
}
