using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; // Required for image file check
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PetHealthRecord1
{

    public partial class PetRecords : Form
    {

        private string _username;
        public PetRecords(string username) // ✅ Constructor with username
        {
            InitializeComponent();
            _username = username; // ✅ Store it
        }

        private List<PetRecord> petRecords = new List<PetRecord>();
        private void LoadComboBoxes()
        {
            comboBoxOwner.Items.Clear();
            comboBoxPetID.Items.Clear();

            var owners = petRecords.Select(p => p.OwnerName).Distinct().ToList();
            comboBoxOwner.Items.AddRange(owners.ToArray());

            var petIDs = petRecords.Select(p => p.PetID).Distinct().ToList();
            comboBoxPetID.Items.AddRange(petIDs.ToArray()); // <---
        }

        public void AddPetRecord(PetRecord record)
        {
           

            petRecords.Add(record);
            LoadComboBoxes();
        }
        public class PetRecord
        {
            public string PetID { get; set; }
            public string PetName { get; set; }
            public string PetType { get; set; }
            public string Breed { get; set; }
            public string Sex { get; set; }
            public string Date { get; set; } // MM/DD/YYYY format
            public string Weight { get; set; }
            public string Appetite { get; set; }
            public string Mood { get; set; }
            public string Medication { get; set; }
            public string Symptoms { get; set; }
            public string OwnerName { get; set; }
            public string Contact { get; set; }
            public string PhotoPath { get; set; }

            public override string ToString() => $"{PetID} - {PetName}";
        }
        public PetRecords()
        {

            InitializeComponent();
        }

        private void MakeFieldsReadOnly()
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;
            textBox9.ReadOnly = true;
            textBox10.ReadOnly = true;

            foreach (var tb in Controls.OfType<TextBox>())
            {
                tb.BackColor = SystemColors.Control;
                tb.BorderStyle = BorderStyle.None;
                tb.TabStop = false;
            }

            comboBoxOwner.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPetID.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        // ✅ PUBLIC METHOD to autofill details from PetForm
        public void FillPetDetails(
            string petID, string petName, string petType, string breed,
            string month, string day, string year, string weight,
            string appetite, string mood, string sex,
            string medication, string symptoms,
            string ownerName, string contact, string photoPath)
        {
            // 🟪 These textboxes should match your designer names
            textBox1.Text = petName;       // Pet Name
            textBox2.Text = petType;       // Pet Type
            textBox3.Text = breed;         // Breed
            textBox4.Text = sex;           // Sex
            textBox5.Text = month;         // Month
            textBox7.Text = day;           // Day
            textBox8.Text = year;          // Year
            textBox6.Text = weight;        // Weight
            textBox10.Text = medication;   // Medication/Vaccine
            textBox9.Text = symptoms;      // Symptoms
            comboBoxOwner.Text = ownerName;    // Owner Name
            comboBoxPetID.Text = petID;        // Pet ID or Pet Selector (if applicable)

            // Load image if file exists
            if (File.Exists(photoPath))
            {
                pictureBox1.Image = Image.FromFile(photoPath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e) // Pet Type
        {
 
        }

        private void button1_Click(object sender, EventArgs e) // Back button
        {
            this.Hide(); // Hide current form (optional)
            Home home = new Home(_username);
            home.FormClosed += (s, args) => this.Close(); // When Home closes, close this form
            home.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // Pet Name
        {

        }

        private void PetRecords_Load(object sender, EventArgs e)
        {
            
            // ✅ Sample pet data
            petRecords.Add(new PetRecord
            {
                PetID = "P001",
                PetName = "Buddy",
                PetType = "Dog",
                Breed = "Labrador",
                Sex = "Male",
                Date = "July/22/2025",
                Weight = "20kg",
                Appetite = "Good",
                Mood = "Happy",
                Medication = "Vaccine A",
                Symptoms = "Cough",
                OwnerName = "John",
                Contact = "09171234567",
                PhotoPath = "C:\\Users\\Andrew\\OneDrive\\Pictures\\Screenshots\\Screenshot 2025-07-24 143606.png"
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
                Appetite = "Average",
                Mood = "Calm",
                Medication = "Deworming",
                Symptoms = "Sneezing",
                OwnerName = "Jane",
                Contact = "09981234567",
                PhotoPath = "C:\\Users\\Andrew\\OneDrive\\Pictures\\Screenshots\\Screenshot 2025-07-24 143721.png"
            });

            LoadComboBoxes();
            MakeFieldsReadOnly();
        }


        private void textBox10_TextChanged(object sender, EventArgs e) // Medication
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // Choose Owner
        {
            string selectedOwner = comboBoxOwner.SelectedItem?.ToString();
            if (selectedOwner != null)
            {
                var record = petRecords.FirstOrDefault(p => p.OwnerName == selectedOwner);
                if (record != null)
                {
                    var dateParts = record.Date.Split('/');
                    FillPetDetails(
                        record.PetID, record.PetName, record.PetType, record.Breed,
                        dateParts[0], dateParts[1], dateParts[2],
                        record.Weight, record.Appetite, record.Mood, record.Sex,
                        record.Medication, record.Symptoms,
                        record.OwnerName, record.Contact, record.PhotoPath
                    );
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // Choose Pet
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e) // Breed
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e) // Sex
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e) // Month
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e) // Day
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e) // Year
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e) // Weight
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e) // Symptoms
        {

        }

        private void button2_Click(object sender, EventArgs e) // Edit profile
        {
            UserProfile profileForm = new UserProfile(_username);
            profileForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save Pet Records";
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.FileName = "pet_records.csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string header = "PetID,PetName,PetType,Breed,Date,Weight,Appetite,Mood,Sex,Medication,Symptoms,OwnerName,Contact,PhotoPath";
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine(header);
                        foreach (var pet in petRecords)
                        {
                            string line = $"{pet.PetID},{pet.PetName},{pet.PetType},{pet.Breed},{pet.Date},{pet.Weight},{pet.Appetite},{pet.Mood},{pet.Sex},{pet.Medication},{pet.Symptoms},{pet.OwnerName},{pet.Contact},{pet.PhotoPath}";
                            writer.WriteLine(line);
                        }
                    }
                    MessageBox.Show("Pet records downloaded successfully.", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error downloading the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxPetID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedID = comboBoxPetID.SelectedItem?.ToString();
            if (selectedID != null)
            {
                var record = petRecords.FirstOrDefault(p => p.PetID == selectedID);
                if (record != null)
                {
                    var dateParts = record.Date.Split('/');
                    FillPetDetails(
                        record.PetID, record.PetName, record.PetType, record.Breed,
                        dateParts[0], dateParts[1], dateParts[2],
                        record.Weight, record.Appetite, record.Mood, record.Sex,
                        record.Medication, record.Symptoms,
                        record.OwnerName, record.Contact, record.PhotoPath
                    );
                }
            }
        }
    }
    
}




    

