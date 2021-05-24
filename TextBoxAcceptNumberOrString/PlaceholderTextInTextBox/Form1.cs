﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaceholderTextInTextBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text=="Enter Your Name")
            {
                textBox1.Text = "";
                textBox1.ForeColor=Color.Black; 
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                textBox1.Text = "Enter Your Name";
                textBox1.ForeColor=Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Enter Your Email")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text=="")
            {
                textBox2.Text = "Enter Your Email";
                textBox2.ForeColor=Color.Gray;
            }
        }
    }
}
