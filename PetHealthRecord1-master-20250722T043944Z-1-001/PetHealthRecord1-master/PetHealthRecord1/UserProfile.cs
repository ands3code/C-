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
    public partial class UserProfile : Form
    {
        private string _username;
        public UserProfile(string username)
        {
            InitializeComponent();
            _username = username;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//full name 
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, @"\d"))
            {
                MessageBox.Show("Full Name cannot contain numbers.");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void button2_Click(object sender, EventArgs e) // save 
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || // Full Name
               string.IsNullOrWhiteSpace(textBox2.Text) || // Address
              string.IsNullOrWhiteSpace(textBox3.Text) || // Phone Number
              string.IsNullOrWhiteSpace(textBox4.Text) || // Username
              string.IsNullOrWhiteSpace(textBox5.Text))   // Password
            {
                MessageBox.Show("Please fill in all fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

          

            // Collect values
            string fullName = textBox1.Text;
            string address = textBox2.Text;
            string phone = textBox3.Text;
            string username = textBox4.Text;
            string password = textBox5.Text;

            // Show confirmation message
            string message = $"Saved Successfully!\n\n" +
                             $"Full Name: {fullName}\n" +
                             $"Address: {address}\n" +
                             $"Phone Number: {phone}\n" +
                             $"Username: {username}\n" +
                             $"Password: {password}\n" +
                             $"Photo: Uploaded ✔️";

            MessageBox.Show(message, "Profile Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void UserProfile_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) // address 
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, @"[^a-zA-Z0-9\s,.\-]"))
            {
                MessageBox.Show("Address contains invalid characters.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Text = ""; // clear if invalid
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e) // Phone Number
        {
            string input = textBox3.Text;

            // Remove all non-digit characters
            string digitsOnly = new string(input.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length > 11)
            {
                MessageBox.Show("Contact number must only be 11 digits.", "Invalid Length", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                digitsOnly = digitsOnly.Substring(0, 11); // limit to 11 digits
            }

            if (textBox3.Text != digitsOnly)
            {
                textBox3.Text = digitsOnly;
                textBox3.SelectionStart = textBox3.Text.Length;
            }

            // Optional: feedback if length is not yet 11
            if (digitsOnly.Length == 11)
            {
                Console.WriteLine("Contact number is valid: " + digitsOnly);
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e) //username 
        {
            var valid = textBox4.Text.Where(c => char.IsLetterOrDigit(c) || c == '_').ToArray();
            if (textBox4.Text != new string(valid))
            {
                MessageBox.Show("Username can only contain letters, numbers, and underscores.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Text = new string(valid);
                textBox4.SelectionStart = textBox4.Text.Length;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e) //password
        {
            if (textBox5.Text.Contains(" "))
            {
                MessageBox.Show("Password cannot contain spaces.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox5.Text = textBox5.Text.Replace(" ", "");
                textBox5.SelectionStart = textBox5.Text.Length;
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
