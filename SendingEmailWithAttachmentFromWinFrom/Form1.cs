using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace SendingEmailWithAttachmentFromWinFrom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage(fromTextBox.Text, toTextBox.Text, subjectTextBox.Text, bodyTextBox.Text);
                mail.Attachments.Add(new Attachment(attachTextBox.Text.ToString()));
                SmtpClient client = new SmtpClient(smtpComboBox.SelectedItem.ToString());
                client.UseDefaultCredentials = false;
                client.Port = 587;
                //client.Port = 465;
                //client.Port = 25;
                client.Credentials = new NetworkCredential(userNameTextBox.Text, passwordTextBox.Text);
                client.EnableSsl = true;
                client.Send(mail);
                MessageBox.Show("Email send", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetControls();

            }
            catch (Exception exception)
            {

                MessageBox.Show("Error" + exception.Message);
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select File";
            ofd.Filter = "All file (*.*)|*.*";
            ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName.ToString();
                attachTextBox.Text = path;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        void ResetControls()
        {
            fromTextBox.Clear();
            toTextBox.Clear();
            subjectTextBox.Clear();
            smtpComboBox.SelectedItem = null;
            userNameTextBox.Clear();
            passwordTextBox.Clear();
            bodyTextBox.Clear();
            attachTextBox.Clear();
        }
    }
}
