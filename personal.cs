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
    public partial class personal : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\Documents\\INVENTORY.accdb");

        public personal()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult df = MessageBox.Show("Confirm Exit ?", "Makueni County Assembly", MessageBoxButtons.YesNo);
            if (df == DialogResult.Yes) { this.Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand cmd = con.CreateCommand();
                OleDbDataAdapter ad = new OleDbDataAdapter();
                con.Open();
               string query = "select  staffname, ename, date_issued, quantity, condition from equipment";
// sql = "SELECT ItemID, `Parts`,`Location`, `ComputerSet` FROM `tblbrand` b,`tblitems` i, `tblparts` p, `tbllocation` l,tblcompset c WHERE b.`BrandID`=i.`BrandID` AND i.`PartsID`=p.`PartsID` AND i.`LocationID`=l.`LocationID` AND i.CompSetID=c.CompSetID AND Status='Good'";
                //
                cmd.CommandText = query;
                con.Close();
                ad.SelectCommand = cmd;
                DataTable df = new DataTable();
                ad.Fill(df);
                BindingSource bs = new BindingSource();
                bs.DataSource = df;

                dataGridView1.DataSource = bs;
             //   dataGridView1.Columns[0].HeaderText = "pid";
                dataGridView1.Columns[0].HeaderText = "Staff Name";
               // dataGridView1.Columns[2].HeaderText = "department";
                dataGridView1.Columns[1].HeaderText = "Equipment Name";
                dataGridView1.Columns[2].HeaderText = "Date Issued";
                dataGridView1.Columns[3].HeaderText = "Quantity";
                dataGridView1.Columns[4].HeaderText = "Condition";
                
                ad.Update(df);


                // trial
                /*
               OleDbCommand cmd1 = con.CreateCommand();
                OleDbDataAdapter add = new OleDbDataAdapter();
                con.Open();
                string queryy = "select pid,  department from  staff";
                
                cmd1.CommandText = queryy;
                con.Close();
                add.SelectCommand = cmd1;
                DataTable ddf = new DataTable();
                add.Fill(ddf);
                BindingSource bbs = new BindingSource();
                bbs.DataSource = ddf;

                dataGridView1.DataSource = bbs;
                dataGridView1.Columns[5].HeaderText = "pid";
                
                dataGridView1.Columns[6].HeaderText = "department";
               

                add.Update(ddf); 
                */


            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
