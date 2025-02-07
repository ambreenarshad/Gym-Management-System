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
    public partial class Select_Workout : Form
    {
        public Select_Workout()
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
                    string query = "select w.wpid, w.goal, w.experiencelevel, w.member_username as Creator, e.name, e.muscle_target, c.sets, c.reps, c.restintervals from WorkOutPlan_M w join ContainTable_M c on c.wpid = w.wpid join Exercise e on e.exid = c.exid where w.member_username = @member_username";
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
                    string query = "select w.wpid , w.goal, w.experiencelevel, w.trainer_username as Creator, e.name,e.muscle_target, c.sets,c.reps,c.restintervals  from WorkOutPlan_T w join ContainTable_T c on c.wpid = w.wpid join Exercise e on e.exid = c.exid;";
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
                    string query = "select w.wpid, w.goal, w.experiencelevel, w.member_username as Creator, e.name, e.muscle_target, c.sets, c.reps, c.restintervals from WorkOutPlan_M w join ContainTable_M c on c.wpid = w.wpid join Exercise e on e.exid = c.exid where w.member_username != @member_username";
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

        //goal
        private void LoadData1(string a)
        {
            string query="";
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();
                    if (gb.mchoosen == 1 ) { query = "select w.wpid, w.goal, w.experiencelevel, w.member_username as Creator, e.name, e.muscle_target, c.sets, c.reps, c.restintervals from WorkOutPlan_M w join ContainTable_M c on c.wpid = w.wpid join Exercise e on e.exid = c.exid where w.goal = @goal and w.member_username = @member_username"; }
                    else if (gb.mchoosen == 2) { query = "select w.wpid , w.goal, w.experiencelevel, w.trainer_username as Creator, e.name,e.muscle_target, c.sets,c.reps,c.restintervals  from WorkOutPlan_T w join ContainTable_T c on c.wpid = w.wpid join Exercise e on e.exid = c.exid where w.goal = @goal "; }
                    else if (gb.mchoosen == 3) { query = "select w.wpid, w.goal, w.experiencelevel, w.member_username as Creator, e.name, e.muscle_target, c.sets, c.reps, c.restintervals from WorkOutPlan_M w join ContainTable_M c on c.wpid = w.wpid join Exercise e on e.exid = c.exid where w.goal = @goal and w.member_username != @member_username"; }
                     
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@goal", a);
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
                        query = "select w.wpid, w.goal, w.experiencelevel, w.member_username as Creator, e.name, e.muscle_target, c.sets, c.reps, c.restintervals from WorkOutPlan_M w join ContainTable_M c on c.wpid = w.wpid join Exercise e on e.exid = c.exid where w.experiencelevel = @experiencelevel and w.member_username = @member_username";
                    }
                    else if (gb.mchoosen == 2)
                    {
                        query = "select w.wpid , w.goal, w.experiencelevel, w.trainer_username as Creator, e.name,e.muscle_target, c.sets,c.reps,c.restintervals  from WorkOutPlan_T w join ContainTable_T c on c.wpid = w.wpid join Exercise e on e.exid = c.exid where w.experiencelevel = @experiencelevel";
                    }
                    else if (gb.mchoosen == 3) {
                        query = "select w.wpid, w.goal, w.experiencelevel, w.member_username as Creator, e.name, e.muscle_target, c.sets, c.reps, c.restintervals from WorkOutPlan_M w join ContainTable_M c on c.wpid = w.wpid join Exercise e on e.exid = c.exid where w.experiencelevel = @experiencelevel and w.member_username != @member_username";
                    }


                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@experiencelevel", a);
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
                        query = "select w.wpid, w.goal, w.experiencelevel, w.member_username as Creator, e.name, e.muscle_target, c.sets, c.reps, c.restintervals from WorkOutPlan_M w join ContainTable_M c on c.wpid = w.wpid join Exercise e on e.exid = c.exid where w.member_username = @member_username";
                    }
                    else if (gb.mchoosen == 2)
                    {
                        query = "select w.wpid , w.goal, w.experiencelevel, w.trainer_username as Creator, e.name,e.muscle_target, c.sets,c.reps,c.restintervals  from WorkOutPlan_T w join ContainTable_T c on c.wpid = w.wpid join Exercise e on e.exid = c.exid where w.trainer_username = @trainer_username";
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


        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            interselectWO Form = new interselectWO();
            Form.ShowDialog();
        }


        private void Select_Workout_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //okexplev
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();
                string ex = comboBox2.Text; //goal

                if (!string.IsNullOrEmpty(ex))
                {
                    LoadData2(ex);

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

        private void button2_Click_1(object sender, EventArgs e)
        {
            //okgoal
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();
                string g = comboBox1.Text; //goal

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //creator
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //experience
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //goal
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //okcreator username
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();
                string cr = textBox2.Text; //goal

                if (!string.IsNullOrEmpty(cr))
                {
                    LoadData3(cr);

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //workout iD
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();


                string wi = textBox1.Text;

                string query = "";
                string query3 = "";


                if (!string.IsNullOrEmpty(wi))
                {
                    if (gb.mchoosen == 1 || gb.mchoosen == 3) { query = "SELECT COUNT(*) FROM WorkOutPlan_M WHERE wpid = @wpid"; }
                    else if (gb.mchoosen == 2) { query = "SELECT COUNT(*) FROM WorkOutPlan_T WHERE wpid = @wpid"; }
                     
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@wpid", wi);

                    int count = (int)cmd.ExecuteScalar();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (count > 0)
                    {
                        

                        MessageBox.Show("Workout  Selected");
                        if (gb.mchoosen == 1 || gb.mchoosen == 3) { query3 = "Insert into Explore_M values ('" + gb.mun + "', '" + wi + "' )"; }
                        else if (gb.mchoosen == 2) { query3 = "Insert into Explore_T values ('" + gb.mun + "', '" + wi + "' )"; }
                        
                        SqlCommand cmd3 = new SqlCommand(query3, conn);
                        cmd3.ExecuteNonQuery();
                        cmd3.Dispose();
                        this.Hide();
                        mainpage mainPage = new mainpage();
                        mainPage.ShowDialog();
                    }
                    else {
                        MessageBox.Show("Invalid workout ID");
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
            //select
            //this.Hide();

            //mainpage Form = new mainpage();
            //Form.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (gb.mchoosen == 1) { LoadData_o(); }
            else if (gb.mchoosen == 2) { LoadData_t(); }
            else if (gb.mchoosen == 3) { LoadData_oth(); }
        }
    }
}
