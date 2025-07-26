using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PetHealthRecord1
{
    public partial class Home : Form
    {
        private string _username;
        public Home(string username)
        {
            InitializeComponent();
            _username = username;
        }

        private void btnNewForm_Click(object sender, EventArgs e)
        {
           
            PetForm petRecords = new PetForm(_username);
            petRecords.Show();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PetRecords pethealthRecords = new PetRecords();
            pethealthRecords.Show();
            this.Close();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            labelWelcome.Text = $"Welcome, {_username}!";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Reminders remindersForm = new Reminders(); // Create a new Reminders form
            remindersForm.Show();                      // Show the Reminders form
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PetRecords petReports = new PetRecords();
            petReports.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn loginForm = new LogIn();
            loginForm.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserProfile userProfileForm = new UserProfile(_username);
            userProfileForm.Show();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.labelWelcome = new System.Windows.Forms.Label();
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Font = new System.Drawing.Font("Arial", 14F, FontStyle.Bold);
            this.labelWelcome.Location = new System.Drawing.Point(30, 30);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(120, 24);
            this.labelWelcome.Text = "Welcome,!";
        }
    }
}
