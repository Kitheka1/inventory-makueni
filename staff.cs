using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;



namespace inventory
{
    public partial class staff : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\Documents\\INVENTORY.accdb");
        public staff()
        {
            InitializeComponent();
            //string temp;
        }
        DataTable table;
       
        public void populateDGV()
        {
            // populate the datagridview
            string selectQuery = "select pid, Fname, Lname, department from staff";
            DataTable table = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, con);
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void staff_Load(object sender, EventArgs e)
        {
            //number of staff
            try
            {
                // All reserved equipment
                OleDbConnection tom = new OleDbConnection();
                con.Open();
                OleDbCommand cmdd = new OleDbCommand("select COUNT(pid) from staff", con);

                //SELECT COUNT(*) AS UniqueRecordsCount FROM (SELECT DISTINCT [ColumnName] FROM [SampleTable]) AS T1
                var count = cmdd.ExecuteScalar();
                label5.Text = count.ToString();
                con.Close();
            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }
            timer1.Start();
            try
            {
                OleDbCommand cmd = con.CreateCommand();
                OleDbDataAdapter ad = new OleDbDataAdapter();
                con.Open();
                string query = "select pid, Fname, Lname, department from staff ";
                cmd.CommandText = query;
                con.Close();
                ad.SelectCommand = cmd;
                DataTable df = new DataTable();
                ad.Fill(df);
                BindingSource bs = new BindingSource();
                bs.DataSource = df;
                //comboBox2.Items.Add(df[0].ToString() + "   " + df[1].ToString());
                dataGridView1.DataSource = bs;
                dataGridView1.Columns[0].HeaderText = "Personal Number";
                dataGridView1.Columns[1].HeaderText = "First Name";
                dataGridView1.Columns[2].HeaderText = "Last Name";
                dataGridView1.Columns[3].HeaderText = "Department";
                ad.Update(df);



            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }

            searchData("");
        }
        public void searchData(string valueToSearch)
        {
            string query = "SELECT pid,Fname,Lname,department FROM  staff WHERE pid LIKE '%" + textBox2.Text + "%'";
            OleDbCommand command = new OleDbCommand(query, con);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }





        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox1.Text != "" && textBox5.Text != "" && textBox3.Text != "" && textBox4.Text != "") { 
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            string query = "UPDATE  staff SET Fname='" + textBox1.Text + "',Lname='" + textBox5.Text + "',department='" + textBox3.Text + "'  WHERE pid LIKE '%" + textBox2.Text + "%'";
                //  string query = "UPDATE  staff SET Fname='" + textBox1.Text + "',Lname='" + textBox5.Text + "',department='" + textBox3.Text + "'  WHERE Fname LIKE '%" + textBox2.Text + "%'  OR Lname LIKE '%" + textBox2.Text + "%'  OR pid LIKE '%" + textBox2.Text + "%'";
                cmd.CommandText = query;
            // WHERE Fname LIKE '%" + textBox2.Text + "%'  OR Lname LIKE '%" + textBox2.Text + "%'  OR pid LIKE '%" + textBox2.Text + "%'
            cmd.ExecuteNonQuery();
            MessageBox.Show("updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox1.Clear(); textBox4.Clear(); textBox5.Clear(); textBox3.Clear(); textBox4.Focus();

            con.Close(); populateDGV();
            }
            else { MessageBox.Show("Enter personal number of Staff  in the search box ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
             if ( textBox1.Text != "" && textBox5.Text != "" && textBox3.Text != "" && textBox4.Text != "")
             {
            textBox4.Enabled = true;
            OleDbCommand cmd = con.CreateCommand();



            OleDbCommand cmd1 = new OleDbCommand("select * from staff where pid =@pid", con);
            cmd1.Parameters.AddWithValue("@pid", textBox4.Text);
            OleDbDataReader reader1;
            con.Open();
            reader1 = cmd1.ExecuteReader();

            if (reader1.Read())
            {
                MessageBox.Show("Staff Exists in Database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
                textBox1.Clear(); textBox4.Clear(); textBox5.Clear(); textBox3.Clear(); textBox4.Focus();
            }

            else
            {
                con.Close();
                cmd.CommandText = "Insert into staff(pid,Fname,Lname,department)Values('" + textBox4.Text + "','" + textBox1.Text + "','" + textBox5.Text + "','" + textBox3.Text + "')";

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                    populateDGV();
                    DialogResult df = MessageBox.Show("Do You Wish Add Another Staff ?", "Staff Registered !!", MessageBoxButtons.YesNo);
                //
                if (df == DialogResult.Yes) { this.Show(); textBox1.Clear(); textBox4.Clear(); textBox5.Clear(); textBox3.Clear(); textBox4.Focus(); } else { this.Close(); }


                con.Close();
            }
        }

            else { MessageBox.Show("Empty fields ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            { 
                e.Handled = true;
                MessageBox.Show("enter numbers only ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            { e.Handled = true; MessageBox.Show("enter character only ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            textBox4.Enabled = false;
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                //string query = "select * from  staff where Fname   ='" + textBox2.Text + "'";
                string query = "SELECT * FROM  staff WHERE pid LIKE '%" + textBox2.Text + "%'";
                // string query = "SELECT * FROM  staff WHERE Fname LIKE '%" + textBox2.Text + "%'  OR Lname LIKE '%" + textBox2.Text + "%'  OR pid LIKE '%" + textBox2.Text + "%'";
                //"SELECT * FROM `tbltransfer` WHERE Parts Like '%" + cboParts.Text + "%'"
                cmd.CommandText = query;
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    textBox4.Text = reader["pid"].ToString();
                    textBox3.Text = reader["department"].ToString();
                    textBox5.Text = reader["Lname"].ToString();
                    textBox1.Text = reader["Fname"].ToString();
                }
                 else
                {
                    MessageBox.Show("Personal Number not found", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.Close();
            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }

            //trial
            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
           
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //REFRESH BUTTON
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            button2.Enabled = false;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox4.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox5.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        Bitmap bmp;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            int height = dataGridView1.Height;
            dataGridView1.Height = dataGridView1.Rows.Count * dataGridView1.RowTemplate.Height * 2;
            bmp = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(bmp, new System.Drawing.Rectangle(0,0, dataGridView1.Width, dataGridView1.Height));
            dataGridView1.Height = height;
            printPreviewDialog1.ShowDialog();






          }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Equipment equipment = new Equipment();
            equipment.Show();
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
            //printDocument1.Print();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("enter personal number ! ", "Administtator Says", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            { e.Handled = true; MessageBox.Show("enter character only ! ", "Administrator Says", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            { e.Handled = true; MessageBox.Show("enter character only ! ", "Administrator Says", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            Close();

           // DialogResult df = MessageBox.Show("Confirm Exit", "Makueni County Assembly", MessageBoxButtons.YesNo);
           // if (df == DialogResult.Yes) { this.Close(); }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox5.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox2.Text != "")
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("DELETE * FROM staff WHERE  pid LIKE '%" + textBox2.Text + "%'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Staff Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                textBox4.Clear();
                textBox5.Clear();
                textBox3.Clear();
                textBox2.Clear();
                textBox1.Clear();
            }
            else { MessageBox.Show("Enter personal number of Staff  in the search box ! ", "Administrator Says", MessageBoxButtons.OK, MessageBoxIcon.Error); textBox2.Focus(); }

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.DarkCyan;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

        }
    }
}
