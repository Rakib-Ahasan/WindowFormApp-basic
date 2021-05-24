using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace RememberMeFunctionalityInLoginFrom
{
    public partial class Form1 : Form
    {
        private string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
            LoadCredential();
        }

        void SaveCredential()
        {
            if (checkBox1.Checked == true)
            {
                Properties.Settings.Default.UserName = userNameTextBox.Text;
                Properties.Settings.Default.Password = passwordTextBox.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.UserName = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
        }

        void LoadCredential()
        {
            if (Properties.Settings.Default.UserName != string.Empty)
            {
                userNameTextBox.Text = Properties.Settings.Default.UserName;
                passwordTextBox.Text = Properties.Settings.Default.Password;
                checkBox1.Checked = true;
            }

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userNameTextBox.Text)==true&& string.IsNullOrEmpty(passwordTextBox.Text)==true)
            {
                MessageBox.Show("Both fields are requered", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection con=new SqlConnection(cs);
                string query = "select * from remember_me where username=@username and password=@pass ";
                SqlCommand cmd =new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@username", userNameTextBox.Text);
                cmd.Parameters.AddWithValue("@pass", passwordTextBox.Text);

                con.Open();
               SqlDataReader dr= cmd.ExecuteReader();
                if (dr.HasRows==true)
                {
                    MessageBox.Show("Login Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveCredential();

                    this.Hide();
                    Form2 f2 = new Form2();
                    f2.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Login failed", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close(); 
            }
            //if (userNameTextBox.Text=="Rakib"&& passwordTextBox.Text=="rakib123")
            //{
            //    MessageBox.Show("Login Successfull");
            //    SaveCredential();
                
            //    this.Hide();
            //    Form2 f2=new Form2();
            //    f2.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Username or password is should be apply.");
            //}
        }
    }
}
