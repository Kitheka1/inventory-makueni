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
using Excel = Microsoft.Office.Interop.Excel;

namespace inventory
{
    public partial class summary : Form
    {

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\ERIC-LAMELA\\Documents\\INVENTORY.accdb");

        public summary()
        {
            InitializeComponent();
        }

        private void summary_Load(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand cmfd = con.CreateCommand();
                OleDbDataAdapter aqd = new OleDbDataAdapter();
                con.Open();
                string query1 = "select staffname, ename,serial, date_issued,quantity,condition from equipment  ";
                cmfd.CommandText = query1;
                con.Close();
                aqd.SelectCommand = cmfd;
                DataTable dfj = new DataTable();
                aqd.Fill(dfj);
                BindingSource bsw = new BindingSource();
                bsw.DataSource = dfj;

                dataGridView1.DataSource = bsw;
                dataGridView1.Columns[0].HeaderText = "Staff  Name";
                dataGridView1.Columns[1].HeaderText = "Equipment";
                dataGridView1.Columns[2].HeaderText = "Serilal No.";
                dataGridView1.Columns[3].HeaderText = "Date Issued";
                dataGridView1.Columns[4].HeaderText = "Quantity";

                dataGridView1.Columns[5].HeaderText = "condition";
                aqd.Update(dfj);



            }
            catch (Exception ex)
            { MessageBox.Show("Error " + ex); }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult df = MessageBox.Show("Exit?", "Makueni County Assembly", MessageBoxButtons.YesNo);
            if (df == DialogResult.Yes) { this.Close(); } else { this.Show(); }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            Int16 i, j;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            for (i = 0; i <= dataGridView1.RowCount - 2; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    xlWorkSheet.Cells[i + 1, j + 1] = dataGridView1[j, i].Value.ToString();
                }
            }

            
          //  xlWorkBook.Name = ("dd", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.SaveAs(@"C:\Users\ERIC-LAMELA\Documents\informations.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

        }
        //oblect
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    
}