using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;

namespace inventory
{
    public partial class password : Form
    {

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\Documents\\INVENTORY.accdb");

        public password()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void password_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            string query = "select admin_name,password from addadmin  ";
            cmd.CommandText = query;
            OleDbDataReader df = cmd.ExecuteReader();
            while (df.Read())
            {
                comboBox1.Items.Add(df[0].ToString());
                //comboBox2.Items.Add(df[1].ToString());
            }

            con.Close();
            //if (passwd.Length < 8 || passwd.Length > 14)
            //return false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                if (textBox3.Text == textBox4.Text)
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("UPDATE addadmin SET password = '" + textBox3.Text + "' where admin_name = '" + linkLabel5.Text + "'", con);



                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Your Password Is  Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



                else
                {
                    MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                }
            }

            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            string query = "select admin_name,password from addadmin where admin_name ='" + comboBox1.Text + "'";
            cmd.CommandText = query;
            OleDbDataReader df = cmd.ExecuteReader();
            while (df.Read())
            {
                textBox1.Text = df["password"].ToString();
                linkLabel5.Text = df["admin_name"].ToString();
                //string query = "select admin_name,password from addadmin  ";
            }

            con.Close();
        }

        // MessageBox.Show("Equipment Located", "Congrats", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
}
