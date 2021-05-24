using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendSmsSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (System.Net.WebClient client=new System.Net.WebClient())
            {
                try
                {
                    string url = "http://smsc.vianett.no/v3/send.ashx?" +
                                 "src="+pnTextBox.Text+"&" +
                                 "dst="+pnTextBox.Text+"&" +
                                 "msg="+System.Web.HttpUtility.UrlEncode(msgTextBox.Text,System.Text.Encoding.GetEncoding("ISO-8859-1"))+"" +
                                 "username="+System.Web.HttpUtility.UrlEncode(usernameTextBox.Text)+"&" +
                                 "password=" + System.Web.HttpUtility.UrlEncode(PassTextBox.Text);
                    string result = client.DownloadString(url);
                    if (result.Contains("OK"))
                    {
                        MessageBox.Show("Your message has been successfully sent.","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Your message has been failed sent.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
