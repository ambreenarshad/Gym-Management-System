using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Interface
{
    public partial class addmeal : Form
    {
        public addmeal()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (gb.check == 1)
            {
                this.Hide();

                Dietplan Form = new Dietplan();
                Form.ShowDialog();

            }
            else
            {
                this.Hide();

                Dietplan_T Form = new Dietplan_T();
                Form.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //add

            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();


                string mealname = comboBox1.Text;
                string p = textBox2.Text;
                string c = textBox3.Text;
                string fa= textBox4.Text;
                string fib = textBox5.Text;
                string pa = textBox6.Text;




                if (!string.IsNullOrEmpty(mealname) && !string.IsNullOrEmpty(p) && !string.IsNullOrEmpty(c) && !string.IsNullOrEmpty(fa) && !string.IsNullOrEmpty(fib) && !string.IsNullOrEmpty(pa))
                {
                    
                        MessageBox.Show("Meal Added");
                        string query3 = "Insert into Meal values ('" + mealname + "', '" + p + "',  '" + c + "',  '" + fib + "',  '" + fa + "',  '" + pa + "' )";
                        SqlCommand cmd3 = new SqlCommand(query3, conn);
                        cmd3.ExecuteNonQuery();
                        cmd3.Dispose();
                        if (gb.check == 1)
                        {
                            this.Hide();

                            Dietplan Form = new Dietplan();
                            Form.ShowDialog();

                        }
                        else
                        {
                            this.Hide();

                            Dietplan_T Form = new Dietplan_T();
                            Form.ShowDialog();
                        }

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

            //this.Hide();

            //Dietplan Form = new Dietplan();
            //Form.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //mealname
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //fibers
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //protein
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //carbs
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //fats
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //potential allergies
        }

        private void addmeal_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
