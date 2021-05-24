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

namespace AutoIncrementTextFildValue
{
    public partial class Form1 : Form
    {
        private string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
            GetIncrementId();
            BindGridView(); 
        }

        void GetIncrementId()
        {
            SqlConnection con=new SqlConnection(cs);
            string query = "select id from small_tbl";
            SqlDataAdapter sda=new SqlDataAdapter(query,con);
            DataTable data=new DataTable();
            sda.Fill(data);
            if (data.Rows.Count<1)
            {
                idTextBox.Text = "1";
            }
            else
            {
                SqlConnection con1 = new SqlConnection(cs);
                string query1 = "select max(id) from small_tbl";
                SqlCommand cmd=new SqlCommand(query1,con1);
                con1.Open();
                int a=Convert.ToInt32(cmd.ExecuteScalar());
                a = a + 1;
                idTextBox.Text = a.ToString();
                con.Close();
            }
        }

        private void insertButton_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(cs);
            string query = "insert into small_tbl values(@id,@name,@age)";
            SqlCommand cmd=new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@id", idTextBox.Text);
            cmd.Parameters.AddWithValue("@name", nameTextBox.Text);
            cmd.Parameters.AddWithValue("@age", ageTextBox.Text);
           
            con.Open();
            int a=cmd.ExecuteNonQuery();
            if (a>0)
            {
                MessageBox.Show("Inserted");
                BindGridView();
                clear();
            }
            else
            {
                MessageBox.Show("Not inserted");
            }
            con.Close();
        }

        void clear()
        {
            //idTextBox.Clear();
            nameTextBox.Clear();
            ageTextBox.Clear();
        }

        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from small_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = data;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            idTextBox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            nameTextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            ageTextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update small_tbl set name =@name,age=@age where id =@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", idTextBox.Text);
            cmd.Parameters.AddWithValue("@name", nameTextBox.Text);
            cmd.Parameters.AddWithValue("@age", ageTextBox.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Updated");
                BindGridView();
                clear();
            }
            else
            {
                MessageBox.Show("Not updated");
            }
            con.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from small_tbl where id =@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", idTextBox.Text);
            
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Deleted");
                BindGridView();
                clear();
            }
            else
            {
                MessageBox.Show("Not deleted");
            }
            con.Close();
        }
    }
}
