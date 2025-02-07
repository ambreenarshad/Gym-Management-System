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
    public partial class CancelSession : Form
    {
        public CancelSession()
        {
            InitializeComponent();
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)  //cancel button
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                conn.Open();

                string sid = textBox3.Text;
                int id = int.Parse(sid);

                if (!string.IsNullOrEmpty(sid))
                {
                    string query = "delete from SessionTable where sessionid = @id and '" + gb.T_un + "' = trainer_username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Session cancelled!");

                        this.Hide();
                        CancelSession form = new CancelSession();
                        form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Session id not found!");
                    }
                }
                else
                {
                    MessageBox.Show("Session id cannot be empty!");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e) //back buttom
        {
            this.Hide();
            TrainerSessions form = new TrainerSessions();
            form.ShowDialog();
        }

        private void CancelSession_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)  //session id
        {

        }
        private void LoadData()
        {
            try
            {

                {
                    SqlConnection conn = new SqlConnection("Data Source=AMBREEN\\SQLEXPRESS;Initial Catalog=finalproj;Integrated Security=True;");
                    conn.Open();
                    string query = "SELECT * FROM SessionTable where '" + gb.T_un + "' = trainer_username and status = 'booked'";
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
