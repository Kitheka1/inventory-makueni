using Microsoft.Office.Interop.Access.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inventory
{
    public partial class main_menu : Form
    {
        public main_menu()
        {
            InitializeComponent();
        }
        Thread th;

        private void open(object obj)
        {
            //Application.Run(new staff());
        }
        private void logout_btn_Click(object sender, EventArgs e)
        {
            login login = new login();
            this.Close();
            login.Show();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void update_Click(object sender, EventArgs e)
        {
            paid_orders_panel.Controls.Clear();
            Equipment practise = new Equipment();

            practise.TopLevel = false;
            paid_orders_panel.Controls.Add(practise);
           
            practise.BringToFront();
            practise.Show();
        
        }

        private void add_Click(object sender, EventArgs e)
        {
            paid_orders_panel.Controls.Clear();
            staff practise = new staff();
            
            practise.TopLevel=false;
            paid_orders_panel.Controls.Add(practise);
            
            practise.BringToFront();
            practise.Show();
            
        }

        private void delete_Click(object sender, EventArgs e)
        {
            paid_orders_panel.Controls.Clear();
            un_locate practise = new un_locate();

            practise.TopLevel = false;
            paid_orders_panel.Controls.Add(practise);
            practise.BringToFront();
            practise.Show();
        }

        private void paid_orders_Click(object sender, EventArgs e)
        {
            paid_orders_panel.Controls.Clear();
            Reserved_Machine practise = new Reserved_Machine();

            practise.TopLevel = false;
            paid_orders_panel.Controls.Add(practise);
            practise.BringToFront();
            practise.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToString("hh:mm:ss");
            label3.Text = DateTime.Now.ToString("dddd");
            label4.Text = DateTime.Now.ToString("dd:MM:yyyy");
        }

        private void main_menu_Load(object sender, EventArgs e)
        {
            label8.Text = Class2.Role;
            //users
            label6.Text = Class1.Uname;
            timer1.Start();
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            paid_orders_panel.Controls.Clear();
            if (label8.Text == "admin") { button1.Enabled = false; } else 
            { button1.Enabled = true; 

            superadmin practise = new superadmin();

            practise.TopLevel = false;
            paid_orders_panel.Controls.Add(practise);
            practise.BringToFront();
            practise.Show();
            }
        }

        private void unpaid_orders_Click(object sender, EventArgs e)
        {
            paid_orders_panel.Controls.Clear();
            summary practise = new summary();

            practise.TopLevel = false;
            paid_orders_panel.Controls.Add(practise);
            practise.BringToFront();
            practise.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            paid_orders_panel.Controls.Clear();
            practise practise = new practise();

            practise.TopLevel = false;
            paid_orders_panel.Controls.Add(practise);
            practise.BringToFront();
            practise.Show();
        }
    }
}
