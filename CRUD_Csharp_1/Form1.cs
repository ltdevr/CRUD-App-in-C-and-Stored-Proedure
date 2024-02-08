using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Csharp_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=Test1_DB;User ID=sa;Password=ionU8IYG&^%543");
        private void button1_Click(object sender, EventArgs e)
        {
            int empid = int.Parse(textBox1.Text);
            string empname = textBox2.Text, city = comboBox1.Text, contact = textBox4.Text, sex="";
            double age = double.Parse(textBox3.Text);
            DateTime joindate = DateTime.Parse(dateTimePicker1.Text);
            if(radioButton1.Checked == true) { sex = "Male"; } else { sex = "Female"; }
            con.Open();
            SqlCommand c = new SqlCommand("exec InsertEmp_SP '" + empid + "', '" + empname + "', '" + city + "','"+ age + "','"+ sex + "','"+ joindate + "','"+ contact + "'", con);
            c.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Inserted...");
            GetEmpList();
            textBox1.Text = ""; textBox2.Text = ""; comboBox1.Text = "";
            textBox3.Text = "";  textBox4.Text = ""; dateTimePicker1.Text = "";
        }

        void GetEmpList()
        {
            SqlCommand c = new SqlCommand("exec ListEmp_SP", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetEmpList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Update
            int empid = int.Parse(textBox1.Text);
            string empname = textBox2.Text, city = comboBox1.Text, contact = textBox4.Text, sex = "";
            double age = double.Parse(textBox3.Text);
            DateTime joindate = DateTime.Parse(dateTimePicker1.Text);
            if (radioButton1.Checked == true) { sex = "Male"; } else { sex = "Female"; }
            con.Open();
            SqlCommand c = new SqlCommand("exec UpdateEmp_SP '" + empid + "', '" + empname + "', '" + city + "','" + age + "','" + sex + "','" + joindate + "','" + contact + "'", con);
            c.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Updated...");
            GetEmpList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Delete
            if (MessageBox.Show("Are you sure to delete?", "Delete Document", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { 
                int empid = int.Parse(textBox1.Text);
                con.Open();
                SqlCommand c = new SqlCommand("exec DeleteEmp_SP '" + empid + "'", con);
                c.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Deleted...");
                GetEmpList();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Load specific employee
            int empid = int.Parse(textBox1.Text);
            //con.Open();
            SqlCommand c = new SqlCommand("exec LoadEmp_SP '" + empid + "'", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
            //c.ExecuteNonQuery();
            //MessageBox.Show("Successfully Deleted...");
            //GetEmpList();
        }
    }
}
