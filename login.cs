using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Configuration;
using System.Windows.Forms;
using OpenQA.Selenium;

namespace inventory
{
    public partial class login : Form
    {
        // OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\OneDrive\\Documents\\INVENTORY.accdb");
        //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ERIC-LAMELA\Documents\INVENTORY.accdb
        public login()
        {
            InitializeComponent();
        }
        public static string username;

        private void button1_Click(object sender, EventArgs e)
            
        {
           // if ((bool)(comboBox1.SelectedItem = null)) { MessageBox.Show("Select Role", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
            if (textBox1.Text=="" && textBox2.Text == "" || comboBox1.Text =="") {
                MessageBox.Show("Enter credentials", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();comboBox1.Text = "";textBox1.Clear();textBox1.Focus(); }
            else { 
            string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\Documents\\INVENTORY.accdb";
            //conn = ConfigurationManager.ConnectionStrings["employee1ConnectionString"].ToString();
            OleDbConnection objsqlconn = new OleDbConnection(conn);
            objsqlconn.Open();

            OleDbCommand cmd = new OleDbCommand("select * from addadmin where admin_name=@admin_name and password=@password", objsqlconn);
            cmd.Parameters.AddWithValue("@username", textBox1.Text);
            cmd.Parameters.AddWithValue("@password", textBox2.Text);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
           
                string cv = comboBox1.SelectedItem.ToString();
          
            if (dt.Rows.Count > 0)
            {
                //if (dt.Rows[0]["role"].ToString() == "admin")
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                        if (dt.Rows[m]["role"].ToString() == cv)
                        {
                            if (comboBox1.SelectedIndex == 0)
                            {
                                Class2.Role = comboBox1.Text;
                                Class1.Uname = textBox1.Text;
                                // MessageBox.Show("Login Success", "Success", MessageBoxIcon.Information); ;
                                this.Close();
                                main_menu frm = new main_menu();
                                frm.ShowDialog();
                                
                            }
                            if (comboBox1.SelectedIndex == 1)
                            {
                                Class1.Uname = textBox1.Text;
                                Class2.Role = comboBox1.Text;
                                this.Close();
                                username = textBox1.Text;
                                main_menu frnm = new main_menu();
                                frnm.ShowDialog();

                              
                            }
                        }
                    }


                }
            else { MessageBox.Show("Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Clear(); textBox2.Clear();comboBox1.Text = "";
                }

                //MessageBox.Show("updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult df = MessageBox.Show("Confirm Closing ?", "Makueni County Assembly", MessageBoxButtons.YesNo);
            if (df == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void login_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*') 
            {
                pictureBox2.BringToFront();
                textBox2.PasswordChar = '\0';
            }
            else {
                textBox2.PasswordChar = '*';
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
           /* if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsNumber(e.KeyChar)) 
            { e.Handled = false;
               comboBox1.Text = ""; }
            {
                MessageBox.Show("Select Role", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Text = "";
                    
            }*/
            //if (!char.IsControl(e.KeyChar) && !char.IsNumber(e.KeyChar)) { e.Handled = true; }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            DialogResult df = MessageBox.Show("Confirm Closing ?", "Makueni County Assembly", MessageBoxButtons.YesNo);
            if (df == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Green;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.BackColor = Color.Crimson;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Transparent;
        }
    }
}
/*using System.Data.OleDb;

string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=mydatabase.accdb;";
string query = "INSERT INTO mytable (column1, column2, column3) " +
               "SELECT column1, column2, column3 FROM mytable " +
               "WHERE some_condition = true";

using (OleDbConnection connection = new OleDbConnection(connectionString))
{
    connection.Open();
    using (OleDbCommand command = new OleDbCommand(query, connection))
    {
        command.ExecuteNonQuery();
    }
}
Replace mydatabase.accdb with the name of your Access database file, mytable with the name of the table you want to insert into, and column1, column2, column3 with the names of the columns you want to insert data into. Replace some_condition with the WHERE clause condition that selects the rows you want to insert.

finally
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
*/



