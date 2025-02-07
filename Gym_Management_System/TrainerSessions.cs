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
    public partial class TrainerSessions : Form
    {
        public TrainerSessions()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)   //reschedule button
        {
            this.Hide();
            RescheduleSessions form = new RescheduleSessions();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)  //cancel session button
        {
            this.Hide();
            CancelSession form = new CancelSession();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)  //add sesson button
        {
            this.Hide();
            Sessions form = new Sessions();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)  //back  button
        {
            this.Hide();
            Trainer form = new Trainer();
            form.ShowDialog();
        }

        private void TrainerSessions_Load(object sender, EventArgs e)
        {

        }
    }
}
