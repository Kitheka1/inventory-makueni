using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace inventory
{
    public partial class un_locate : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\Documents\\INVENTORY.accdb");

        public un_locate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                string query = "select * from equipment where serial  LIKE '%" + textBox1.Text + "%'";
                cmd.CommandText = query;
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox3.Text = reader["staffname"].ToString();
                    textBox2.Text = reader["ename"].ToString();
                    numericUpDown1.Text = reader["quantity"].ToString();
                    textBox4.Text = reader["condition"].ToString();
                    linkLabel3.Text = reader["serial"].ToString();
                    dateTimePicker1.Text = reader["date_issued"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }
        }
            else { MessageBox.Show("Empty Fileld ! ", "Administtator Says", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            //unlocating a machine
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox3.Text != "")
            { 
            try
            {
                OleDbCommand cmd = con.CreateCommand();
                con.Open();

                cmd.CommandText = "DELETE * FROM equipment WHERE  serial  LIKE '%" + linkLabel3.Text + "%'";
                // cmd.CommandText = "update reserved (staffname,ename,date_issued,quantity,condition)Values('" + comboBox2.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "','" + numericUpDown1.Text + "','"+richTextBox1.Text+"')";
                // string query = "UPDATE  reserved SET status= 1 WHERE   s_no = ' "+ comboBox1.Text +"' ";
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                string query = "UPDATE  reserved SET status = 0 WHERE   s_no  LIKE '%" + linkLabel3.Text + "%'  ";
                //Fname LIKE '%" + textBox2.Text + "%' 
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                MessageBox.Show("Equipment un located Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBox2.Text = ""; textBox4.Text = ""; textBox3.Clear();
                textBox1.Clear();

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex);
            }
        }
            else
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("enter serial number ! ", "Administtator Says", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
