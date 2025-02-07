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
    public partial class Trainer : Form
    {
        public Trainer()
        {
            InitializeComponent();
        }

        private void Trainer_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //sessions
            this.Hide();

            TrainerSessions sessionsform = new TrainerSessions();
            sessionsform.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //workoutplans
            this.Hide();

            TrainerInterWO workoutform = new TrainerInterWO();
            workoutform.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //dietplans
            this.Hide();

            TrainerInterDP dietform = new TrainerInterDP();
            dietform.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //feedback
            this.Hide();

            Trainer_Feedback feedbackform = new Trainer_Feedback();
            feedbackform.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            Main_Page form = new Main_Page();
            form.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
