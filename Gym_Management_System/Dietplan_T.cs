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
    public partial class Dietplan_T : Form
    {
        public Dietplan_T()
        {
            InitializeComponent();
            LoadData();
        }
        static int counter = 0;
        static int backcounter = 0;

        private void LoadData()
        {
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "select * from Meal";
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
        private void button3_Click(object sender, EventArgs e)
        {
            if (backcounter == 0) {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();
                MessageBox.Show("The inserted purpose,type and no of meals are deleted");

                string query2 = "Delete FROM DietPlan_T WHERE dietid = (SELECT MAX(dietid) FROM DietPlan_T);";
                SqlCommand cmd2 = new SqlCommand(query2, conn);

                cmd2.ExecuteNonQuery();
                cmd2.Dispose();


            }




            this.Hide();
            TrainerInterDP Form = new TrainerInterDP();
            Form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            backcounter = 1;
            //add
            if (counter >= gb.Tnoofmealz)
            {
                MessageBox.Show("You have already entered the total number of meals specified");
                MessageBox.Show("the previous meals are added to the diet plan");

                this.Hide();
                Trainer mainPage = new Trainer();
                mainPage.ShowDialog();


            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();




                    string meid = textBox1.Text;



                    if (!string.IsNullOrEmpty(meid))
                    {
                        string query = "SELECT COUNT(*) FROM Meal WHERE mealid = @mealid";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@mealid", meid);

                        int count = (int)cmd.ExecuteScalar();

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        if (count > 0)
                        {
                            MessageBox.Show("Meal Added");



                            string query3 = "Insert into Have_T values ('" + meid + "', '" + gb.Tdietplanid + "' )";
                            SqlCommand cmd3 = new SqlCommand(query3, conn);
                            cmd3.ExecuteNonQuery();
                            cmd3.Dispose();

                            counter++;
                            this.Hide();
                            Dietplan_T mainPage = new Dietplan_T();
                            mainPage.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("Invalid Meal ID");
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
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            addmeal Form = new addmeal();
            Form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (counter >= gb.Tnoofmealz)
            {
                MessageBox.Show("You have already entered the total number of meals specified");
                MessageBox.Show("the previous meals are added to the diet plan");

                this.Hide();
                Trainer mainPage = new Trainer();
                mainPage.ShowDialog();


            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();




                    string meid = textBox1.Text;



                    if (!string.IsNullOrEmpty(meid))
                    {
                        string query = "SELECT COUNT(*) FROM Meal WHERE mealid = @mealid";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@mealid", meid);

                        int count = (int)cmd.ExecuteScalar();

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        if (count > 0)
                        {
                            MessageBox.Show("Meal Added");



                            string query3 = "Insert into Have_T values ('" + meid + "', '" + gb.Tdietplanid + "' )";
                            SqlCommand cmd3 = new SqlCommand(query3, conn);
                            cmd3.ExecuteNonQuery();
                            cmd3.Dispose();

                            counter++;
                            if (counter < gb.Tnoofmealz)
                            {
                                MessageBox.Show("You have not entered the total number of meals specified yet");
                                MessageBox.Show("you must add more meals");

                                this.Hide();
                                Dietplan_T mainPage = new Dietplan_T();
                                mainPage.ShowDialog();



                            }
                            this.Hide();
                            MessageBox.Show("Diet Plan Created");
                            Trainer form = new Trainer();
                            form.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("Invalid Meal ID");
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

        }

        private void Dietplan_T_Load(object sender, EventArgs e)
        {

        }
    }
}
