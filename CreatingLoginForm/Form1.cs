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

namespace CreatingLoginForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool check = checkBox1.Checked;
            switch (check)
            {
                case true:
                    textBox2.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox2.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!= ""&& textBox2.Text!="")
            {
                SqlConnection connection = new SqlConnection(cs);
                string query = "select*from login_tbl where UserName= @user and Password=@pass";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@user", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                connection.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    MessageBox.Show("Congratulations LOGIN successfull.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sorry LOGIN Failed!.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                connection.Close();
            }
            else
            {
                MessageBox.Show("Please enter your username and password Correctly.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            textBox1.Clear();
            textBox2.Clear();
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                errorProvider1.SetError(this.textBox1, "Please enter your username.");
                textBox1.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                errorProvider2.SetError(this.textBox2, "Please enter your password.");
                textBox2.Focus();
            }
            else
            {
                errorProvider2.Clear();
            }
        }
    }
}
