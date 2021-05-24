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

namespace UseAgressiveOrScalarFunctionOfSql
{
    public partial class Form1 : Form
    {
        private string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
            BindGridView();
            TotalNetSalary();
            TotalEmployees();
        }
       
        private void idTextBox_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTextBox.Text))
            {
                idTextBox.Focus();
                errorProvider1.SetError(this.idTextBox, "Id is empty!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void nameTextBox_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                nameTextBox.Focus();
                errorProvider2.SetError(this.nameTextBox, "Name is empty!");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void designationTextBox_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(designationTextBox.Text))
            {
                designationTextBox.Focus();
                errorProvider3.SetError(this.designationTextBox, "Designation is empty!");
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void basicPayTextBox_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(basicPayTextBox.Text))
            {
                basicPayTextBox.Focus();
                errorProvider4.SetError(this.basicPayTextBox, "Basic Pay is empty!");
            }
            else
            {
                errorProvider4.Clear();
            }
        }
        private void basicPayTextBox_TextChanged_1(object sender, EventArgs e)
        {
            int CA, MA, HR, grossPay, incomeTax, netSalary;
            int basicPay = 0;
            if (string.IsNullOrEmpty(basicPayTextBox.Text))
            {
                basicPayTextBox.Focus();
                errorProvider4.SetError(this.basicPayTextBox, "Empty basic pay");
            }
            else
            {
                errorProvider4.Clear();
                basicPay = Convert.ToInt32(basicPayTextBox.Text);
            }


            if (basicPay >= 40000)
            {
                CA = (int)(basicPay * 0.40);
                conveyanceTextBox.Text = CA.ToString();
                MA = (int)(basicPay * 0.30);
                medicalTextBox.Text = MA.ToString();
                HR = (int)(basicPay * 0.20);
                homeRentTextBox.Text = HR.ToString();
                grossPay = basicPay + CA + MA + HR;
                grossPayTextBox.Text = grossPay.ToString();

                if (grossPay >= 60000)
                {
                    incomeTax = (int)(grossPay * 0.03);
                    incomeTaxTextBox.Text = incomeTax.ToString();
                    netSalary = grossPay - incomeTax;
                    netSalaryTextBox.Text = netSalary.ToString();
                }

                else if (grossPay >= 50000)
                {
                    incomeTax = (int)(grossPay * 0.02);
                    incomeTaxTextBox.Text = incomeTax.ToString();
                    netSalary = grossPay - incomeTax;
                    netSalaryTextBox.Text = netSalary.ToString();
                }

            }
            else if (basicPay >= 30000)
            {
                CA = (int)(basicPay * 0.35);
                conveyanceTextBox.Text = CA.ToString();
                MA = (int)(basicPay * 0.25);
                medicalTextBox.Text = MA.ToString();
                HR = (int)(basicPay * 0.15);
                homeRentTextBox.Text = HR.ToString();
                grossPay = basicPay + CA + MA + HR;
                grossPayTextBox.Text = grossPay.ToString();

                if (grossPay >= 60000)
                {
                    incomeTax = (int)(grossPay * 0.03);
                    incomeTaxTextBox.Text = incomeTax.ToString();
                    netSalary = grossPay - incomeTax;
                    netSalaryTextBox.Text = netSalary.ToString();
                }

                else if (grossPay >= 50000)
                {
                    incomeTax = (int)(grossPay * 0.02);
                    incomeTaxTextBox.Text = incomeTax.ToString();
                    netSalary = grossPay - incomeTax;
                    netSalaryTextBox.Text = netSalary.ToString();
                }

            }
            else
            {
                CA = 3000;
                conveyanceTextBox.Text = CA.ToString();
                MA = 2000;
                medicalTextBox.Text = MA.ToString();
                HR = 1000;
                homeRentTextBox.Text = HR.ToString();
                grossPay = basicPay + CA + MA + HR;
                grossPayTextBox.Text = grossPay.ToString();

                incomeTaxTextBox.Text = "No tax applied";
                netSalary = grossPay;
                netSalaryTextBox.Text = netSalary.ToString();
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            idTextBox.Clear();
            nameTextBox.Clear();
            designationTextBox.Clear();
            basicPayTextBox.Clear();
            conveyanceTextBox.Clear();
            medicalTextBox.Clear();
            homeRentTextBox.Clear();
            incomeTaxTextBox.Clear();
            netSalaryTextBox.Clear();
            grossPayTextBox.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con=new SqlConnection(cs);
            string query = "insert into employee_payroll values (@id,@name,@dgn,@bs,@conv,@m,@hr,@gp,@it,@ns)";
            SqlCommand cmd=new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@id", idTextBox.Text);
            cmd.Parameters.AddWithValue("@name", nameTextBox.Text);
            cmd.Parameters.AddWithValue("@dgn", designationTextBox.Text);
            cmd.Parameters.AddWithValue("@bs", basicPayTextBox.Text);
            cmd.Parameters.AddWithValue("@conv", conveyanceTextBox.Text);
            cmd.Parameters.AddWithValue("@m", medicalTextBox.Text);
            cmd.Parameters.AddWithValue("@hr", homeRentTextBox.Text);
            cmd.Parameters.AddWithValue("@gp", grossPayTextBox.Text);
            cmd.Parameters.AddWithValue("@it", incomeTaxTextBox.Text);
            cmd.Parameters.AddWithValue("@ns", netSalaryTextBox.Text);
            
            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a>0)
            {
                MessageBox.Show("Data Insertrd");
                BindGridView();
                TotalNetSalary();
                TotalEmployees();
            }
            else
            {
                MessageBox.Show("Data not Inserted");
            }
            con.Close();
        }

        private void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from employee_payroll";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void TotalNetSalary()
        {
            SqlConnection con=new SqlConnection(cs);
            string query = "select sum(net_salary) from employee_payroll";
            SqlCommand cmd=new SqlCommand(query,con);
            con.Open();
            int a =Convert.ToInt32(cmd.ExecuteScalar());
            salaryPaidTextBox.Text = a.ToString();
            con.Close();
        }
        void TotalEmployees()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select count(id) from employee_payroll";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int a = Convert.ToInt32(cmd.ExecuteScalar());
            totalEmployeesTextBox.Text = a.ToString();
            con.Close();
        }
    }
}
