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

namespace FilteringDataGridViewUsingRadioButtons
{
    public partial class Form1 : Form
    {
        private string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
            BindGridView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection con=new SqlConnection(cs);
            string query = "select * from employee where gender ='male'";
            SqlDataAdapter sda=new SqlDataAdapter(query,con);
            DataTable data=new DataTable();
            sda.Fill(data);
            //dataGridView1.DataSource = data;
            dataGridView1.Rows.Clear();
            foreach (DataRow row in data.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = row["Id"].ToString();
                dataGridView1.Rows[index].Cells[1].Value = row["Name"].ToString();
                dataGridView1.Rows[index].Cells[2].Value = row["Gender"].ToString();
                dataGridView1.Rows[index].Cells[3].Value = row["Salary"].ToString();
            }

        }

        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from employee";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridView1.DataSource = data;
            dataGridView1.Rows.Clear();
            foreach (DataRow row in data.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = row["Id"].ToString();
                dataGridView1.Rows[index].Cells[1].Value = row["Name"].ToString();
                dataGridView1.Rows[index].Cells[2].Value = row["Gender"].ToString();
                dataGridView1.Rows[index].Cells[3].Value = row["Salary"].ToString();
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from employee where gender ='female'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            //dataGridView1.DataSource = data;
            dataGridView1.Rows.Clear();
            foreach (DataRow row in data.Rows)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = row["Id"].ToString();
                dataGridView1.Rows[index].Cells[1].Value = row["Name"].ToString();
                dataGridView1.Rows[index].Cells[2].Value = row["Gender"].ToString();
                dataGridView1.Rows[index].Cells[3].Value = row["Salary"].ToString();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection(cs);
            //string query = "select * from employee";
            //SqlDataAdapter sda = new SqlDataAdapter(query, con);
            //DataTable data = new DataTable();
            //sda.Fill(data);
            //dataGridView1.DataSource = data;
            BindGridView();
        }
    }
}
