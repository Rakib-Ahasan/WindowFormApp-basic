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
using System.Configuration;
using System.Data.SqlClient;

namespace SignUpAndLoginFom
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }
        string passPattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
        private string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

        private void idTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTextBox.Text) == true)
            {
                errorProvider1.SetError(this.idTextBox, "Please enter your id.");
                idTextBox.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void idTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void nameTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text) == true)
            {
                errorProvider2.SetError(this.nameTextBox, "Please enter your student name.");
                nameTextBox.Focus();
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void nameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsLetter(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8 || ch == 32)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void fatherNameTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fatherNameTextBox.Text) == true)
            {
                errorProvider3.SetError(this.fatherNameTextBox, "Please enter your father name.");
                fatherNameTextBox.Focus();
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void fatherNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsLetter(ch) == true)
            {
                e.Handled = false;
            }
            else if (ch == 8 || ch == 32)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void genderComboBox_Leave(object sender, EventArgs e)
        {
            if (genderComboBox.SelectedItem == null)
            {
                errorProvider4.SetError(this.genderComboBox, "Please select your gender.");
                genderComboBox.Focus();
            }
            else
            {
                errorProvider4.Clear();
            }
        }

        private void classNumericUpDown_Leave(object sender, EventArgs e)
        {
            if (classNumericUpDown.Value == 0)
            {
                errorProvider5.SetError(this.classNumericUpDown, "Please select your class. ");
                classNumericUpDown.Focus();
            }
            else
            {
                errorProvider5.Clear();
            }
        }

        private void emailTextBox_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(emailTextBox.Text, pattern) == false)
            {
                errorProvider6.SetError(this.emailTextBox, "Invalid email");
                emailTextBox.Focus();
            }
            else
            {
                errorProvider6.Clear();
            }
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(passwordTextBox.Text, passPattern) == false)
            {
                errorProvider7.SetError(this.passwordTextBox, "Please enter UpperCase,LowerCase,Numbers,Symbols.");
                passwordTextBox.Focus();
            }
            else
            {
                errorProvider7.Clear();
            }
        }

        private void confirmPasswordTextBox_Leave(object sender, EventArgs e)
        {
            if (confirmPasswordTextBox.Text != passwordTextBox.Text)
            {
                errorProvider8.SetError(this.confirmPasswordTextBox, "Password is not match.");
                confirmPasswordTextBox.Focus();
            }
            else
            {
                errorProvider8.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTextBox.Text) == true)
            {
                errorProvider1.SetError(this.idTextBox, "Please enter your id.");
                idTextBox.Focus();
            }
            else if (string.IsNullOrEmpty(nameTextBox.Text) == true)
            {
                errorProvider2.SetError(this.nameTextBox, "Please enter your student name.");
                nameTextBox.Focus();
            }
            else if (string.IsNullOrEmpty(fatherNameTextBox.Text) == true)
            {
                errorProvider3.SetError(this.fatherNameTextBox, "Please enter your father name.");
                fatherNameTextBox.Focus();
            }
            else if (genderComboBox.SelectedItem == null)
            {
                errorProvider4.SetError(this.genderComboBox, "Please select your gender.");
                genderComboBox.Focus();
            }
            else if (classNumericUpDown.Value == 0)
            {
                errorProvider5.SetError(this.classNumericUpDown, "Please select your class. ");
                classNumericUpDown.Focus();
            }
            else if (Regex.IsMatch(emailTextBox.Text, pattern) == false)
            {
                errorProvider6.SetError(this.emailTextBox, "Invalid email");
                emailTextBox.Focus();
            }
            else if (Regex.IsMatch(passwordTextBox.Text, passPattern) == false)
            {
                errorProvider7.SetError(this.passwordTextBox, "Please enter UpperCase,LowerCase,Numbers,Symbols.");
                passwordTextBox.Focus();
            }
            else if (confirmPasswordTextBox.Text != passwordTextBox.Text)
            {
                errorProvider8.SetError(this.confirmPasswordTextBox, "Password is not match.");
                confirmPasswordTextBox.Focus();
            }
            else
            {
                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);

                string query = "INSERT INTO SIGNUP VALUES (@id,@name,@fname,@gender,@class,@email,@password)";
                string query2 = "SELECT * FROM SIGNUP WHERE Id=@id";

                SqlCommand cmd2=new SqlCommand(query2,con);
                cmd2.Parameters.AddWithValue("@id", idTextBox.Text);
                con.Open();
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.HasRows==true)
                {
                    MessageBox.Show("Id already exist!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                }
                else
                {
                    con.Close(); 
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", idTextBox.Text);
                    cmd.Parameters.AddWithValue("@name", nameTextBox.Text);
                    cmd.Parameters.AddWithValue("@fname", fatherNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@gender", genderComboBox.SelectedItem);
                    cmd.Parameters.AddWithValue("@class", classNumericUpDown.Value);
                    cmd.Parameters.AddWithValue("@email", emailTextBox.Text);
                    cmd.Parameters.AddWithValue("@password", passwordTextBox.Text);

                    con.Open();

                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Congratulations!!", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        MessageBox.Show("Your email is :"+emailTextBox.Text+"\n \n"+"Your password is :"+passwordTextBox.Text, "Attention", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        this.Hide(); 
                        LoginForm longIn=new LoginForm();
                        longIn.Show();
                    }
                    else
                    {
                        MessageBox.Show("Sorry Insetation failed!!", "Failed", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }

                    con.Close();
                }

            }


        }

        private void ResetBUtton_Click(object sender, EventArgs e)
        {
            idTextBox.Clear();
            nameTextBox.Clear();
            fatherNameTextBox.Clear();
            genderComboBox.SelectedItem = null;
            classNumericUpDown.Value = 0;
            emailTextBox.Clear();
            passwordTextBox.Clear();
            confirmPasswordTextBox.Clear();
        }
    }
}
