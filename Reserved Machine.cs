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
using System.Diagnostics;

namespace inventory
{
    public partial class Reserved_Machine : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\Documents\\INVENTORY.accdb");

        public Reserved_Machine()

        {
            InitializeComponent();
        }
        private void refresh()
        {

            string query = "select s_no, category, condition from reserved where status=0 ";
            //select s_no, category, quantity, condition from reserved where status =0
            OleDbDataAdapter ad = new OleDbDataAdapter(query, con);

            DataTable df = new DataTable();
            ad.Fill(df);
            BindingSource bs = new BindingSource();
            bs.DataSource = df;
            //comboBox2.Items.Add(df[0].ToString() + "   " + df[1].ToString());
            dataGridView1.DataSource = bs;
            dataGridView1.Columns[0].HeaderText = "Serial Number";
            dataGridView1.Columns[1].HeaderText = "Category";
            dataGridView1.Columns[2].HeaderText = "Condition";
            //  dataGridView1.Columns[3].HeaderText = "Department";
            ad.Update(df);

        }


        private void button3_Click(object sender, EventArgs e)

        {
             this.Close(); 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://makueniassembly.go.ke/");
        }
    

        private void Reserved_Machine_Load(object sender, EventArgs e)
        {
          timer1.Start();
            try { 
           // All reserved equipment
            OleDbConnection tom = new OleDbConnection();
            con.Open();
            OleDbCommand cmdd = new OleDbCommand("select COUNT(category) from reserved WHERE status = 0 ", con);
                //       OleDbCommand cmdd = new OleDbCommand("select COUNT(category) from reserved WHERE category ='mouse' ", con);  
                //SELECT COUNT(*) AS UniqueRecordsCount FROM (SELECT DISTINCT [ColumnName] FROM [SampleTable]) AS T1
                var count = cmdd.ExecuteScalar();
            linkLabel10.Text = count.ToString();
            con.Close();
            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }

            // end

            // laptop
            try
            {
                // All reserved equipment
                OleDbConnection tomm = new OleDbConnection();
                con.Open();

                //laptop
                //OleDbCommand cmdm = new OleDbCommand("SELECT COUNT(category) FROM (SELECT DISTINCT category FROM reserved)", con);
                OleDbCommand cmdm = new OleDbCommand("select COUNT(category) from reserved WHERE category ='laptop' AND status =0 ", con);

                var count = cmdm.ExecuteScalar();
                linkLabel4.Text = count.ToString();
                //mouse

                OleDbCommand m = new OleDbCommand("select COUNT(category) from reserved WHERE category ='mouse' AND status =0 ", con);

                var cunt = m.ExecuteScalar();
                linkLabel6.Text = cunt.ToString();
                //printer
                OleDbCommand printer = new OleDbCommand("select COUNT(category) from reserved WHERE category ='printer' AND status =0 ", con);

                var cnt = printer.ExecuteScalar();
                linkLabel3.Text = cnt.ToString();
                //monitor
                OleDbCommand monitor = new OleDbCommand("select COUNT(category) from reserved WHERE category ='Monitor' AND status =0 ", con);

                var ct = monitor.ExecuteScalar();
                linkLabel2.Text = ct.ToString();
                //cable
                OleDbCommand cable = new OleDbCommand("select COUNT(category) from reserved WHERE category ='cable' AND status =0 ", con);

                var ctt = cable.ExecuteScalar();
                linkLabel7.Text = ctt.ToString();
                // cpu
                OleDbCommand cpu = new OleDbCommand("select COUNT(category) from reserved WHERE category ='System Unit' AND status =0 ", con);

                var cph = cpu.ExecuteScalar();
                linkLabel8.Text = cph.ToString();
                //keyboard
                OleDbCommand KeyBoard = new OleDbCommand("select COUNT(category) from reserved WHERE category ='KeyBoard' AND status =0 ", con);

                var ph = KeyBoard.ExecuteScalar();
                linkLabel9.Text = ph.ToString();


                con.Close();
            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }
            //end laptop

            try
            {
                OleDbCommand cmd = con.CreateCommand();
                OleDbDataAdapter ad = new OleDbDataAdapter();
                con.Open();
                string query = "select s_no, category, quantity, condition from reserved where status =0 ";

                cmd.CommandText = query;
                con.Close();
                ad.SelectCommand = cmd;
                DataTable df = new DataTable();
                ad.Fill(df);
                BindingSource bs = new BindingSource();
                bs.DataSource = df;
                dataGridView1.DataSource = bs;
                dataGridView1.Columns[0].HeaderText = "Serial Number";
                dataGridView1.Columns[1].HeaderText = " Category";
                dataGridView1.Columns[2].HeaderText = "Quantity";
                dataGridView1.Columns[3].HeaderText = "Condition";
                ad.Update(df);



            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if(textBox1.Text!=""&& richTextBox1.Text != "")
         //  using(
            OleDbCommand cmd = con.CreateCommand();
            OleDbCommand cmd1 = new OleDbCommand("select * from reserved where s_no =@s_no", con);
            cmd1.Parameters.AddWithValue("@s_no", textBox1.Text);
            con.Open();
            OleDbDataReader reader1;
         
            reader1 = cmd1.ExecuteReader();
          
            if (reader1.Read())
            {
                MessageBox.Show("Serial Number Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
            


            else
            {
                if (textBox1.Text != "" && richTextBox1.Text != "")
                { 
                    con.Close();
                cmd.CommandText = "Insert into reserved(s_no,category,quantity,condition)Values('" + textBox1.Text + "','" + comboBox1.Text + "','" + numericUpDown1.Text + "','" + richTextBox1.Text + "')";

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("New Equipment Registered", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();richTextBox1.Clear();comboBox1.Text = "";


                con.Close();
                }
                else
                {
                    MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
           
            /*else
            {
                MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text !=""&& richTextBox1.Text != "") { 
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            string query = "UPDATE reserved SET condition='" + richTextBox1.Text + "' WHERE s_no = '" + textBox1.Text + "'";
            cmd.CommandText = query;

            cmd.ExecuteNonQuery();
            MessageBox.Show("updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox1.Clear(); textBox1.Clear();

            con.Close();
            }
            else { MessageBox.Show("Empty fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                string query = "select * from reserved where s_no  ='" + textBox2.Text + "'";
                cmd.CommandText = query;
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader["s_no"].ToString();
                    comboBox1.Text = reader["category"].ToString();
                    numericUpDown1.Text = reader["quantity"].ToString();
                    richTextBox1.Text = reader["condition"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Equipment equipment = new Equipment();
            equipment.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refresh();
        
        }
    }
}
    
