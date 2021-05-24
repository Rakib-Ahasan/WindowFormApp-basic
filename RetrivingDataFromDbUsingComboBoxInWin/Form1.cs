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
using RetrivingDataFromDbUsingComboBoxInWin.Properties;
using System.IO;

namespace RetrivingDataFromDbUsingComboBoxInWin
{
    public partial class Form1 : Form
    {
        private string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
            BindGridView();
            BindComboBOx();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            //FILTER PROPERTY PATTERN 
            // FOR PNG FORMAT:"PNG FILE (*.png) | *.png"
            // FOR JPG FORMAT:"JPG FILE (*.jpg) | *.jpg"
            // FOR BMP FORMAT:"BMP FILE (*.bmp) | *.bmp"
            // FOR GIF FORMAT:"GIF FILE (*.gif) | *.gif"
            // FOR 4 IMAGE FORMATS: "Image File (*.png, *.jpg,*.bmp,*.gif) | *.png,*.jpg,*.bmp,*.gif"
            //FOR ALL FILES: "All files (*.*) | *.*"
            ofd.Filter = "PNG FILE(*.png) | *.png |JPG FILE (*.jpg) | *.jpg |BMP FILE (*.bmp) | *.bmp |GIF FILE (*.gif) | *.gif |Image File (*.png, *.jpg,*.bmp,*.gif) | *.png,*.jpg,*.bmp,*.gif |All files (*.*) | *.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into student_details values (@id,@name,@age,@img)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@name", textBox2.Text);
            cmd.Parameters.AddWithValue("@age", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@img", SavePhoto());

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Inserted");
                BindGridView();
                BindComboBOx();
                ResetControls();
            }
            else
            {
                MessageBox.Show("Data not Inserted");
            }
            con.Close();
        }

        private Byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }
        void BindGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from student_details";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            //Creating column for image
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[3];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            //Adjusting all column in one data grid view windows
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Increasing the height of rows
            dataGridView1.RowTemplate.Height = 50;

        }
        void ResetControls()
        {
            textBox1.Clear();
            textBox2.Clear();
            numericUpDown1.Value = 0;
            pictureBox1.Image = Resources.iconfinder_icon_image_2867912;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            numericUpDown1.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value);
            pictureBox1.Image = GetPhoto((byte[])dataGridView1.SelectedRows[0].Cells[3].Value);
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update student_details set id =@id,name=@name,age=@age,pictuer=@img where id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@name", textBox2.Text);
            cmd.Parameters.AddWithValue("@age", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@img", SavePhoto());

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Updated");
                BindGridView();
                BindComboBOx();
                ResetControls();
            }
            else
            {
                MessageBox.Show("Data not Updated");
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from student_details where id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", textBox1.Text);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Deleted");
                BindGridView();
                BindComboBOx();
                ResetControls();
            }
            else
            {
                MessageBox.Show("Data not Deleted");
            }
            con.Close();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        void BindComboBOx()
        {
            idComboBox.Items.Clear();
            SqlConnection con=new SqlConnection(cs);
            string query = "select * from student_details ";
            SqlCommand cmd=new SqlCommand(query,con);

            con.Open();
           SqlDataReader dr= cmd.ExecuteReader();
           while (dr.Read())
           {
               int id = dr.GetInt32(0);
               idComboBox.Items.Add(id);
           }
            con.Close();
        }

        private void idComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (idComboBox.SelectedItem.ToString() != null)
            {
                int id = Convert.ToInt32(idComboBox.SelectedItem.ToString());
                SqlConnection con = new SqlConnection(cs);
                string query = "select * from student_details where id=@id";
                SqlDataAdapter sda=new SqlDataAdapter(query,con);
                //sqlDataAdapter is a disconnected architecture so it no need con.open()/con.close().
                sda.SelectCommand.Parameters.AddWithValue("@id", id);
                DataTable data=new DataTable();
                sda.Fill(data);
                if (data.Rows.Count>0)
                {
                    textBox3.Text = data.Rows[0]["name"].ToString();
                    textBox4.Text = data.Rows[0]["age"].ToString();
                    MemoryStream ms =new MemoryStream((byte[])data.Rows[0]["pictuer"]);
                    pictureBox2.Image =new Bitmap(ms);
                }
            }
        }
    }
}
