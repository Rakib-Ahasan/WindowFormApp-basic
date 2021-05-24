using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtractNumberFromString
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int total = 0;
            foreach (string s in listBox1.SelectedItems)
            {
                total = total + int.Parse(Regex.Match(s, @"\d+").Value);
            }

            MessageBox.Show("Total coast is" + total);
        }
    }
}
