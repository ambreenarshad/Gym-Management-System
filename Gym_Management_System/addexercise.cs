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
    public partial class addexercise : Form
    {
        public addexercise()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "select * from  Machine";
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

        private void button1_Click(object sender, EventArgs e)
        {
            //add
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

               
                string exercisename = textBox1.Text;
                string mt = textBox2.Text;
                string m = textBox3.Text;
                int mid = int.Parse(m);
               


                if (!string.IsNullOrEmpty(exercisename) && !string.IsNullOrEmpty(mt) && !string.IsNullOrEmpty(m))
                {
                    string query = "SELECT COUNT(*) FROM Exercise WHERE name = @name";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", exercisename);

                    int count2 = (int)cmd.ExecuteScalar();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (count2 > 0)
                    {
                        MessageBox.Show("An Exercise with this name already exists choose another name");

                        

                    }
                    else
                    {
                        string query2 = "SELECT COUNT(*) FROM Machine WHERE macid = @macid";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.Parameters.AddWithValue("@macid", m);

                        int count = (int)cmd.ExecuteScalar();

                        cmd2.ExecuteNonQuery();
                        cmd2.Dispose();
                        if (count > 0)
                        {
                            MessageBox.Show("Invalid Machine ID");

                        }
                        else
                        {

                            MessageBox.Show("Exercise Created");

                            string query3 = "Insert into Exercise values ('" + exercisename + "', '" + mt + "',  '" + mid + "' )";
                            SqlCommand cmd3 = new SqlCommand(query3, conn);
                            cmd3.ExecuteNonQuery();
                            cmd3.Dispose();
                            this.Hide();
                            if (gb.check == 0)
                            {
                                Create_Workout_T mainPage = new Create_Workout_T();
                                mainPage.ShowDialog();
                            }
                            else
                            {
                                Create_Workout mainPage = new Create_Workout();
                                mainPage.ShowDialog();
                            }
                            
                        }
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
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.Hide();
            if (gb.check == 0)
            {
                Create_Workout_T mainPage = new Create_Workout_T();
                mainPage.ShowDialog();
            }
            else
            {
                Create_Workout mainPage = new Create_Workout();
                mainPage.ShowDialog();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Exercise name
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //Muscle Target
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //machine id
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void addexercise_Load(object sender, EventArgs e)
        {

        }
    }
}
