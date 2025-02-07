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
    public partial class adminmain : Form
    {
        public adminmain()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//approval
        {

            this.Hide();
            addgym owner = new addgym();
            owner.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)// revokes membership
        {
            this.Hide();
            RevokeMembership owner = new RevokeMembership();
            owner.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            Performanceadmin owner = new Performanceadmin();
            owner.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            this.Hide();

            Main_Page signinForm = new Main_Page();
            signinForm.ShowDialog();
        }

    }
}
