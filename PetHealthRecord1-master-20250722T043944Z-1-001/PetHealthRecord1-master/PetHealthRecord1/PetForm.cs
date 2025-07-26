using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PetHealthRecord1
{

    public partial class PetForm : Form
    {
        private string _username;
        public PetForm(string username)
        {

            InitializeComponent();
            _username = username;

            cmbYear.SelectedIndexChanged += cmbYear_SelectedIndexChanged;
            cmbMonth.SelectedIndexChanged += cmbMonth_SelectedIndexChanged;
            cmbDay.SelectedIndexChanged += cmbDay_SelectedIndexChanged;
                
            cmbxPetType.Items.Add("Cat");
            cmbxPetType.Items.Add("Dog");
            

            cmbxPetType.SelectedIndexChanged += cmbxPetType_SelectedIndexChanged;


        
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide current form (optional)
            Home home = new Home(_username);
            home.FormClosed += (s, args) => this.Close(); // When Home closes, close this form
            home.Show();
        }

        private void btnBrowsePetPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an image.";
                ofd.Filter = "Image Files| *.jpg; *.jpeg; *.png;";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textboxPetImagePath.Text = ofd.FileName;
                    pictureBoxPetPhoto.Image = Image.FromFile(ofd.FileName);
                    pictureBoxPetPhoto.SizeMode = PictureBoxSizeMode.StretchImage;

                   
                }
            }
        }

        private void textboxtPetID_TextChanged(object sender, EventArgs e)
        {
            textboxtPetID.TextChanged -= textboxtPetID_TextChanged;

            string prefix = "P003";
            string text = textboxtPetID.Text;

            // Kung wala pa yung prefix, idagdag siya
            if (!text.StartsWith(prefix))
            {
                text = prefix + text;
            }

            // Kuhanin yung part after prefix
            string numberPart = text.Substring(prefix.Length);

            // Tanggalin lahat ng hindi digits
            numberPart = new string(numberPart.Where(char.IsDigit).ToArray());

            // I-set balik yung textbox
            textboxtPetID.Text = prefix + numberPart;

            // Ilagay cursor sa dulo para madagdag agad
            textboxtPetID.SelectionStart = textboxtPetID.Text.Length;

            // Re-subscribe event handler
            textboxtPetID.TextChanged += textboxtPetID_TextChanged;
        
        }

       

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textboxPetName_TextChanged(object sender, EventArgs e)
        {
            textboxPetName.TextChanged -= textboxPetName_TextChanged;

            // Filter out non-letter characters
            string filtered = new string(textboxPetName.Text.Where(char.IsLetter).ToArray());

            if (filtered.Length > 0)
            {
                // Capitalize first letter + keep rest as is
                filtered = char.ToUpper(filtered[0]) + filtered.Substring(1);
            }

            if (textboxPetName.Text != filtered)
            {
                textboxPetName.Text = filtered;
                textboxPetName.SelectionStart = textboxPetName.Text.Length;
            }

            // Re-subscribe event
            textboxPetName.TextChanged += textboxPetName_TextChanged;
    
        }

        private void PetForm_Load(object sender, EventArgs e)
        {
            // Populate Year and Month when form loads
            for (int year = 2000; year <= DateTime.Now.Year; year++)
            {
                cmbYear.Items.Add(year);
            }

            // Populate Month ComboBox with month names
            for (int month = 1; month <= 12; month++)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                cmbMonth.Items.Add(monthName);
            }

            // Optionally populate Day ComboBox with default 1–31
            for (int day = 1; day <= 31; day++)
            {
                cmbDay.Items.Add(day);
            }
        }


        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbYear.SelectedItem != null)
                UpdateDays();

        }

        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbYear.SelectedItem != null && cmbMonth.SelectedItem != null && cmbDay.SelectedItem != null)
            {
                int year = Convert.ToInt32(cmbYear.SelectedItem);
                string monthName = cmbMonth.SelectedItem.ToString();
                int month = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;
                int day = Convert.ToInt32(cmbDay.SelectedItem);

            }

        }
        

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMonth.SelectedItem != null)
                UpdateDays();
        }

        private void UpdateDays()
        {
            if (cmbMonth.SelectedItem == null || cmbYear.SelectedItem == null)
            {
                return; // Walang laman, huwag ituloy
            }

            int year = Convert.ToInt32(cmbYear.SelectedItem);
            string monthName = cmbMonth.SelectedItem.ToString();

            // Convert month name to month number
            int month = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;

            // Get correct number of days in the selected month and year
            int daysInMonth = DateTime.DaysInMonth(year, month);

            // Populate Day ComboBox
            cmbDay.Items.Clear();
            for (int day = 1; day <= daysInMonth; day++)
            {
                cmbDay.Items.Add(day);
            }

            cmbDay.SelectedIndex = 0; // optional: pre-select first day
        }


        private void textboxWeight_TextChanged(object sender, EventArgs e)
        {
            textboxWeight.TextChanged -= textboxWeight_TextChanged;

            string text = textboxWeight.Text;

            // Alisin "kg" sa dulo kung meron
            if (text.EndsWith("kg"))
            {
                text = text.Substring(0, text.Length - 2);
            }

            // Tanggalin invalid characters (digits and one dot lang)
            // Allow only digits and max one '.'
            var filteredChars = new List<char>();
            bool dotFound = false;
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    filteredChars.Add(c);
                }
                else if (c == '.' && !dotFound)
                {
                    filteredChars.Add(c);
                    dotFound = true;
                }
            }
            string numericPart = new string(filteredChars.ToArray());

            // Kung empty numeric part, just clear
            if (string.IsNullOrEmpty(numericPart))
            {
                textboxWeight.Text = "";
                textboxWeight.TextChanged += textboxWeight_TextChanged;
                return;
            }

            // Update textbox with numeric + "kg"
            textboxWeight.Text = numericPart + "kg";

            // Cursor ilagay bago ang "kg"
            textboxWeight.SelectionStart = numericPart.Length;

            // Ibalik event handler
            textboxWeight.TextChanged += textboxWeight_TextChanged;
        
        }

        private void cmbxPetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cmbxPetType.SelectedItem?.ToString();
            Console.WriteLine("Pet type selected: " + selected);
        }

        private void textboxAppetite_TextChanged(object sender, EventArgs e)
        {
            textboxAppetite.TextChanged -= textboxAppetite_TextChanged;

            // Filter out non-letter characters
            string filtered = new string(textboxAppetite.Text.Where(char.IsLetter).ToArray());

            if (filtered.Length > 0)
            {
                // Capitalize first letter + keep rest as is
                filtered = char.ToUpper(filtered[0]) + filtered.Substring(1);
            }

            if (textboxAppetite.Text != filtered)
            {
                textboxAppetite.Text = filtered;
                textboxAppetite.SelectionStart = textboxAppetite.Text.Length;
            }

            // Re-subscribe event
            textboxAppetite.TextChanged += textboxAppetite_TextChanged;
        }

        private void textboxBreed_TextChanged(object sender, EventArgs e)
        {
            textboxBreed.TextChanged -= textboxBreed_TextChanged;

            // Filter out non-letter characters
            string filtered = new string(textboxBreed.Text.Where(char.IsLetter).ToArray());

            if (filtered.Length > 0)
            {
                // Capitalize first letter + keep rest as is
                filtered = char.ToUpper(filtered[0]) + filtered.Substring(1);
            }

            if (textboxBreed.Text != filtered)
            {
                textboxBreed.Text = filtered;
                textboxBreed.SelectionStart = textboxBreed.Text.Length;
            }

            // Re-subscribe event
            textboxBreed.TextChanged += textboxBreed_TextChanged;
        }
        

        private void textboxMood_TextChanged(object sender, EventArgs e)
        {
            textboxMood.TextChanged -= textboxMood_TextChanged;

            // Filter out non-letter characters
            string filtered = new string(textboxMood.Text.Where(char.IsLetter).ToArray());

            if (filtered.Length > 0)
            {
                // Capitalize first letter + keep rest as is
                filtered = char.ToUpper(filtered[0]) + filtered.Substring(1);
            }

            if (textboxMood.Text != filtered)
            {
                textboxMood.Text = filtered;
                textboxMood.SelectionStart = textboxMood.Text.Length;
            }

            // Re-subscribe event
            textboxMood.TextChanged += textboxMood_TextChanged;
        }

        private void rbtnMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMale.Checked)
            {
                Console.WriteLine("Sex selected: Male");
            }
        }

        private void rbtnFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnFemale.Checked)
            {
                Console.WriteLine("Sex selected: Female");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.TextChanged -= textBox1_TextChanged;

            // Filter out non-letter characters
            string filtered = new string(textBox1.Text.Where(char.IsLetter).ToArray());

            if (filtered.Length > 0)
            {
                // Capitalize first letter + keep rest as is
                filtered = char.ToUpper(filtered[0]) + filtered.Substring(1);
            }

            if (textBox1.Text != filtered)
            {
                textBox1.Text = filtered;
                textBox1.SelectionStart = textBox1.Text.Length;
            }

            // Re-subscribe event
            textBox1.TextChanged += textBox1_TextChanged;

        
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.TextChanged -= textBox2_TextChanged;

            // Filter out non-letter characters
            string filtered = new string(textBox2.Text.Where(char.IsLetter).ToArray());

            if (filtered.Length > 0)
            {
                // Capitalize first letter + keep rest as is
                filtered = char.ToUpper(filtered[0]) + filtered.Substring(1);
            }

            if (textBox2.Text != filtered)
            {
                textBox2.Text = filtered;
                textBox2.SelectionStart = textBox2.Text.Length;
            }

            // Re-subscribe event
            textBox2.TextChanged += textBox2_TextChanged;
        
        }

        private void textboxOwnerName_TextChanged(object sender, EventArgs e)
        {
            textboxOwnerName.TextChanged -= textboxOwnerName_TextChanged;

            string text = textboxOwnerName.Text;

            // Allow letters, spaces, and dots only
            string filtered = new string(text.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '.').ToArray());

            if (text != filtered)
            {
                MessageBox.Show("Owner Name must contain letters, spaces, and dots only.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textboxOwnerName.Text = filtered;
                textboxOwnerName.SelectionStart = textboxOwnerName.Text.Length;
            }

            // Re-subscribe event
            textboxOwnerName.TextChanged += textboxOwnerName_TextChanged;
       
        }

        private void textboxContact_TextChanged(object sender, EventArgs e)
        {
            string input = textboxContact.Text;

            // Remove all non-digit characters
            string digitsOnly = new string(input.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length > 11)
            {
                MessageBox.Show("Contact number must only be 11 digits.", "Invalid Length", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                digitsOnly = digitsOnly.Substring(0, 11); // limit to 11 digits
            }

            if (textboxContact.Text != digitsOnly)
            {
                textboxContact.Text = digitsOnly;
                textboxContact.SelectionStart = textboxContact.Text.Length;
            }

            // Optional: feedback if length is not yet 11
            if (digitsOnly.Length == 11)
            {
                Console.WriteLine("Contact number is valid: " + digitsOnly);
            }
        
        }

        private void btnSaveForm_Click(object sender, EventArgs e)
        {

            string petID = textboxtPetID.Text;
            string petName = textboxPetName.Text;
            string petType = cmbxPetType.Text;
            string breed = textboxBreed.Text;
            string month = cmbMonth.Text;
            string day = cmbDay.Text;
            string year = cmbYear.Text;
            string date = $"{month}/{day}/{year}";
            string weight = textboxWeight.Text;
            string appetite = textboxAppetite.Text;
            string mood = textboxMood.Text;
            string sex = rbtnMale.Checked ? "Male" : (rbtnFemale.Checked ? "Female" : "");
            string medication = textBox1.Text;
            string symptoms = textBox2.Text;
            string ownerName = textboxOwnerName.Text;
            string contact = textboxContact.Text;
            string photoPath = textboxPetImagePath.Text;


            // Validate required fields
            if (string.IsNullOrWhiteSpace(petID) ||
                string.IsNullOrWhiteSpace(petName) ||
                string.IsNullOrWhiteSpace(petType) ||
                string.IsNullOrWhiteSpace(breed) ||
                string.IsNullOrWhiteSpace(month) ||
                string.IsNullOrWhiteSpace(day) ||
                string.IsNullOrWhiteSpace(year) ||
                string.IsNullOrWhiteSpace(weight) ||
                string.IsNullOrWhiteSpace(appetite) ||
                string.IsNullOrWhiteSpace(mood) ||
                string.IsNullOrWhiteSpace(sex) ||
                string.IsNullOrWhiteSpace(medication) ||
                string.IsNullOrWhiteSpace(symptoms) ||
                string.IsNullOrWhiteSpace(ownerName) ||
                string.IsNullOrWhiteSpace(contact))
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {


                PetRecords.PetRecord newRecord = new PetRecords.PetRecord
                {
                    PetID = petID,
                    PetName = petName,
                    PetType = petType,
                    Breed = breed,
                    Sex = sex,
                    Date = date,
                    Weight = weight,
                    Appetite = appetite,
                    Mood = mood,
                    Medication = medication,
                    Symptoms = symptoms,
                    OwnerName = ownerName,
                    Contact = contact,
                    PhotoPath = photoPath
                };
                PetRecords PetRecords = Application.OpenForms.OfType<PetRecords>().FirstOrDefault();
                if (PetRecords == null || PetRecords.IsDisposed)
                {
                    PetRecords = new PetRecords();
                }

                PetRecords.Show();
                PetRecords.AddPetRecord(newRecord);

                MessageBox.Show("Pet record saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputFields();
            }
        }

            
        



        private void ClearInputFields()
        {
            textboxtPetID.Clear();
            textboxPetName.Clear();
            cmbxPetType.SelectedIndex = -1;
            textboxBreed.Clear();
            cmbMonth.SelectedIndex = -1;
            cmbDay.SelectedIndex = -1;
            cmbYear.SelectedIndex = -1;
            textboxWeight.Clear();
            textboxAppetite.Clear();
            textboxMood.Clear();
            rbtnMale.Checked = false;
            rbtnFemale.Checked = false;
            textBox1.Clear(); // medication
            textBox2.Clear(); // symptoms
            textboxOwnerName.Clear();
            textboxContact.Clear();
            textboxPetImagePath.Clear();
        
        }

        private void btnViewForm_Click(object sender, EventArgs e)
        {
            PetRecords petRecordsForm = Application.OpenForms.OfType<PetRecords>().FirstOrDefault();

            if (petRecordsForm == null)
            {
                petRecordsForm = new PetRecords();
                petRecordsForm.Show();
            }
            else
            {
                petRecordsForm.BringToFront(); // Dalhin sa harapan
            }

            string petID = textboxtPetID.Text;
            string petName = textboxPetName.Text;
            string petType = cmbxPetType.Text;
            string breed = textboxBreed.Text;
            string month = cmbMonth.Text;
            string day = cmbDay.Text;
            string year = cmbYear.Text;
            string weight = textboxWeight.Text;
            string appetite = textboxAppetite.Text;
            string mood = textboxMood.Text;
            string sex = rbtnMale.Checked ? "Male" : (rbtnFemale.Checked ? "Female" : "");
            string medication = textBox1.Text;
            string symptoms = textBox2.Text;
            string ownerName = textboxOwnerName.Text;
            string contact = textboxContact.Text;
            string photoPath = textboxPetImagePath.Text;

           

            // Fill the form using your public method
            petRecordsForm.FillPetDetails(
                petID, petName, petType, breed,
                month, day, year, weight,
                appetite, mood, sex,
                medication, symptoms,
                ownerName, contact, photoPath);

            petRecordsForm.Show();
        
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
