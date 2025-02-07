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
    public partial class TrainerInterDP : Form
    {
        public TrainerInterDP()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //back button
            this.Hide();

            Trainer Form = new Trainer();
            Form.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //next
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string purp = comboBox1.Text;
                string ty = comboBox2.Text;
                string n = textBox7.Text;
                int nom = int.Parse(n);


                gb.Tnoofmealz = nom;


                if (!string.IsNullOrEmpty(purp) && !string.IsNullOrEmpty(ty) && !string.IsNullOrEmpty(n))
                {



                    string query2 = "Insert into DietPlan_T values ('" + purp + "','" + nom + "','" + ty + "', '" + gb.T_un + "');SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);

                    gb.Tdietplanid = Convert.ToInt32(cmd2.ExecuteScalar());

                    //  cmd2.ExecuteNonQuery();
                    cmd2.Dispose();




                    this.Hide();
                    Dietplan_T mainPage = new Dietplan_T();
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

        private void TrainerInterDP_Load(object sender, EventArgs e)
        {

        }
    }
}
