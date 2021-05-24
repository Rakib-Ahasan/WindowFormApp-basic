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

namespace AddingCheckBoxValuesIntoSqlDb
{
    public partial class Form1 : Form
    {
        private string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            /*Query for store procedure...
             CREATE TABLE MY_TABLE
             (
             NAME VARCHAR(50) NOT NULL,
            INTERESTED_IN_CRICKET BIT,
            INTERESTED_IN_FOOTBALL BIT,
            INTERESTED_IN_READING BIT,
            INTERESTED_IN_TRAVELING BIT,
            );

            SELECT*FROM MY_TABLE;

            CREATE PROCEDURE SP_INSERT_MY_TABLE
            @name varchar(50),
            @cricket bit,
            @football bit,
            @reading bit,
            @traveling bit 
            as 
            begin
            insert into MY_TABLE
            values (@name,@cricket,@football,@reading,@traveling)
            end
            */

            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SP_INSERT_MY_TABLE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@cricket", checkBox1.Checked);
            cmd.Parameters.AddWithValue("@football", checkBox2.Checked);
            cmd.Parameters.AddWithValue("@reading", checkBox3.Checked);
            cmd.Parameters.AddWithValue("@traveling", checkBox4.Checked);

            con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Inserted");
            }
            else
            {
                MessageBox.Show("Not Inserted");
            }
            con.Close();
        }
    }
}
