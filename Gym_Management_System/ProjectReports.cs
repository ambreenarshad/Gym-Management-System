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
    public partial class ProjectReports : Form
    {
        public ProjectReports()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e) //back button
        {
            this.Hide();
            Main_Page form= new Main_Page();
            form.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //report1
            string gymid = textBox1.Text;
            int gid = int.Parse(gymid);
            string t_usern = textBox2.Text;
            string query = "";
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();


                    query = "SELECT m.username AS Member_Username,m.Fname AS Member_FirstName,m.Lname AS Member_LastName, m.dob AS Member_DateOfBirth, m.gender AS Member_Gender,m.email AS Member_Email, g.gym_name AS Gym_Name, t.username AS Trainer_Username FROM MemberTable m JOIN Gym g ON m.gymid = g.gymid JOIN TrainerGym tg ON m.gymid = tg.gymid JOIN Trainer t ON tg.trainer_username = t.username WHERE g.gymid = @gymid AND t.username = @username;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@gymid", gid);
                        cmd.Parameters.AddWithValue("@username", t_usern);
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //report2
            string gymid = textBox4.Text;
            int gid = int.Parse(gymid);
            string dpid = textBox3.Text;
            string query = "";
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();


                    query = "SELECT m.username AS Member_Username,m.Fname AS Member_FirstName, m.Lname AS Member_LastName,  m.dob AS Member_DateOfBirth, m.gender AS Member_Gender, m.email AS Member_Email, g.gym_name AS Gym_Name, dm.purpose AS DietPlan_Purpose, dm.noofmeals AS DietPlan_NumberOfMeals, dm.type AS DietPlan_Type FROM MemberTable m JOIN Gym g ON m.gymid = g.gymid JOIN SelectTable_M stm ON m.username = stm.member_username JOIN DietPlan_M dm ON stm.dietid = dm.dietid WHERE g.gymid = @gymid AND dm.dietid = @dietid;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@gymid", gid);
                        cmd.Parameters.AddWithValue("@dietid", dpid);
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

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //report3
            string dietid = textBox6.Text;
            int did = int.Parse(dietid);
            string t_user = textBox5.Text;
            string query = "";
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();


                    query = "SELECT m.username AS Member_Username,  m.Fname AS Member_FirstName, m.Lname AS Member_LastName, m.dob AS Member_DateOfBirth,  m.gender AS Member_Gender, m.email AS Member_Email, dm.purpose AS DietPlan_Purpose,dm.noofmeals AS DietPlan_NumberOfMeals, dm.type AS DietPlan_Type FROM MemberTable m JOIN SelectTable_T stm ON m.username = stm.member_username JOIN DietPlan_T dm ON stm.dietid = dm.dietid WHERE dm.trainer_username = @trainer_username AND dm.dietid = @dietid;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@trainer_username", t_user);
                        cmd.Parameters.AddWithValue("@dietid", did);
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

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //report4
            string gid = textBox9.Text;

            string day = textBox7.Text;
            string query = "";
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();


                    query = "SELECT Machine.name AS MachineName, COUNT(DISTINCT MemberTable.username) AS MembersUsingMachine FROM Gym JOIN MemberTable ON Gym.gymid = MemberTable.gymid JOIN WorkOutPlan_M ON MemberTable.username = WorkOutPlan_M.member_username JOIN ContainTable_M ON WorkOutPlan_M.wpid = ContainTable_M.wpid JOIN Exercise ON ContainTable_M.exid = Exercise.exid JOIN Machine ON Exercise.macid = Machine.macid  WHERE Gym.gymid = @gymid AND ContainTable_M.day = @day GROUP BY Machine.name;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@gymid", gid);
                        cmd.Parameters.AddWithValue("@day", day);
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

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //report5(member)
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT DietPlan_M.dietid, DietPlan_M.purpose, (Meal.proteins * 4 + Meal.carbs * 4 +  Meal.fats * 9) as Calories FROM DietPlan_M JOIN Have_M ON DietPlan_M.dietid = Have_M.dietid JOIN Meal ON Have_M.mealid = Meal.mealid WHERE Meal.name = 'Breakfast' AND (Meal.proteins * 4 + Meal.carbs * 4 +  Meal.fats * 9)  < 500";
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

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //report5(trainer)
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT DietPlan_T.dietid, DietPlan_T.purpose, (Meal.proteins * 4 + Meal.carbs * 4 +  Meal.fats * 9) as Calories FROM DietPlan_T JOIN Have_T ON DietPlan_T.dietid = Have_T.dietid JOIN Meal ON Have_T.mealid = Meal.mealid WHERE Meal.name = 'Breakfast' AND (Meal.proteins * 4 + Meal.carbs * 4 +  Meal.fats * 9)  < 500";
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

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //report6(member)
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT DietPlan_M.dietid, DietPlan_M.purpose, SUM(Meal.carbs) as totalCarbs FROM DietPlan_M JOIN Have_M ON DietPlan_M.dietid = Have_M.dietid JOIN Meal ON Have_M.mealid = Meal.mealid GROUP BY DietPlan_M.dietid, DietPlan_M.purpose HAVING SUM(Meal.carbs) < 300;";
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

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //report6(trainer)
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = " SELECT DietPlan_T.dietid, DietPlan_T.purpose, SUM(Meal.carbs) as totalCarbs FROM DietPlan_T JOIN Have_T ON DietPlan_T.dietid = Have_T.dietid JOIN Meal ON Have_T.mealid = Meal.mealid GROUP BY DietPlan_T.dietid, DietPlan_T.purpose HAVING SUM(Meal.carbs) < 300;";
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

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //report8 (members)
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT DISTINCT DietPlan_M.dietid, DietPlan_M.purpose FROM DietPlan_M JOIN Have_M ON DietPlan_M.dietid = Have_M.dietid JOIN Meal ON Have_M.mealid = Meal.mealid WHERE Meal.potential_allergies IS NULL OR Meal.potential_allergies != 'Peanuts'";
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

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //report8 (trainer)
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT DISTINCT DietPlan_T.dietid, DietPlan_T.purpose FROM DietPlan_T JOIN Have_T ON DietPlan_T.dietid = Have_T.dietid JOIN Meal ON Have_T.mealid = Meal.mealid WHERE Meal.potential_allergies IS NULL OR Meal.potential_allergies != 'Peanuts'";
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

        private void button1_Click(object sender, EventArgs e) //next button
        {
            this.Hide();
            ReportProjects_T form= new ReportProjects_T();
            form.ShowDialog();
        }

        private void ProjectReports_Load(object sender, EventArgs e)
        {

        }
    }
}
