using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PetHealthRecord1
{
    public partial class Reminders : Form

    {

        private List<PetRecord> petRecords = new List<PetRecord>();


        public Reminders()
        {
            InitializeComponent();

            comboBox2.Items.Add("Pending");
            comboBox2.Items.Add("Completed");
            comboBox2.Items.Add("Missed");

            petRecords.Add(new PetRecord
            {
                PetID = "P001",
                PetName = "Buddy",
                PetType = "Dog",
                Breed = "Labrador",
                Sex = "Male",
                Date = "July/22/2025",
                Weight = "20kg",
                Medication = "Vaccine A",
                Symptoms = "Cough",
                OwnerName = "John",

            });

            petRecords.Add(new PetRecord
            {
                PetID = "P002",
                PetName = "Mittens",
                PetType = "Cat",
                Breed = "Persian",
                Sex = "Female",
                Date = "June/15/2025",
                Weight = "5kg",
                Medication = "Deworming",
                Symptoms = "Sneezing",
                OwnerName = "Jane",

            });
        }
        
        private void LoadComboBoxes()
        {
            comboBoxPetName.Items.Clear();
            foreach (var pet in petRecords)
            {
                comboBoxPetName.Items.Add(pet.PetName);
            }
        }
        public class PetRecord
        {
            public string PetID { get; set; }
            public string OwnerName { get; set; }
            public string PetName { get; set; }
            public string PetType { get; set; }
            public string Breed { get; set; }
            public string Sex { get; set; }
            public string Date { get; set; }
            public string Weight { get; set; }
            public string Medication { get; set; }
            public string Symptoms { get; set; }
            public string PhotoPath { get; set; }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // addreminders 
        {
            if (string.IsNullOrWhiteSpace(comboBoxPetName.Text) ||      // Pet name
        string.IsNullOrWhiteSpace(textBox1.Text) ||      // Medication/Vaccine
        string.IsNullOrWhiteSpace(textBox2.Text) ||      // Symptoms
        comboBox2.SelectedItem == null)                   // Status
            {
                MessageBox.Show("Please fill in all required fields.", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Collect data
            string petName = comboBoxPetName.Text;
            string medication = textBox1.Text;
            string symptoms = textBox2.Text;
            string dateTimeToTake = dateTimePicker1.Value.ToString("f"); // Full date/time string
            string status = comboBox2.SelectedItem.ToString();

            // Add to DataGridView
            dataGridView1.Rows.Add(petName, medication, symptoms, dateTimeToTake, status);

            // Confirmation
            MessageBox.Show("Reminder added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void textBox1_TextChanged_1(object sender, EventArgs e) //Medication/Vaccine 
        {
            string input = textBox1.Text;
            string valid = new string(input.Where(c => char.IsLetterOrDigit(c) || c == ' ').ToArray());

            if (input != valid)
            {
                textBox1.Text = valid;
                textBox1.SelectionStart = valid.Length;
            }
        }

        private void Reminders_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Pet Name";
            dataGridView1.Columns[1].Name = "Medication/Vaccine";
            dataGridView1.Columns[2].Name = "Symptoms";
            dataGridView1.Columns[3].Name = "Date/Time to Take";
            dataGridView1.Columns[4].Name = "Status";

            // Optional: Make columns auto-size for better appearance
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadComboBoxes();  // <-- tawagin dito para mapuno ang comboBoxPetName
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide current form (optional)
            Home home = new Home("");
            home.FormClosed += (s, args) => this.Close(); // When Home closes, close this form
            home.Show();

        }

        private void textBox3_TextChanged(object sender, EventArgs e) //petname 
        {
            string input = comboBoxPetName.Text;
            string valid = new string(input.Where(c => char.IsLetter(c) || c == ' ').ToArray());

            if (input != valid)
            {
                comboBoxPetName.Text = valid;
                comboBoxPetName.SelectionStart = valid.Length;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e) //symptoms
        {
            string input = textBox2.Text;
            string valid = new string(input.Where(c => char.IsLetter(c) || c == ' ' || c == ',' || c == '/').ToArray());

            if (input != valid)
            {
                textBox2.Text = valid;
                textBox2.SelectionStart = valid.Length;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) //date/time to take
        {
            DateTime selectedDate = dateTimePicker1.Value;
            MessageBox.Show("You selected: " + selectedDate.ToString("f"), "Date Changed");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) //status
        {
            string status = comboBox2.SelectedItem.ToString();

            switch (status)
            {
                case "Pending":
                    comboBox2.BackColor = Color.LightYellow;
                    break;
                case "Completed":
                    comboBox2.BackColor = Color.LightGreen;
                    break;
                case "Missed":
                    comboBox2.BackColor = Color.LightCoral;
                    break;
                default:
                    comboBox2.BackColor = SystemColors.Window;
                    break;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) // List of Reminders per Pet
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Pet Name";
            dataGridView1.Columns[1].Name = "Medication/Vaccine";
            dataGridView1.Columns[2].Name = "Symptoms";
            dataGridView1.Columns[3].Name = "Date/Time to Take";
            dataGridView1.Columns[4].Name = "Status";
        }

        private void comboBoxPetName_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedPetName = comboBoxPetName.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedPetName))
            {
                var record = petRecords.FirstOrDefault(p => p.PetName == selectedPetName);
                if (record != null)
                {
                    // I-set lang ang Medication/Vaccine at Symptoms textbox
                    textBox1.Text = record.Medication;
                    textBox2.Text = record.Symptoms;
                }
            }
        }
    }
}
