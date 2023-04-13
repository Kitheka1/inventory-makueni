using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace inventory
{
    public partial class emailcs : Form
    {
        public emailcs()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string to,pass, from ,mail;
            to = (textBox1.Text).ToString(); from = (textBox2.Text).ToString(); pass = (textBox3.Text).ToString(); mail = (richTextBox1.Text).ToString();
            MailMessage ms = new MailMessage();
            ms.To.Add(to);
            ms.From = new MailAddress(from);
            ms.Body = mail;
            ms.Subject = "MAKUENI COUNTY ASSEMBLY";
            SmtpClient sl = new SmtpClient("smtp.gmail.com");
            sl.EnableSsl = true;
            sl.Port = 587;
            sl.DeliveryMethod = SmtpDeliveryMethod.Network;
            sl.Credentials = new NetworkCredential(from, pass);
            try
            {
                sl.Send(ms);
                MessageBox.Show("Email Send Successfully", "Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult df = MessageBox.Show("Confirm Exit", "Makueni County Assembly", MessageBoxButtons.YesNo);
            if (df == DialogResult.Yes) { this.Close(); }
        }
    }
}
