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

namespace CRUDApplication
{

    //Connected Architecture--SqlDataReader, SqlCommand.
    //Disconnected Architecture -- SqlDataAdapter--DataTable-DataSet.
    public partial class Form1 : Form
    {
        private string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query2 = "SELECT * FROM Employee WHERE Id=@id";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            cmd2.Parameters.AddWithValue("@id", idTextBox.Text);
            con.Open();
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.HasRows == true)
            {
                MessageBox.Show(idTextBox.Text +" "+ "Id already exsit!!.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                con.Close();

                string query = " INSERT INTO Employee VALUES (@id,@name,@gender,@age,@designation,@salary)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", idTextBox.Text);
                cmd.Parameters.AddWithValue("@name", nameTextBox.Text);
                cmd.Parameters.AddWithValue("@gender", genderComboBox.SelectedItem);
                cmd.Parameters.AddWithValue("@age", ageNumericUpDown.Value);
                cmd.Parameters.AddWithValue("@designation", designationComboBox.SelectedItem);
                cmd.Parameters.AddWithValue("@salary", salaryTextBox.Text);

                con.Open();

                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    MessageBox.Show("Inserted successfully!!", "Congratulations", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    BindGridView();
                }
                else
                {
                    MessageBox.Show("Inserted failed!!", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                con.Close();
                Clear();
            }
        }

        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "SELECT * FROM Employee";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = " UPDATE Employee SET Id=@id,Name=@name,Gender=@gender,Age=@age,Designaton=@designation,Salary=@salary WHERE Id=@id ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", idTextBox.Text);
            cmd.Parameters.AddWithValue("@name", nameTextBox.Text);
            cmd.Parameters.AddWithValue("@gender", genderComboBox.SelectedItem);
            cmd.Parameters.AddWithValue("@age", ageNumericUpDown.Value);
            cmd.Parameters.AddWithValue("@designation", designationComboBox.SelectedItem);
            cmd.Parameters.AddWithValue("@salary", salaryTextBox.Text);

            con.Open();

            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Update successfully!!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
            }
            else
            {
                MessageBox.Show("Update failed!!", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            Clear();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            idTextBox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            nameTextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            genderComboBox.SelectedItem = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            ageNumericUpDown.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value);
            designationComboBox.SelectedItem = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            salaryTextBox.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = " DELETE FROM Employee WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", idTextBox.Text);

            con.Open();

            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Delete successfully!!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGridView();
            }
            else
            {
                MessageBox.Show("Delete failed!!", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            Clear();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            idTextBox.Clear();
            nameTextBox.Clear();
            genderComboBox.SelectedItem = null;
            ageNumericUpDown.Value = 0;
            designationComboBox.SelectedItem = null;
            salaryTextBox.Clear();
            idTextBox.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SqlConnection con=new SqlConnection(cs);
            string qury = "SELECT * FROM Employee WHERE Name LIKE @name +'%'  ";
            SqlDataAdapter data=new SqlDataAdapter(qury,con);
            data.SelectCommand.Parameters.AddWithValue("@name", searchTextBox.Text.Trim());
            DataTable dt=new DataTable();
            data.Fill(dt);
            if (dt.Rows.Count>0)
            {
                dataGridView1.DataSource = data;
            }
            else
            {
                MessageBox.Show("No data found!!", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
            }
          
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string qury = "SELECT * FROM Employee Where Name like @name + '%' ";
            SqlDataAdapter sda = new SqlDataAdapter(qury, con);
            sda.SelectCommand.Parameters.AddWithValue("@name", searchTextBox.Text.Trim());
            DataTable data= new DataTable();
            sda.Fill(data);
            if (data.Rows.Count > 0)
            {
                dataGridView1.DataSource = data;
            }
            else
            {
                MessageBox.Show("No data found!!", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
            }
        }
    }
}
