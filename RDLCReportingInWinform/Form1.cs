﻿ using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

 namespace RDLCReportingInWinform
 {
     public partial class Form1 : Form
     {
         public Form1()
         {
             InitializeComponent();
         }

         private void Form1_Load(object sender, EventArgs e)
         {
             // TODO: This line of code loads data into the 'RdlcReportDbDataSet1.employee' table. You can move, or remove it, as needed.
             this.employeeTableAdapter.Fill(this.RdlcReportDbDataSet1.employee);

             this.reportViewer1.RefreshReport();
         }

         private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
         {

         }

         private void LoadButton_Click(object sender, EventArgs e)
         {
            //this.employeeTableAdapter.FillByGender(this.RdlcReportDbDataSet1.employee, comboBox1.SelectedItem.ToString());

            //this.reportViewer1.RefreshReport();
            this.employeeTableAdapter.FillByName(this.RdlcReportDbDataSet1.employee,nameTextBox.Text);

            this.reportViewer1.RefreshReport();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.employeeTableAdapter.Fill(this.RdlcReportDbDataSet1.employee);

            this.reportViewer1.RefreshReport();
        }
    }
 }
