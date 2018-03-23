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

namespace dwatcher
{
    public partial class Sources : Form
    {
        public Sources()
        {
            InitializeComponent();

            this.textBox1.Text = Properties.Settings.Default.dstarSource;
            this.textBox2.Text = Properties.Settings.Default.DXSource;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.dstarSource = this.textBox1.Text;
            Properties.Settings.Default.DXSource = this.textBox2.Text;
            Properties.Settings.Default.Save();

            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "http://dstarusers.org";
            this.textBox2.Text = "http://dxdisplay.caps.ua.edu:82/api/stations_heard_list";
            Properties.Settings.Default.dstarSource = this.textBox1.Text;
            Properties.Settings.Default.DXSource = this.textBox2.Text;
            Properties.Settings.Default.Save();

        }
    }
}
