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
    public partial class ReportProjects_T : Form
    {
        public ReportProjects_T()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e) //back button
        {
            this.Hide();
            ProjectReports form= new ProjectReports();
            form.ShowDialog();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //rep one
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = " SELECT T.username, T.Fname, T.Lname, TG.gymid, G.gym_name, T.specialty_areas, T.experience, T.qualifications\r\nFROM Trainer T JOIN TrainerGym TG ON T.username = TG.trainer_username\r\nJOIN Gym G ON TG.gymid = G.gymid\r\nwhere T.status='accepted';\r\n";
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

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //rep 2
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT T.username AS trainer_username, M.username AS member_username, S.sessionid, S.type, S.date, S.time, S.duration    \r\nFROM Trainer T JOIN SessionTable S ON T.username = S.trainer_username\r\nJOIN MemberTable M ON S.member_username = M.username\r\nwhere S.status='booked'\r\n ";
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
            //rep 3
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT T.username, T.Fname, T.Lname, AVG(F.stars) AS avg_rating\r\nFROM Trainer T JOIN Feedback F ON T.username = F.trainer_username\r\nGROUP BY T.username, T.Fname, T.Lname;\r\n ";
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
            //rep 4
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT T.username, T.Fname, T.Lname, COUNT(S.sessionid) AS session_count\r\nFROM Trainer T JOIN SessionTable S ON T.username = S.trainer_username\r\nGROUP BY T.Fname, T.Lname, T.username\r\nOrder by session_count DESC;\r\n ";
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
            //rep 5
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = " SELECT T.username, T.Fname, T.Lname, T.specialty_areas, COUNT(DISTINCT S.member_username) AS member_count\r\nFROM Trainer T JOIN SessionTable S ON T.username = S.trainer_username\r\nGROUP BY T.username, T.Fname, T.Lname, T.specialty_areas\r\nOrder by member_count DESC;\r\n";
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
            //rep 6
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = " SELECT T.username, T.Fname, T.Lname, MAX(S.duration) AS max_session_duration, MIN(S.duration) AS min_session_duration\r\nFROM Trainer T JOIN SessionTable S ON T.username = S.trainer_username\r\nwhere T.status='accepted'\r\nGROUP BY T.username, T.Fname, T.Lname\r\norder by max_session_duration, min_session_duration;\r\n";
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
            ProjectReports_GA form= new ProjectReports_GA();
            form.ShowDialog();
        }
    }
}
