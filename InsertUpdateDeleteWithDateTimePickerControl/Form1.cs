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

namespace InsertUpdateDeleteWithDateTimePickerControl
{
    public partial class Form1 : Form
    {
        private string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
            BindGridView();
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            SqlConnection con=new SqlConnection(cs);
            string query = "insert into date_time values (@id,@name,@doj,@toj,@dob)";
            SqlCommand cmd=new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@name", textBox2.Text);
            cmd.Parameters.AddWithValue("@doj", dateTimePicker1.Value.ToString("dd-MM-yyyy"));
            cmd.Parameters.AddWithValue("@toj", dateTimePicker2.Value.ToString("hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@dob", dateTimePicker3.Value.ToString("dd-MM-yyyy hh:mm:ss tt"));
            con.Open();
            int a =cmd.ExecuteNonQuery();
            if (a>0)
            {
                MessageBox.Show("Record inserted");
                BindGridView();
            }
            else
            {
                MessageBox.Show("Record not inserted");
            }
            con.Close();
        }

        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from date_time";
            SqlDataAdapter sda=new SqlDataAdapter(query,con);
            DataTable data=new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;


        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            dateTimePicker1.Value = DateTime.ParseExact(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(),"dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dateTimePicker2.Value = DateTime.ParseExact(dataGridView1.SelectedRows[0].Cells[3].Value.ToString(),"hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
            dateTimePicker3.Value = DateTime.ParseExact(dataGridView1.SelectedRows[0].Cells[4].Value.ToString(),"dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
           
           
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update date_time set id=@id,name=@name,doj=@doj,toj=@toj,dob=@dob where id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@name", textBox2.Text);
            cmd.Parameters.AddWithValue("@doj", dateTimePicker1.Value.ToString("dd-MM-yyyy"));
            cmd.Parameters.AddWithValue("@toj", dateTimePicker2.Value.ToString("hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@dob", dateTimePicker3.Value.ToString("dd-MM-yyyy hh:mm:ss tt"));
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Record updated");
                BindGridView();
            }
            else
            {
                MessageBox.Show("Record not updated");
            }
            con.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from date_time where id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
          
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Record deleted");
                BindGridView();
            }
            else
            {
                MessageBox.Show("Record not deleted");
            }
            con.Close();
        }
    }
}
