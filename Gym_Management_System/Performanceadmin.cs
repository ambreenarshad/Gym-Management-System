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
    public partial class Performanceadmin : Form
    {
        public Performanceadmin()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string commandText = "SELECT gym_name, ";

                if (comboBox1.SelectedItem != null)
                {
                    string filter = comboBox1.SelectedItem.ToString().ToLower();
                    switch (filter)
                    {
                        case "go_username":
                            commandText += "go_username ";
                            break;
                        case "admin_username":
                            commandText += "admin_username ";
                            break;
                        default:
                            commandText += "* ";  // Default case to handle unexpected items
                            break;
                    }
                }
                else
                {
                    commandText += "* ";
                }

                commandText += "FROM Gym ";  // Make sure table name is correct

                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    commandText += "WHERE gym_name = @username";  // Use parameterized query to avoid SQL injection
                }

                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        cmd.Parameters.AddWithValue("@username", txtSearch.Text);
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminmain owner = new adminmain();
            owner.ShowDialog();
        }
    }
}
