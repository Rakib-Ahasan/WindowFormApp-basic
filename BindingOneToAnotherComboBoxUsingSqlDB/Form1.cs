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

namespace BindingOneToAnotherComboBoxUsingSqlDB
{
    public partial class Form1 : Form
    {
        private string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        private DataRow dr;
        public Form1()
        {
            InitializeComponent();
            BindCountryComboBox();
        }

        void BindCountryComboBox()
        {
            SqlConnection con=new SqlConnection(cs);
            string query = "select * from countries";
            SqlDataAdapter sda=new SqlDataAdapter(query,con);
            DataTable data=new DataTable();
            sda.Fill(data);
            dr = data.NewRow();
            dr.ItemArray=new object[]{ 0, "---Select Country---" };
            data.Rows.InsertAt(dr,0);
            comboBox1.DisplayMember = "NAME";
            comboBox1.ValueMember = "COUNTRY_ID";
            comboBox1.DataSource = data;
        }

        void BindCitiesComboBox(int countryId)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from cities where country_Id=@c_Id";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.Parameters.AddWithValue("@c_Id", countryId);
            DataTable data = new DataTable();
            sda.Fill(data);
            dr = data.NewRow();
            dr.ItemArray = new object[] { 0, "---Select City---" };
            data.Rows.InsertAt(dr, 0);
            comboBox2.DisplayMember = "NAME";
            comboBox2.ValueMember = "City_ID";
            comboBox2.DataSource = data;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue.ToString()!=null)
            {
                int countryId =Convert.ToInt32(comboBox1.SelectedValue.ToString());
                BindCitiesComboBox(countryId );
            }
        }
    }
}
