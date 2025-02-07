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
    public partial class welcome : Form
    {
        public welcome()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            MemberReport owner = new MemberReport();
            owner.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            this.Hide();
            REMOVE owner = new REMOVE();
            owner.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.Hide();
            Add owner = new Add();
            owner.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            trainerrepo owner = new trainerrepo();
            owner.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            this.Hide();
            Main_Page owner = new Main_Page();
            owner.ShowDialog();
        }

        private void welcome_Load(object sender, EventArgs e)
        {

        }
        // Constructor that takes gymId as a parameter
        public welcome(int gymId)
        {
            InitializeComponent();
            this.gymId = gymId;  // Store gymId for use within the form
            LoadGymSpecificData();  // Call method to load data specific to this gym
        }
        private void LoadGymSpecificData()
        {
            // Use this.gymId to load data specific to the gym
            // Example: MessageBox.Show("Loaded data for gym ID: " + this.gymId);
        }
        private int gymId;  // Member variable to store gymId

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            addtrainer owner = new addtrainer();
            owner.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            revokemember owner = new revokemember();
            owner.ShowDialog();
        }

    }
}
