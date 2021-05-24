using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string pattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

        private void userNameTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userNameTextBox.Text)==true)
            {
                errorProvider1.SetError(this.userNameTextBox,"Please Enter User Name.");
                userNameTextBox.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(passwordTextBox.Text,pattern))
            {
                errorProvider2.SetError(this.passwordTextBox,"UPPERCASE,LOWERCASE,NUMBERS,SPECIAL CHARACTERS");
                passwordTextBox.Focus();
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void confirmPasswordTextBox_Leave(object sender, EventArgs e)
        {
            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                errorProvider3.SetError(this.confirmPasswordTextBox,"Passwoard is not match.");
                confirmPasswordTextBox.Focus();
            }
            else
            {
                errorProvider3.Clear();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userNameTextBox.Text) == true)
            {
                errorProvider1.SetError(this.userNameTextBox, "Please Enter User Name.");
                userNameTextBox.Focus();
            }
            else if (Regex.IsMatch(passwordTextBox.Text, pattern))
            {
                errorProvider2.SetError(this.passwordTextBox, "UPPERCASE,LOWERCASE,NUMBERS,SPECIAL CHARACTERS");
                passwordTextBox.Focus();
            }
            else if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                errorProvider3.SetError(this.confirmPasswordTextBox, "Passwoard is not match.");
                confirmPasswordTextBox.Focus();
            }
            else
            {
                MessageBox.Show("Congratulations cnfirmation successful.");
            }
        }
    }
}
