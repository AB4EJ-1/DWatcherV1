using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Security;

namespace dwatcher
{
    

    public partial class emailForm : Form
    {

        public int theTimeout; 

        public emailForm()
        {
            InitializeComponent();
        }

        private void email_Load(object sender, EventArgs e)
        {
          //  theTimeout = 0;
            this.SMTPBox.Text = Properties.Settings.Default.SMTPserver ;
            this.emailFrom.Text = Properties.Settings.Default.emailFrom;
            this.emailTo.Text =Properties.Settings.Default.emailTo ;
            this.SMTPport.Text =Properties.Settings.Default.SMTPport  ;
            this.SMTPuid.Text = Properties.Settings.Default.SMTPuid  ;
            this.SMTPpw.Text = Properties.Settings.Default.SMTPpw ;
            this.timeoutBox.Text = Properties.Settings.Default.timeOut;
            theTimeout = Convert.ToInt16(this.timeoutBox.Text);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SMTPserver = this.SMTPBox.Text;
            Properties.Settings.Default.emailFrom = this.emailFrom.Text;
            Properties.Settings.Default.emailTo = this.emailTo.Text;
            Properties.Settings.Default.SMTPport = this.SMTPport.Text;
            Properties.Settings.Default.SMTPuid = this.SMTPuid.Text;
            Properties.Settings.Default.SMTPpw = this.SMTPpw.Text;
            Properties.Settings.Default.timeOut = this.timeoutBox.Text;
            Properties.Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               // Console.WriteLine("try to send...");
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(this.SMTPBox.Text);

                mail.From = new MailAddress(this.emailFrom.Text);
                mail.To.Add(this.emailTo.Text);
                mail.Subject = "dwatcher test message";

                mail.Body = "dwatcher test message";
                //  mail.IsBodyHtml = true;
                //  string htmlBody;

                //  htmlBody = "Template HTML here";

                //mail.Body = htmlBody;
                SmtpServer.Timeout = 30000;
                SmtpServer.Port = Convert.ToInt16(this.SMTPport.Text);

                SmtpServer.Credentials = new System.Net.NetworkCredential(this.SMTPuid.Text, this.SMTPpw.Text);

                SmtpServer.EnableSsl = true;
                //  SmtpServer.Host = "outbound.att.net";

                SmtpServer.Send(mail);
                Console.WriteLine("send complete");
            }
            catch (Exception ex)
            {
             //   Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
            }

        }

        private void SMTPBox_TextChanged(object sender, EventArgs e)
        {

        }



        private void timeoutBox_Leave(object sender, EventArgs e)
        {
            if (theTimeout == 0) return;
            int theEntry;
            bool canConvert = int.TryParse(this.timeoutBox.Text, out theEntry);
            if (canConvert == true)
            {
                if (theEntry < 10 | theEntry > 55)
                {
                    MessageBox.Show("Please enter a number between 10 and 55 (minutes)");

                    this.timeoutBox.Text = Properties.Settings.Default.timeOut;
                    return;
                }
                else
                {
                    theTimeout = theEntry;
                    return;
                }
            }
            MessageBox.Show("Please enter a number between 10 and 55 (minutes)");

            this.timeoutBox.Text = Properties.Settings.Default.timeOut;
           
        }

        private void timeoutBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
