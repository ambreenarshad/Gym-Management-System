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
    public partial class interselectDP : Form
    {
        public interselectDP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            mainpage Form = new mainpage();
            Form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //own
            gb.mchoosen = 1;
           
            this.Hide();

            Select_DietPlan Form = new Select_DietPlan();
            Form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //trainer
            gb.mchoosen = 2;
            this.Hide();

            Select_DietPlan Form = new Select_DietPlan();
            Form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //other
            gb.mchoosen = 3;
            this.Hide();

            Select_DietPlan Form = new Select_DietPlan();
            Form.ShowDialog();
        }
    }
}
