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
    public partial class TrainerGymFeedback : Form
    {
        public TrainerGymFeedback()
        {
            InitializeComponent();
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)  //back button
        {
            this.Hide();
            Trainer_Feedback form = new Trainer_Feedback();
            form.ShowDialog();
        }
        private void LoadData()
        {
            try
            {
                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "select f.member_username, f.stars from feedback f join TrainerGym t on f.trainer_username = t.trainer_username " +
                        "where status = 'accepted' and '" + gb.T_gymid + "' = t.gymid and '" + gb.T_un + "' = f.trainer_username;";

                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView2.DataSource = ds.Tables[0]; // Bind the DataGridView to the DataTable

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) //members feedback
        {

        }

        private void TrainerGymFeedback_Load(object sender, EventArgs e)
        {

        }

    }
}
