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
    public partial class practise : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\Documents\\INVENTORY.accdb");

        public practise()
        {
            InitializeComponent();
        }

        private void practise_Load(object sender, EventArgs e)
        {
            try
            {
                // All reserved equipment
                OleDbConnection tomm = new OleDbConnection();
                con.Open();

                //laptop
                //OleDbCommand cmdm = new OleDbCommand("SELECT COUNT(category) FROM (SELECT DISTINCT category FROM reserved)", con);
                OleDbCommand cmdm = new OleDbCommand("select COUNT(Fname) from staff  ", con);

                var count = cmdm.ExecuteScalar();
              label6.Text = count.ToString();
                //mouse

                OleDbCommand m = new OleDbCommand("select COUNT(s_no) from reserved WHERE  status =0 ", con);

                var cunt = m.ExecuteScalar();
               label7.Text = cunt.ToString();
                //printer
                OleDbCommand printer = new OleDbCommand("select COUNT(s_no) from reserved WHERE  condition ='damaged' ", con);

                var cnt = printer.ExecuteScalar();
                label8.Text = cnt.ToString();
                //monitor
                OleDbCommand monitor = new OleDbCommand("select COUNT(staffname) from equipment ", con);

                var ct = monitor.ExecuteScalar();
                label9.Text = ct.ToString();
                //cable
                

                con.Close();
            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }

        }
    }
}
