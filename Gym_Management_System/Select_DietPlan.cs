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
    public partial class Select_DietPlan : Form
    {
        public Select_DietPlan()
        {
            InitializeComponent();
            if (gb.mchoosen == 1) { LoadData_o(); }
            else if (gb.mchoosen == 2) { LoadData_t(); }
            else if (gb.mchoosen == 3) { LoadData_oth(); }
        }

        private void LoadData_o()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                    string query = " select d.dietid, d.purpose, d.noofmeals, d.type,d.member_username as Creator , m.name, m.proteins * 4 + m.carbs * 4 +  m.fats * 9 as Calories from DietPlan_M d join Have_M h on h.dietid = d.dietid join Meal m on m.mealid = h.mealid  where d.member_username = @member_username order by dietid";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@member_username", gb.mun);

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        var ds = new DataSet();
                        sda.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0]; // Bind the DataGridView to the DataTable
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void LoadData_t()
        {
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "select d.dietid, d.purpose, d.noofmeals, d.type,d.trainer_username as Creator , m.name, m.proteins * 4 + m.carbs * 4 +  m.fats * 9 as Calories from DietPlan_T d join Have_T h on h.dietid = d.dietid join Meal m on m.mealid = h.mealid order by dietid";
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
        private void LoadData_oth()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                    string query = "select d.dietid, d.purpose, d.noofmeals, d.type,d.member_username as Creator , m.name, m.proteins * 4 + m.carbs * 4 +  m.fats * 9 as Calories from DietPlan_M d join Have_M h on h.dietid = d.dietid join Meal m on m.mealid = h.mealid  where d.member_username != @member_username order by dietid";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@member_username", gb.mun);

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        var ds = new DataSet();
                        sda.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0]; // Bind the DataGridView to the DataTable
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadData1(string a)
        {
            string query = "";
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                    if (gb.mchoosen == 1) { query = " select d.dietid, d.purpose, d.noofmeals, d.type ,d.member_username as Creator, m.name, m.proteins * 4 + m.carbs * 4 +  m.fats * 9 as Calories from DietPlan_M d join Have_M h on h.dietid = d.dietid join Meal m on m.mealid = h.mealid where d.purpose = @purpose and d.member_username = @member_username order by dietid"; }
                    else if (gb.mchoosen == 2) { query = " select d.dietid, d.purpose, d.noofmeals, d.type ,d.trainer_username as Creator, m.name, m.proteins * 4 + m.carbs * 4 +  m.fats * 9 as Calories from DietPlan_T d join Have_T h on h.dietid = d.dietid join Meal m on m.mealid = h.mealid where d.purpose = @purpose order by dietid "; }
                    else if (gb.mchoosen == 3) { query = " select d.dietid, d.purpose, d.noofmeals, d.type ,d.member_username as Creator, m.name, m.proteins * 4 + m.carbs * 4 +  m.fats * 9 as Calories from DietPlan_M d join Have_M h on h.dietid = d.dietid join Meal m on m.mealid = h.mealid  where d.purpose = @purpose and d.member_username != @member_username order by dietid"; }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@purpose", a);
                        cmd.Parameters.AddWithValue("@member_username", gb.mun);


                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        var ds = new DataSet();
                        sda.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0]; // Bind the DataGridView to the DataTable
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadData2(string a)
        {
            string query = "";
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                    if (gb.mchoosen == 1)
                    {
                        query = "select d.dietid, d.purpose, d.noofmeals, d.type ,d.member_username as Creator, m.name, m.proteins * 4 + m.carbs * 4 +  m.fats * 9 as Calories from DietPlan_M d join Have_M h on h.dietid = d.dietid join Meal m on m.mealid = h.mealid where d.type = @type and d.member_username = @member_username order by dietid";
                    }
                    else if (gb.mchoosen == 2)
                    {
                        query = "select d.dietid, d.purpose, d.noofmeals, d.type ,d.trainer_username as Creator, m.name, m.proteins * 4 + m.carbs * 4 +  m.fats * 9 as Calories from DietPlan_T d join Have_T h on h.dietid = d.dietid join Meal m on m.mealid = h.mealid where d.type = @type order by dietid";
                    }
                    else if (gb.mchoosen == 3)
                    {
                        query = "select d.dietid, d.purpose, d.noofmeals, d.type ,d.member_username as Creator, m.name, m.proteins * 4 + m.carbs * 4 +  m.fats * 9 as Calories from DietPlan_M d join Have_M h on h.dietid = d.dietid join Meal m on m.mealid = h.mealid where d.type = @type and d.member_username != @member_username order by dietid";
                    }



                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@type", a);
                        cmd.Parameters.AddWithValue("@member_username", gb.mun);

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        var ds = new DataSet();
                        sda.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0]; // Bind the DataGridView to the DataTable
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void LoadData3(string a)
        {
            string query = "";
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                    if (gb.mchoosen == 1 || gb.mchoosen == 3)
                    {
                        query = "select d.dietid, d.purpose, d.noofmeals, d.type ,d.member_username as Creator, m.name, m.proteins * 4 + m.carbs * 4 +  m.fats * 9 as Calories from DietPlan_M d join Have_M h on h.dietid = d.dietid join Meal m on m.mealid = h.mealid where d.member_username = @member_username order by dietid";
                    }
                    else if (gb.mchoosen == 2)
                    {
                        query = "select d.dietid, d.purpose, d.noofmeals, d.type ,d.trainer_username as Creator, m.name, m.proteins * 4 + m.carbs * 4 +  m.fats * 9 as Calories from DietPlan_T d join Have_T h on h.dietid = d.dietid join Meal m on m.mealid = h.mealid where d.trainer_username = @trainer_username order by dietid";
                    }



                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@trainer_username", a);
                        cmd.Parameters.AddWithValue("@member_username", a);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        var ds = new DataSet();
                        sda.Fill(ds);
                        dataGridView1.DataSource = ds.Tables[0]; // Bind the DataGridView to the DataTable
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            //back
            this.Hide();

            interselectDP Form = new interselectDP();
            Form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //select
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();


                string di = textBox1.Text;

                string query = "";
                string query3 = "";


                if (!string.IsNullOrEmpty(di))
                {
                    if (gb.mchoosen == 1 || gb.mchoosen == 3) { query = "SELECT COUNT(*) FROM DietPlan_M WHERE dietid = @dietid"; }
                    else if (gb.mchoosen == 2) { query = "SELECT COUNT(*) FROM DietPlan_T WHERE dietid = @dietid"; }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@dietid", di);

                    int count = (int)cmd.ExecuteScalar();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (count > 0)
                    {



                        MessageBox.Show("Dietplan  Selected");
                        if (gb.mchoosen == 1 || gb.mchoosen == 3) { query3 = "Insert into SelectTable_M values ('" + gb.mun + "', '" + di + "' )"; }
                        else if (gb.mchoosen == 2) { query3 = "Insert into SelectTable_T values ('" + gb.mun + "', '" + di + "' )"; }

                        SqlCommand cmd3 = new SqlCommand(query3, conn);
                        cmd3.ExecuteNonQuery();
                        cmd3.Dispose();
                        this.Hide();
                        mainpage mainPage = new mainpage();
                        mainPage.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Diet ID");
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

            //mainpage Form = new mainpage();
            //Form.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //ok purpose 
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();
                string g = comboBox4.Text; //goal

                if (!string.IsNullOrEmpty(g))
                {
                    LoadData1(g);

                }
                else
                {
                    MessageBox.Show("Can't be left empty");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void Select_DietPlan_Load(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //purpose
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //type
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //oktype
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();
                string g = comboBox3.Text; //goal

                if (!string.IsNullOrEmpty(g))
                {
                    LoadData2(g);

                }
                else
                {
                    MessageBox.Show("Can't be left empty");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //creator
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //okcreator
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();
                string g = textBox2.Text; //goal

                if (!string.IsNullOrEmpty(g))
                {
                    LoadData3(g);

                }
                else
                {
                    MessageBox.Show("Can't be left empty");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //dietplan id
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //reset
            if (gb.mchoosen == 1) { LoadData_o(); }
            else if (gb.mchoosen == 2) { LoadData_t(); }
            else if (gb.mchoosen == 3) { LoadData_oth(); }
        }
    }
}
