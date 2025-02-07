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
    public partial class ProjectReports_GA : Form
    {
        public ProjectReports_GA()
        {
            InitializeComponent();
        }
        private void button7_Click(object sender, EventArgs e) //back button
        {
            this.Hide();
            ReportProjects_T form= new ReportProjects_T();
            form.ShowDialog();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //1
            string gid = textBox1.Text;

            string query = "";
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();


                    query = "SELECT m.username\r\nFROM MemberTable m\r\nINNER JOIN Gym g ON m.gymid = g.gymid\r\nWHERE g.gymid = @gymid;\r\n";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@gymid", gid);

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
            //2
            string gid = textBox4.Text;

            string query = "";
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();


                    query = "SELECT t.username, t.Fname, t.Lname, t.qualifications, t.experience, t.specialty_areas\r\nFROM Trainer t\r\nJOIN TrainerGym tg ON t.username = tg.trainer_username\r\nWHERE tg.gymid = @gymid;\r\n";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@gymid", gid);

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
            //3
            string gid = textBox9.Text;

            string query = "";
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
                {
                    conn.Open();


                    query = "SELECT \r\n    m.username, \r\n    m.Fname, \r\n    m.Lname\r\nFROM \r\n    MemberTable m\r\nWHERE \r\n    m.gymid = @gymid AND m.status = 'revoke'; \r\n";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameter to the command
                        cmd.Parameters.AddWithValue("@gymid", gid);

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
            //4
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = " SELECT g.gymid, g.gym_name, mt.username, mt.typeofmembership, mt.duration\r\nFROM Gym g\r\nJOIN MemberTable mt ON g.gymid = mt.gymid;\r\n";
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
            //5
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT go.username, go.Fname, go.Lname, COUNT(DISTINCT g.gymid) AS num_gyms, COUNT(t.username) AS total_trainers\r\nFROM GymOwner go\r\nJOIN Gym g ON go.username = g.go_username\r\nLEFT JOIN TrainerGym tg ON g.gymid = tg.gymid\r\nLEFT JOIN Trainer t ON tg.trainer_username = t.username\r\nGROUP BY go.username, go.Fname, go.Lname;\r\n";
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
            //6
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT \r\n    g.gym_name, \r\n    m.status, \r\n    COUNT(m.username) AS MemberCount\r\nFROM \r\n    Gym g\r\nJOIN \r\n    MemberTable m ON g.gymid = m.gymid\r\nGROUP BY \r\n    g.gym_name, m.status;\r\n";
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
            //7
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT \r\n    g.gym_name, \r\n    mt.typeofmembership, \r\n    COUNT(*) AS Count\r\nFROM \r\n    MemberTable mt\r\nJOIN \r\n    Gym g ON mt.gymid = g.gymid\r\nGROUP BY \r\n    g.gym_name, mt.typeofmembership\r\nORDER BY \r\n    g.gym_name, COUNT(*) DESC;\r\n";
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
    }
}
