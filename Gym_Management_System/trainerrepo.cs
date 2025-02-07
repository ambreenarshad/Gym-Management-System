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
    public partial class trainerrepo : Form
    {
        public trainerrepo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;"))
            {
                conn.Open();

                // Start with a base query
                string commandText = "SELECT trainer_username, ";  // Always include username

                if (comboBox1.SelectedItem != null)
                {
                    string filter = comboBox1.SelectedItem.ToString().ToLower();
                    switch (filter)
                    {
                        case "performance_ratings":
                            commandText += "performance_rating ";  // Append only the selected field
                            break;
                        case "report_details":
                            commandText += "report_details ";
                            break;
                        default:
                            commandText += "* ";  // Safety case, should not be necessary
                            break;
                    }
                }
                else
                {
                    commandText += "* ";  // If nothing is selected, select all columns
                }

                commandText += "FROM TrainerReport";  // Complete the SQL statement

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    // Apply search criteria if there is search text
                    commandText += " WHERE trainer_username = @username and gymid='" + gb.g_id + "'";
                }
                else
                    commandText += " WHERE gymid='" + gb.g_id + "'";

                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // Add parameter for the search text
                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        cmd.Parameters.AddWithValue("@username", txtSearch.Text);
                    }

                    using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                    {
                        DataSet DS = new DataSet();
                        DA.Fill(DS);  // Fill the dataset

                        // Bind the dataset to the DataGridView
                        dataGridView1.DataSource = DS.Tables[0];
                    }
                }
            }
        }
        //private int gymId; // Member variable to store gymId

        //public trainerrepo() // Constructor that takes gymId as a parameter
        //{
        //    InitializeComponent();
        //    this.gymId = gymId; // Store gymId for use within the form
        //}
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            welcome owner = new welcome();
            owner.ShowDialog();
        }

        private void trainerrepo_Load(object sender, EventArgs e)
        {

        }
    }
}
