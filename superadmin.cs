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
using Microsoft.Office.Interop.Access.Dao;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using static iText.Svg.SvgConstants;
using System.CodeDom.Compiler;
using DataTable = System.Data.DataTable;
using System.Data.SqlClient;
using System.Collections;

namespace inventory
{
    public partial class superadmin : Form
    {
        OleDbCommand command;
      
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\Documents\\INVENTORY.accdb");

        public superadmin()
        {
            InitializeComponent();
        }
        public void populateDGV()
        {
            // populate the datagridview
            string selectQuery = "SELECT admin_name, password, role FROM addadmin";
            DataTable table = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, con);
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void department_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            textBox3.Enabled = false; textBox4.Enabled = false;
            //end

            //textBox2.Enabled = false;
            try
            {
                OleDbCommand cmd = con.CreateCommand();
                OleDbDataAdapter ad = new OleDbDataAdapter();
                con.Open();
                string queruy = "SELECT admin_name, password, role FROM addadmin";

                cmd.CommandText = queruy;

                OleDbCommand cmdd = new OleDbCommand("select COUNT(admin_name) from addadmin", con);
                 var count = cmdd.ExecuteScalar();
                populateDGV();
                label4.Text = count.ToString();


                con.Close();
                ad.SelectCommand = cmd;
                DataTable df = new DataTable();
                ad.Fill(df);
                BindingSource bs = new BindingSource();
                bs.DataSource = df;
                dataGridView1.DataSource = bs;
                dataGridView1.Columns[0].HeaderText = "Admin Username";
                dataGridView1.Columns[1].HeaderText = " Password";
                dataGridView1.Columns[2].HeaderText = " Role";
                ad.Update(df);


               
            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\Documents\\INVENTORY.accdb;";
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string insertQuery = "insert into addadmin(admin_name,role)Values('" + textBox1.Text + "','"+comboBox1.Text+"')";
            OleDbCommand command = new OleDbCommand(insertQuery, connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@admin_name", textBox1.Text);
            command.Parameters.AddWithValue("@role", comboBox1.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Admin Added", "Congrats", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            textBox1.Clear();comboBox1.Text = "";

            populateDGV();
        }
        



       
        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Green;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) { textBox2.Enabled = true;
                textBox3.Enabled = true;  textBox4.Enabled = false;
            }
            else
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false; textBox4.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("UPDATE addadmin SET password='" + textBox2.Text + "' WHERE  admin_name = '" + textBox4.Text + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
                populateDGV();
            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand cmd = new OleDbCommand("DELETE * FROM addadmin WHERE  admin_name = '" + textBox4.Text + "'", con) ;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Admin Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
            populateDGV();

        }
    }
}
