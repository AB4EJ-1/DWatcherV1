using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Mail;
using System.Security;
using Newtonsoft.Json;

namespace dwatcher
{
    /*
    class HeardReport
    {
        public string station_heard;
        public string station_reporting;
        public string date_time_reported;
        public string mode_computed;
        public string country;
        public string report_key;
        public string solar_flux_value;
        public string distance_value;
        public string band_value;
        public string heard_latitude_value;
        public string heard_longitude_value;
        public string reporting_latitude_value;
        public string reporting_longitude_value;
        public string frequency_value;
        public string timeGMT_value;
        public string country_reporting;
        public string dataSource;
    }
    */


    public partial class DWatcher : Form
    {

        public int pauseTime;
        public string dstarSource;
        public string DXSource;
        string[] callsign;
        bool nowcancel;
        private WindowsFormsSynchronizationContext m_SynchronizationContext;

        public DWatcher()
        {
            InitializeComponent();

            nowcancel = false;

            m_SynchronizationContext = (WindowsFormsSynchronizationContext)WindowsFormsSynchronizationContext.Current;
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = false;
            toolStripMenuItem9.Checked = true;
            toolStripMenuItem10.Checked = false;
            toolStripMenuItem11.Checked = false;
            toolStripMenuItem12.Checked = false;
            toolStripMenuItem13.Checked = false;
            pauseTime = 30;
            this.Text = this.Text + " - Version: " + Application.ProductVersion;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            callsign = new string[6];
            this.watchDstar.Checked = Properties.Settings.Default.watchdstar;
            this.watchDX.Checked = Properties.Settings.Default.watchdx;

            this.textBox1.Text = Properties.Settings.Default.call1;
            this.textBox2.Text = Properties.Settings.Default.call2;
            this.textBox3.Text = Properties.Settings.Default.call3;
            this.textBox4.Text = Properties.Settings.Default.call4;
            this.textBox5.Text = Properties.Settings.Default.call5;
            this.textBox6.Text = Properties.Settings.Default.call6;

        }

        private void dataSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new Sources();

            f.ShowDialog();

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            toolStripMenuItem7.Checked = true;
            toolStripMenuItem8.Checked = false;
            toolStripMenuItem9.Checked = false;
            toolStripMenuItem10.Checked = false;
            toolStripMenuItem11.Checked = false;
            toolStripMenuItem12.Checked = false;
            toolStripMenuItem13.Checked = false;

            pauseTime = 10;

        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = true;
            toolStripMenuItem9.Checked = false;
            toolStripMenuItem10.Checked = false;
            toolStripMenuItem11.Checked = false;
            toolStripMenuItem12.Checked = false;
            toolStripMenuItem13.Checked = false;
            pauseTime = 20;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = false;
            toolStripMenuItem9.Checked = true;
            toolStripMenuItem10.Checked = false;
            toolStripMenuItem11.Checked = false;
            toolStripMenuItem12.Checked = false;
            toolStripMenuItem13.Checked = false;
            pauseTime = 30;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = false;
            toolStripMenuItem9.Checked = false;
            toolStripMenuItem10.Checked = true;
            toolStripMenuItem11.Checked = false;
            toolStripMenuItem12.Checked = false;
            toolStripMenuItem13.Checked = false;
            pauseTime = 60;
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = false;
            toolStripMenuItem9.Checked = false;
            toolStripMenuItem10.Checked = false;
            toolStripMenuItem11.Checked = true;
            toolStripMenuItem12.Checked = false;
            toolStripMenuItem13.Checked = false;
            pauseTime = 120;
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = false;
            toolStripMenuItem9.Checked = false;
            toolStripMenuItem10.Checked = false;
            toolStripMenuItem11.Checked = false;
            toolStripMenuItem12.Checked = true;
            toolStripMenuItem13.Checked = false;
            pauseTime = 180;
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            toolStripMenuItem7.Checked = false;
            toolStripMenuItem8.Checked = false;
            toolStripMenuItem9.Checked = false;
            toolStripMenuItem10.Checked = false;
            toolStripMenuItem11.Checked = false;
            toolStripMenuItem12.Checked = false;
            toolStripMenuItem13.Checked = true;
            pauseTime = 300;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nowcancel = false;
            string webInfo = "";
            string dxInfo = "";

