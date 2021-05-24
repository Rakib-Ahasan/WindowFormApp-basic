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

namespace InsertingDateIntoDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                errorProvider1.SetError(this.textBox1, "Please enter your Id.");
                textBox1.Focus();
            }

            else if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                errorProvider2.SetError(this.textBox2, "Please enter your name.");
                textBox2.Focus();
            }
            else if (comboBox1.SelectedItem == null)
            {
                errorProvider3.SetError(this.comboBox1, "Please enter your gender.");
                comboBox1.Focus();
            }
            else if (string.IsNullOrEmpty(richTextBox1.Text) == true)
            {
                errorProvider4.SetError(this.richTextBox1, "Please enter your Id.");
                richTextBox1.Focus();
            }
            else
            {


                string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);

                string query2 = "select * from Customer where Id =@Id";
                SqlCommand cmd2 = new SqlCommand(query2, connection);
                cmd2.Parameters.AddWithValue("@Id", textBox1.Text);
                connection.Open();
                SqlDataReader dr = cmd2.ExecuteReader(); // for select statement.
                if (dr.HasRows == true)
                {
                    MessageBox.Show(textBox1.Text + "Your Id already exist!!");
                }
                else
                {
                    connection.Close();
                    string query = "insert into Customer values(@Id,@Name,@Gender,@Address)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Gender", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Address", richTextBox1.Text);

                    connection.Open();

                    int a = cmd.ExecuteNonQuery(); //insert,update,delete.
                    if (a > 0)
                    {
                        MessageBox.Show("Congratulations customer has been added.");
                    }
                    else
                    {
                        MessageBox.Show("Sorry customer insertion failed.");
                    }

                    connection.Close();
                }
                textBox1.Clear();
                textBox2.Clear();
                comboBox1 .ResetText();
                richTextBox1.Clear();
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                errorProvider1.SetError(this.textBox1, "Please enter your Id.");
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
                errorProvider2.SetError(this.textBox2, "Please enter your name.");
                textBox2.Focus();
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                errorProvider3.SetError(this.comboBox1, "Please enter your gender.");
                comboBox1.Focus();
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text) == true)
            {
                errorProvider4.SetError(this.richTextBox1, "Please enter your Id.");
                richTextBox1.Focus();
            }
            else
            {
                errorProvider4.Clear();
            }
        }
    }
}
