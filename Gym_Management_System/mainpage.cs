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
    public partial class mainpage : Form
    {
        public mainpage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void mainpage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            intermediate_WO Form = new intermediate_WO();
            Form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            interselectWO Form = new interselectWO();
            Form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            intermediateDP Form = new intermediateDP();
            Form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            interselectDP Form = new interselectDP();
            Form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            BookSessions Form = new BookSessions();
            Form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();

            Feedback Form = new Feedback();
            Form.ShowDialog();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            Main_Page Form = new Main_Page();
            Form.ShowDialog();
        }

    }
}
