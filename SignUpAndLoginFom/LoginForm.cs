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


namespace SignUpAndLoginFom
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUpForm su=new SignUpForm();
            this.Hide();
            su.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            string query = "SELECT * FROM SIGNUP WHERE EMAIL=@email AND PASSWORD=@password";
           // string query2 = "SELECT * FROM SIGNUP WHERE Id=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@email", textBox1.Text);
            cmd.Parameters.AddWithValue("@password", textBox2.Text);

            con.Open();
            SqlDataReader dr=cmd.ExecuteReader();
            if (dr.HasRows==true)
            {
                MessageBox.Show("Congratulations LOGIN succesfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                SignOut so=new SignOut();
                so.Show();
            }
            else
            {
                MessageBox.Show("Sorry LOGIN failed!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

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

        private void LoginForm_VisibleChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

       
    }
}
