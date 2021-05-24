using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace CRUDInListBoxWithSqlServer
{

    public partial class Form1 : Form
    {
        private string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillListBox();
        }

        void FillListBox()
        {
            listBox1.Items.Clear();
            SqlConnection con = new SqlConnection(cs);
            string query = " select name from sports ";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string name = dr.GetString(0);
                listBox1.Items.Add(name);
            }
            con.Close();
            listBox1.Sorted = true;

        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "INSERT INTO Sports VALUES (@name)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Inserted Successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillListBox();
                textBox1.Clear();
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("Inserted Failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = " update sports set name=@name where name=@name1";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@name1", listBox1.SelectedItem);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Update Successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillListBox();
                textBox1.Clear();
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("Update Failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from sports where name=@name";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name",listBox1.SelectedItem);
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Deleted Successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillListBox();
                textBox1.Clear();
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("Deleted Failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }
    }
}
