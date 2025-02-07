using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Interface
{
    public partial class MemberReport : Form
    {
        public MemberReport()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
            {
                conn.Open();

                // Initialize base query
                string commandText = "SELECT username, typeofmembership, duration FROM MemberTable";
                List<string> conditions = new List<string>();

                // Check each field for input and add conditions accordingly
                if (!string.IsNullOrWhiteSpace(txtSearchUsername.Text))
                {
                    conditions.Add("username LIKE @username");
                }
                if (!string.IsNullOrWhiteSpace(txtSearchTypeOfMembership.Text))
                {
                    conditions.Add("typeofmembership LIKE @typeofmembership");
                }
                if (!string.IsNullOrWhiteSpace(txtSearchDuration.Text))
                {
                    conditions.Add("duration LIKE @duration");
                }

                //// If there are any conditions, append them to the commandText
                if (conditions.Any())
                {
                    commandText += " WHERE " + string.Join(" AND ", conditions) + " and gymid = '" + gb.g_id + "'";
                }

                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // Add parameters only if conditions are present
                    if (!string.IsNullOrWhiteSpace(txtSearchUsername.Text))
                    {
                        cmd.Parameters.AddWithValue("@username", $"%{txtSearchUsername.Text}%");
                    }
                    if (!string.IsNullOrWhiteSpace(txtSearchTypeOfMembership.Text))
                    {
                        cmd.Parameters.AddWithValue("@typeofmembership", $"%{txtSearchTypeOfMembership.Text}%");
                    }
                    if (!string.IsNullOrWhiteSpace(txtSearchDuration.Text))
                    {
                        cmd.Parameters.AddWithValue("@duration", $"%{txtSearchDuration.Text}%");
                    }

                    using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                    {
                        DataSet DS = new DataSet();
                        DA.Fill(DS);
                        dataGridView1.DataSource = DS.Tables[0];
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            welcome owner = new welcome();
            owner.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MemberReport_Load(object sender, EventArgs e)
        {

        }
    }
}
