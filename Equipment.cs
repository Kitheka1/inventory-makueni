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

namespace inventory
{
    public partial class Equipment : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\Documents\\INVENTORY.accdb");
   
        public Equipment()
        {
            InitializeComponent();
        }
       

        private void button3_Click(object sender, EventArgs e)
        {
            
                this.Close();
           
    }
      
        private void Equipment_Load(object sender, EventArgs e)
        {
           
            //
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            string query = "select pid, Fname, Lname from staff ";
            cmd.CommandText = query;
            OleDbDataReader df = cmd.ExecuteReader();
            while (df.Read())
            {
                comboBox2.Items.Add(df[0].ToString() + "   " + df[1].ToString()+"  " + df[2].ToString());
            }

            con.Close();


          
            con.Open();
            

            OleDbCommand oleDbCommand = new OleDbCommand("select  s_no,category,condition from reserved where status=0", con);

            OleDbDataAdapter da = new OleDbDataAdapter(oleDbCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.Width = 40;
            checkBoxColumn.Name = "Check";
            checkBoxColumn.HeaderText = "";
            dataGridView1.Columns.Insert(0, checkBoxColumn);

            con.Close();



            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                bool select1 = Convert.ToBoolean(row.Cells["Check"].Value);
                if(select1)
                {
                    OleDbCommand oleDbCommand = new OleDbCommand("Insert into equipment(staffname,ename,s_no,category,condition)Values('" + comboBox2.Text + "',@s_no,@category,@condition,'" + dateTimePicker1.Text + "')",con);
                    oleDbCommand.Parameters.AddWithValue("@staffname", row.Cells["staffname"].Value);
                    oleDbCommand.Parameters.AddWithValue("category", row.Cells["category"].Value);
                    oleDbCommand.Parameters.AddWithValue("s_no", row.Cells["s_no"].Value);
                    oleDbCommand.Parameters.AddWithValue("category", row.Cells["category"].Value);
                    oleDbCommand.Parameters.AddWithValue("condition", row.Cells["condition"].Value);
                    oleDbCommand.Parameters.AddWithValue("@date_issued", row.Cells["date_issued"].Value);
                   
                    con.Open();
                    oleDbCommand.ExecuteNonQuery();
                    con.Close();
                }
            }
            MessageBox.Show("DONE");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            un_locate nm = new un_locate();
            nm.Show();
            this.Close();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            staff gh = new staff();
            gh.Show();this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