            var context = TaskScheduler.FromCurrentSynchronizationContext();
            textBox1.Font = new Font(textBox1.Font, FontStyle.Regular);  // reset this in case user is doing monitoring
            textBox2.Font = new Font(textBox2.Font, FontStyle.Regular);
            textBox3.Font = new Font(textBox3.Font, FontStyle.Regular);
            textBox4.Font = new Font(textBox4.Font, FontStyle.Regular);
            textBox5.Font = new Font(textBox5.Font, FontStyle.Regular);
            textBox5.Font = new Font(textBox6.Font, FontStyle.Regular);
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";

            if (!watchDstar.Checked & !watchDX.Checked) // user turned off both, so we default to dstar
            {
                watchDstar.Checked = true;
                Properties.Settings.Default.watchdstar = true;
                Properties.Settings.Default.Save();
                return;
            }


            //        Task.Factory.StartNew(() =>
            //      {
            this.statusBox.Text = "starting";
            Thread.Sleep(500);
            //      }, token, TaskCreationOptions.None, context);
            getCallsigns();
            Task task = Task.Factory.StartNew(() =>
                {
                    var token = Task.Factory.CancellationToken;
                    while (1 == 1)
                    {

                        if (Properties.Settings.Default.watchdstar)
                        {

                            Task.Factory.StartNew(() =>
                            {
                                this.statusBox.Text = "Getting Dstar info";

                            }, token, TaskCreationOptions.None, context);

                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                                   | SecurityProtocolType.Tls11
                                                                   | SecurityProtocolType.Tls12
                                                                   | SecurityProtocolType.Ssl3;
                            WebRequest request = WebRequest.Create(Properties.Settings.Default.dstarSource);
                            WebResponse response = request.GetResponse();

                            // Get the stream containing content returned by the server.
                            Stream dataStream = response.GetResponseStream();
                            // Open the stream using a StreamReader for easy access.
                            StreamReader reader = new StreamReader(dataStream);
                            // Read the content.
                            string responseFromServer = reader.ReadToEnd();
                            // Display the content.
                            //   Console.WriteLine(responseFromServer);         // debugging only                           
                            webInfo = responseFromServer;
                            reader.Close();
                            response.Close();
                            reader.Dispose();
                            response.Dispose();


                            //     Thread.Sleep(5000);
                            Task.Factory.StartNew(() =>
                            {
                                this.statusBox.Text = string.Concat("bytes received:", Convert.ToString(webInfo.Length));

                            }, token, TaskCreationOptions.None, context);
                        }
                        List<HeardReport> reportList = new List<HeardReport>();

                        if (Properties.Settings.Default.watchdx)
                        {
                            Task.Factory.StartNew(() =>
                            {
                                this.statusBox.Text = "Getting DXCluster info";

                            }, token, TaskCreationOptions.None, context);
                            Thread.Sleep(1000);
                            WebRequest request = WebRequest.Create(Properties.Settings.Default.DXSource);
                            WebResponse response = request.GetResponse();

                            // Get the stream containing content returned by the server.
                            Stream dataStream = response.GetResponseStream();
                            // Open the stream using a StreamReader for easy access.
                            StreamReader reader = new StreamReader(dataStream);
                            // Read the content.
                            string responseFromServer = reader.ReadToEnd();
                            // Display the content.
                            //   Console.WriteLine(responseFromServer);                                    
                            dxInfo = responseFromServer;

                            reportList = JsonConvert.DeserializeObject<List<HeardReport>>(dxInfo);

                        }

                        getCallsigns();
                        for (int i = 0; i < 6; i++)
                        {
                            if (callsign[i].Length < 3) continue;  // no callsign here
                            if (nowcancel) break;
                            string callsign_target = callsign[i];

                            try
                            {

                                //   var token1 = Task.Factory.CancellationToken;
                                Task.Factory.StartNew(() =>
                                {
                                    this.statusBox.Text = string.Concat("monitoring started ", Convert.ToString(i));

                                }, token, TaskCreationOptions.None, context);




                                // handle case where user wants to monitor dstar

                                if (Properties.Settings.Default.watchdstar)
                                {

                                    Task.Factory.StartNew(() =>
                                    {
                                        this.statusBox.Text = "checking dstar";

                                    }, token, TaskCreationOptions.None, context);
                                    //  Thread.Sleep(2000);

                                    //KV4S 05/01/2019 - attempt to find exact calls vs slight matches. exp: N4LX vs N4LXT
                                    int sloc = webInfo.IndexOf("<b>" + callsign_target + "</b>");  // where callsign entry starts
                                    if (sloc >= 1)
                                    {
                                        //     Task.Factory.StartNew(() =>
                                        //     {
                                        //      this.statusBox.Text = "found";

                                        //   }, token, TaskCreationOptions.None, context);

                                        int sloc_end = webInfo.IndexOf("</b>", sloc + 1);  // where callsign ends
                                        string call_entry = webInfo.Substring(sloc + 3, sloc_end - (sloc + 3));
                                        if (call_entry != callsign_target)
                                        {
                                            continue;
                                        }
                                        int time_loc = webInfo.IndexOf("<td>", sloc_end);
                                        int time_end = webInfo.IndexOf("</td>", time_loc);
                                        string time_entry = webInfo.Substring(time_loc + 4, time_end - time_loc - 8);
                                        int spoint = webInfo.IndexOf(">", time_end + 6); // where next <td> ends
                                        int spoint2 = webInfo.IndexOf(">", spoint + 5);
                                        int rptr_end = webInfo.IndexOf("</a>", spoint2);
                                        string rptr_entry = webInfo.Substring(spoint2 + 1, rptr_end - spoint2 - 1);
                                        rptr_entry = rptr_entry.PadRight(24);
                                        int locptr = webInfo.IndexOf("<td>", rptr_end);
                                        int location_end = webInfo.IndexOf("</td>", locptr);
                                        string location = webInfo.Substring(locptr + 4, location_end - locptr - 4);
                                        location = location.Substring(0, Math.Min(24, location.Length));
                                        location = location.PadLeft(30, ' ');

                                        // location = string.Format("{0,24}", location);

                                        DateTime dt = Convert.ToDateTime(time_entry.Substring(0, 17));

                                        DateTime nowDate = DateTime.UtcNow;

                                        TimeSpan ts = nowDate - dt;

                                        double difference_mins = ts.TotalMinutes;
                                        string report;
                                        bool timeoutExceeded = false;

                                        if (difference_mins > Convert.ToDouble(Properties.Settings.Default.timeOut))
                                        {
                                            report = "";
                                            timeoutExceeded = true;
                                        }
                                        else
                                        {

                                            string result = String.Format("{0,6:###.0}", difference_mins);

                                            report = string.Format("{0,-18} {1,-20} {2,26} {3,8}", time_entry, rptr_entry, location, result);
                                        }

                                        switch (i)
                                        {
                                            case 0:
                                                // this.textBox7.Text = report;
                                                Task.Factory.StartNew(() => this.textBox7.Text = report, token, TaskCreationOptions.None, context);
                                                if (timeoutExceeded)
                                                {
                                                    textBox1.Font = new Font(textBox1.Font, FontStyle.Regular);
                                                }
                                                // report this via email only the first time we see it
                                                // the text box's font property is used to control this
                                                else
                                                {
                                                    if (this.textBox1.Font.Bold == false)
                                                    {
                                                        sendEmail(callsign_target, rptr_entry);
                                                        Task.Factory.StartNew(() => textBox1.Font = new Font(textBox1.Font, FontStyle.Bold), token, TaskCreationOptions.None, context);
                                                    }
                                                }
                                                break;

                                            case 1:
                                                //this.textBox8.Text = report;
                                                Task.Factory.StartNew(() => this.textBox8.Text = report, token, TaskCreationOptions.None, context);
                                                if (timeoutExceeded)
                                                {
                                                    textBox2.Font = new Font(textBox2.Font, FontStyle.Regular);
                                                }
                                                // report this via email only the first time we see it
                                                // the text box's font property is used to control this
                                                else
                                                {
                                                    if (this.textBox2.Font.Bold == false)
                                                    {
                                                        sendEmail(callsign_target, rptr_entry);
                                                        Task.Factory.StartNew(() => textBox2.Font = new Font(textBox2.Font, FontStyle.Bold), token, TaskCreationOptions.None, context);
                                                    }
                                                }
                                                break;

                                            case 2:
                                                //this.textBox9.Text = report;
                                                Task.Factory.StartNew(() => this.textBox9.Text = report, token, TaskCreationOptions.None, context);
                                                if (timeoutExceeded)
                                                {
                                                    textBox3.Font = new Font(textBox3.Font, FontStyle.Regular);
                                                }
                                                // report this via email only the first time we see it
                                                // the text box's font property is used to control this
                                                else
                                                {
                                                    if (this.textBox3.Font.Bold == false)
                                                    {
                                                        sendEmail(callsign_target, rptr_entry);
                                                        Task.Factory.StartNew(() => textBox3.Font = new Font(textBox3.Font, FontStyle.Bold), token, TaskCreationOptions.None, context);
                                                    }
                                                }
                                                break;
                                            case 3:
                                                //this.textBox10.Text = report;
                                                Task.Factory.StartNew(() => this.textBox10.Text = report, token, TaskCreationOptions.None, context);
                                                if (timeoutExceeded)
                                                {
                                                    textBox4.Font = new Font(textBox4.Font, FontStyle.Regular);
                                                }
                                                // report this via email only the first time we see it
                                                // the text box's font property is used to control this
                                                else
                                                {
                                                    if (this.textBox4.Font.Bold == false)
                                                    {
                                                        sendEmail(callsign_target, rptr_entry);
                                                        Task.Factory.StartNew(() => textBox4.Font = new Font(textBox4.Font, FontStyle.Bold), token, TaskCreationOptions.None, context);
                                                    }
                                                }
                                                break;
                                            case 4:
                                                //this.textBox11.Text = report;
                                                Task.Factory.StartNew(() => this.textBox11.Text = report, token, TaskCreationOptions.None, context);
                                                if (timeoutExceeded)
                                                {
                                                    textBox5.Font = new Font(textBox5.Font, FontStyle.Regular);
                                                }
                                                // report this via email only the first time we see it
                                                // the text box's font property is used to control this
                                                else
                                                {
                                                    if (this.textBox5.Font.Bold == false)
                                                    {
                                                        sendEmail(callsign_target, rptr_entry);
                                                        Task.Factory.StartNew(() => textBox5.Font = new Font(textBox5.Font, FontStyle.Bold), token, TaskCreationOptions.None, context);
                                                    }
                                                }
                                                break;
                                            case 5:
                                                //this.textBox12.Text = report;
                                                Task.Factory.StartNew(() => this.textBox12.Text = report, token, TaskCreationOptions.None, context);
                                                if (timeoutExceeded)
                                                {
                                                    textBox6.Font = new Font(textBox6.Font, FontStyle.Regular);
                                                }
                                                // report this via email only the first time we see it
                                                // the text box's font property is used to control this
                                                else
                                                {
                                                    if (this.textBox6.Font.Bold == false)
                                                    {
                                                        sendEmail(callsign_target, rptr_entry);
                                                        Task.Factory.StartNew(() => textBox6.Font = new Font(textBox6.Font, FontStyle.Bold), token, TaskCreationOptions.None, context);
                                                    }
                                                }
                                                break;



                                        }


                                    }  // end of section if (sloc >= 1)
                                       //          else
                                       //         {
                                       //            Task.Factory.StartNew(() =>
                                       //              {
                                       //                  this.statusBox.Text =  callsign_target + "not found in dstar";

                                    //              }, token, TaskCreationOptions.None, context);
                                    //             Thread.Sleep(5000);
                                    //          }

                                    //         Task.Factory.StartNew(() =>
                                    //          {
                                    //             this.statusBox.Text = "exit dstar section";

                                    //         }, token, TaskCreationOptions.None, context);
                                    //          Thread.Sleep(5000);

                                } // end of section handling dstar


                                //      Task.Factory.StartNew(() =>
                                //     {
                                //           this.statusBox.Text = "preparing to check DX";

                                //   }, token, TaskCreationOptions.None, context);
                                //     Thread.Sleep(5000);

                                // handle case where user wants to monitor DX cluster
                                if (Properties.Settings.Default.watchdx)
                                {

                                    //  Task.Factory.StartNew(() =>
                                    // {
                                    //      this.statusBox.Text = "reached search of rep";

                                    //  }, token, TaskCreationOptions.None, context);
                                    //  Thread.Sleep(5000);
                                    foreach (HeardReport rep in reportList)
                                    {

                                        //    Task.Factory.StartNew(() =>
                                        //    {
                                        //        this.statusBox.Text = "Looking at " + rep.station_heard;

                                        //     }, token, TaskCreationOptions.None, context);
                                        //  Thread.Sleep(1000);

                                        if (rep.station_heard == callsign_target)
                                        {
                                            //            Task.Factory.StartNew(() =>
                                            //            {
                                            //               this.statusBox.Text = "FOUND "+ callsign_target;

                                            //          }, token, TaskCreationOptions.None, context);
                                            //          Thread.Sleep(2000);
                                            string convertedDate = rep.date_time_reported.Substring(5, 2) + "/" +
                                                    rep.date_time_reported.Substring(8, 2) + "/" +
                                                    rep.date_time_reported.Substring(2, 2) + " " +
                                                    rep.date_time_reported.Substring(11, 5) + ":00";


                                            DateTime dt = Convert.ToDateTime(convertedDate);

                                            DateTime nowDate = DateTime.UtcNow;

                                            TimeSpan ts = nowDate - dt;

                                            double difference_mins = ts.TotalMinutes;
                                            string report;
                                            bool timeoutExceeded = false;

                                            //     if (difference_mins > Convert.ToDouble(Properties.Settings.Default.timeOut))
                                            //     {
                                            //         report = "";
                                            //          timeoutExceeded = true;
                                            //     }
                                            //      else
                                            //      {

                                            string result = String.Format("{0,6:###.0}", difference_mins);

                                            report = string.Format("{0,-18} {1,-20} {2,34} {3,8}", convertedDate, rep.frequency_value, rep.country, result);
                                            //      }

                                            switch (i)
                                            {
                                                case 0:
                                                    this.textBox7.Text = report;
                                                    if (timeoutExceeded)
                                                    {
                                                        textBox1.Font = new Font(textBox1.Font, FontStyle.Regular);
                                                    }
                                                    // report this via email only the first time we see it
                                                    // the text box's font property is used to control this
                                                    else
                                                    {
                                                        if (this.textBox1.Font.Bold == false)
                                                        {
                                                            sendEmail(this.textBox1.Text, rep.frequency_value);
                                                            textBox1.Font = new Font(textBox1.Font, FontStyle.Bold);
                                                        }
                                                    }
                                                    break;

                                                case 1:
                                                    this.textBox8.Text = report;
                                                    if (timeoutExceeded)
                                                    {
                                                        textBox2.Font = new Font(textBox2.Font, FontStyle.Regular);
                                                    }
                                                    // report this via email only the first time we see it
                                                    // the text box's font property is used to control this
                                                    else
                                                    {
                                                        if (this.textBox2.Font.Bold == false)
                                                        {
                                                            sendEmail(this.textBox2.Text, rep.frequency_value);
                                                            textBox2.Font = new Font(textBox2.Font, FontStyle.Bold);
                                                        }
                                                    }
                                                    break;

                                                case 2:
                                                    this.textBox9.Text = report;
                                                    if (timeoutExceeded)
                                                    {
                                                        textBox3.Font = new Font(textBox3.Font, FontStyle.Regular);
                                                    }
                                                    // report this via email only the first time we see it
                                                    // the text box's font property is used to control this
                                                    else
                                                    {
                                                        if (this.textBox3.Font.Bold == false)
                                                        {
                                                            sendEmail(this.textBox1.Text, rep.frequency_value);
                                                            textBox3.Font = new Font(textBox3.Font, FontStyle.Bold);
                                                        }
                                                    }
                                                    break;
                                                case 3:
                                                    this.textBox10.Text = report;
                                                    if (timeoutExceeded)
                                                    {
                                                        textBox4.Font = new Font(textBox4.Font, FontStyle.Regular);
                                                    }
                                                    // report this via email only the first time we see it
                                                    // the text box's font property is used to control this
                                                    else
                                                    {
                                                        if (this.textBox4.Font.Bold == false)
                                                        {
                                                            sendEmail(this.textBox4.Text, rep.frequency_value);
                                                            textBox4.Font = new Font(textBox4.Font, FontStyle.Bold);
                                                        }
                                                    }
                                                    break;
                                                case 4:
                                                    this.textBox11.Text = report;
                                                    if (timeoutExceeded)
                                                    {
                                                        textBox5.Font = new Font(textBox5.Font, FontStyle.Regular);
                                                    }
                                                    // report this via email only the first time we see it
                                                    // the text box's font property is used to control this
                                                    else
                                                    {
                                                        if (this.textBox5.Font.Bold == false)
                                                        {
                                                            sendEmail(this.textBox5.Text, rep.frequency_value);
                                                            textBox5.Font = new Font(textBox5.Font, FontStyle.Bold);
                                                        }
                                                    }
                                                    break;
                                                case 5:
                                                    this.textBox12.Text = report;
                                                    if (timeoutExceeded)
                                                    {
                                                        textBox6.Font = new Font(textBox6.Font, FontStyle.Regular);
                                                    }
                                                    // report this via email only the first time we see it
                                                    // the text box's font property is used to control this
                                                    else
                                                    {
                                                        if (this.textBox6.Font.Bold == false)
                                                        {
                                                            sendEmail(this.textBox6.Text, rep.frequency_value);
                                                            textBox6.Font = new Font(textBox6.Font, FontStyle.Bold);
                                                        }
                                                    }
                                                    break;



                                            }


                                        }  // end of callsign search loop

                                    }

                                }



                            }
                            catch (Exception e1)
                            {

                                Task.Factory.StartNew(() =>
                                {
                                    this.statusBox.Text = "error! ";

                                }, token, TaskCreationOptions.None, context);
                                Thread.Sleep(2000);




                                /* Console.WriteLine("An error occurred: '{0}'", e1);*/
                            } // ignore if the response fails





                        } // end of for loop

                        //KV4S 04/10/18 - adding to help with debugging.
                        //this.statusBox.Text = "waiting";                     
                        Task.Factory.StartNew(() => this.statusBox.Text = "waiting ", token, TaskCreationOptions.None, context);

                        if (nowcancel) break;
                        Thread.Sleep(1000 * pauseTime);
                        //this.statusBox.Text = Convert.ToString(1000 * pauseTime);
                        Task.Factory.StartNew(() => this.statusBox.Text = Convert.ToString(1000 * pauseTime), token, TaskCreationOptions.None, context);
                        // Thread.Sleep(2000);
                    }  // end of infinite loop 

                });

