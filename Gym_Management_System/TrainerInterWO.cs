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
    public partial class TrainerInterWO : Form
    {
        public TrainerInterWO()
        {
            InitializeComponent();
        }

        private void TrainerInterWO_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string goal = comboBox1.Text;
                string exp = comboBox2.Text;





                if (!string.IsNullOrEmpty(goal) && !string.IsNullOrEmpty(exp))
                {



                    string query2 = "Insert into WorkoutPlan_T values ('" + goal + "','" + exp + "', '" + gb.T_un + "');SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);

                    gb.Tworkoutid = Convert.ToInt32(cmd2.ExecuteScalar());

                    //  cmd2.ExecuteNonQuery();
                    cmd2.Dispose();




                    this.Hide();
                    Create_Workout_T mainPage = new Create_Workout_T();
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

        private void button3_Click(object sender, EventArgs e)
        {
            //back button
            this.Hide();

            Trainer Form = new Trainer();
            Form.ShowDialog();
        }
    }
}
