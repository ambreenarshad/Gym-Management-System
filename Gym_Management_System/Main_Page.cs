using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Interface
{
    public partial class Main_Page : Form
    {
        public Main_Page()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //admin
        {
            this.Hide();
            adminlogin page = new adminlogin();
            page.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) //gym owner
        {
            this.Hide();
            loginowner page = new loginowner();
            page.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e) //trainer
        {
            this.Hide();

            TrainerLogin Form = new TrainerLogin();
            Form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e) //member
        {
            this.Hide();

            Form1 Form = new Form1();
            Form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e) //cross button
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e) //
        {
            this.Hide();
            ProjectReports form= new ProjectReports();
            form.ShowDialog();
        }
    }
}
