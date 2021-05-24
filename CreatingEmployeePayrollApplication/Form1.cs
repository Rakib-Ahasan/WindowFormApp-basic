using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatingEmployeePayrollApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void idTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTextBox.Text))
            {
                idTextBox.Focus();
                errorProvider1.SetError(this.idTextBox,"Id is empty!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void nameTextBox_Leave(object sender, EventArgs e)
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

        private void designationTextBox_Leave(object sender, EventArgs e)
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

        private void basicPayTextBox_Leave(object sender, EventArgs e)
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

        private void basicPayTextBox_TextChanged(object sender, EventArgs e)
        {
            int CA, MA, HR,grossPay,incomeTax,netSalary;
            int basicPay = 0;
            if (string.IsNullOrEmpty(basicPayTextBox.Text))
            {
                basicPayTextBox.Focus();
                errorProvider4.SetError(this.basicPayTextBox,"Empty basic pay");
            }
            else
            {
                errorProvider4.Clear();
                basicPay = Convert.ToInt32(basicPayTextBox.Text);
            }

            
            if (basicPay>=40000)
            {
                CA = (int) (basicPay * 0.40);
                conveyanceTextBox.Text = CA.ToString();
                MA= (int)(basicPay * 0.30);
                medicalTextBox.Text = MA.ToString();
                HR= (int)(basicPay * 0.20);
                homeRentTextBox.Text = HR.ToString();
                grossPay = basicPay + CA + MA + HR;
                grossPayTextBox.Text = grossPay.ToString();

                if (grossPay>=60000)
                {
                    incomeTax =(int) (grossPay * 0.03);
                    incomeTaxTextBox.Text = incomeTax.ToString();
                    netSalary = grossPay - incomeTax;
                    netSalaryTextBox.Text = netSalary.ToString();
                }

                else if(grossPay >= 50000)
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
    }
}
