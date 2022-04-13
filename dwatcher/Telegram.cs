using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using System.Windows.Forms;

namespace dwatcher
{
    public partial class Telegram : Form
    {
        public Telegram()
        {
            InitializeComponent();
        }

        private void Telegram_Load(object sender, EventArgs e)
        {
            this.tbBotToken.Text = Properties.Settings.Default.BotToken;
            this.tbDestID.Text = Properties.Settings.Default.DestinationID;
        }

        private void btnTSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BotToken = this.tbBotToken.Text; 
            Properties.Settings.Default.DestinationID = this.tbDestID.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnTeleTest_Click(object sender, EventArgs e)
        {
            TelegramBotClient bot = new TelegramBotClient(this.tbBotToken.Text);
            bot.SendTextMessageAsync(this.tbDestID.Text, "dwatcher test message");
            Thread.Sleep(2000);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