            //           for ( int i = 0; i < 6; i++ )
            //           {
            //           }
        }

        private void sendEmail(string theCallsign, string theNode)
        {
            if (this.enableNotification.Checked)  // skip this if user has not enabled monitoring
            {
                try
                {
                    // Console.WriteLine("try to send...");
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(Properties.Settings.Default.SMTPserver);

                    mail.From = new MailAddress(Properties.Settings.Default.emailFrom);
                    mail.To.Add(Properties.Settings.Default.emailTo);
                    mail.Subject = "dwatcher: heard " + theCallsign.ToUpper();

                    mail.Body = " on Node: " + theNode;
                    //  mail.IsBodyHtml = true;
                    //  string htmlBody;

                    //  htmlBody = "Template HTML here";

                    //mail.Body = htmlBody;
                    SmtpServer.Timeout = 30000;
                    SmtpServer.Port = Convert.ToInt16(Properties.Settings.Default.SMTPport);

                    SmtpServer.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.SMTPuid, Properties.Settings.Default.SMTPpw);

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

        }

        private void statusBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            var token = Task.Factory.CancellationToken;
            nowcancel = true;
            Task.Factory.StartNew(() =>
            {
                this.statusBox.Text = "Stopped";

            }, token, TaskCreationOptions.None, context);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox7.Text = "";
            getCallsigns();
            Properties.Settings.Default.call1 = textBox1.Text;
            Properties.Settings.Default.Save();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox8.Text = "";
            getCallsigns();
            Properties.Settings.Default.call2 = textBox2.Text;
            Properties.Settings.Default.Save();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox9.Text = "";
            getCallsigns();
            Properties.Settings.Default.call3 = textBox3.Text;
            Properties.Settings.Default.Save();
        }



        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox10.Text = "";
            getCallsigns();
            Properties.Settings.Default.call4 = textBox4.Text;
            Properties.Settings.Default.Save();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox11.Text = "";
            getCallsigns();
            Properties.Settings.Default.call5 = textBox5.Text;
            Properties.Settings.Default.Save();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.call6 = textBox6.Text;
            Properties.Settings.Default.Save();
            textBox12.Text = "";
            getCallsigns();
        }

        private void getCallsigns()
        {
            callsign[0] = textBox1.Text.ToUpper();
            callsign[1] = textBox2.Text.ToUpper();
            callsign[2] = textBox3.Text.ToUpper();
            callsign[3] = textBox4.Text.ToUpper();
            callsign[4] = textBox5.Text.ToUpper();
            callsign[5] = textBox6.Text.ToUpper();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm1 helpForm = new HelpForm1();
            helpForm.Show();
        }

        private void notificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            emailForm emailForm = new emailForm();
            emailForm.Show();
        }

        private void watchDstar_CheckedChanged(object sender, EventArgs e)
        {
            //         if (!watchDstar.Checked & !watchDX.Checked) // user turned off both, so we default to dstar
            //    {
            //         watchDstar.Checked = true;
            //        Properties.Settings.Default.watchdstar = true;
            //         Properties.Settings.Default.Save();
            //         return;
            //     }
            Properties.Settings.Default.watchdstar = watchDstar.Checked;
            //  Properties.Settings.Default.watchdx = watchDX.Checked;
            Properties.Settings.Default.Save();

        }

        private void watchDX_CheckedChanged(object sender, EventArgs e)
        {

            //     Properties.Settings.Default.watchdstar = watchDstar.Checked;
            Properties.Settings.Default.watchdx = watchDX.Checked;
            Properties.Settings.Default.Save();
        }

        private void enableNotification_CheckedChanged(object sender, EventArgs e)
        {
            if (this.enableNotification.Checked & missingData())
            {
                MessageBox.Show("Cannot enable email notification; missing data. Please fill in all the fields in the Configure/Notification screen.");
                this.enableNotification.Checked = false;
            }
        }

        private bool missingData()
        {
            if (Properties.Settings.Default.emailFrom == "" | Properties.Settings.Default.emailTo == "" |
                Properties.Settings.Default.SMTPserver == "" | Properties.Settings.Default.SMTPport == "" |
                Properties.Settings.Default.SMTPuid == "" | Properties.Settings.Default.SMTPpw == "")
                return true;
            return false;
        }

    }


    class HeardReport
    {
        public string station_heard;
        public string station_reporting;
        public string date_time_reported;
        public string mode_computed;
        public string country;
        public string report_key;
        public string solar_flux_value;
        public string distance_value;
        public string band_value;
        public string heard_latitude_value;
        public string heard_longitude_value;
        public string reporting_latitude_value;
        public string reporting_longitude_value;
        public string frequency_value;
        public string timeGMT_value;
        public string country_reporting;
        public string dataSource;
    }
}
