namespace dwatcher
{
    partial class Telegram
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Telegram));
            this.btnTeleTest = new System.Windows.Forms.Button();
            this.btnTSave = new System.Windows.Forms.Button();
            this.tbDestID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbBotToken = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTeleTest
            // 
            this.btnTeleTest.Location = new System.Drawing.Point(183, 93);
            this.btnTeleTest.Name = "btnTeleTest";
            this.btnTeleTest.Size = new System.Drawing.Size(113, 27);
            this.btnTeleTest.TabIndex = 19;
            this.btnTeleTest.Text = "Send test message";
            this.btnTeleTest.UseVisualStyleBackColor = true;
            this.btnTeleTest.Click += new System.EventHandler(this.btnTeleTest_Click);
            // 
            // btnTSave
            // 
            this.btnTSave.Location = new System.Drawing.Point(20, 93);
            this.btnTSave.Name = "btnTSave";
            this.btnTSave.Size = new System.Drawing.Size(140, 28);
            this.btnTSave.TabIndex = 18;
            this.btnTSave.Text = "Use and Save";
            this.btnTSave.UseVisualStyleBackColor = true;
            this.btnTSave.Click += new System.EventHandler(this.btnTSave_Click);
            // 
            // tbDestID
            // 
            this.tbDestID.Location = new System.Drawing.Point(104, 40);
            this.tbDestID.Name = "tbDestID";
            this.tbDestID.Size = new System.Drawing.Size(320, 20);
            this.tbDestID.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Destination ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "BotToken";
            // 
            // tbBotToken
            // 
            this.tbBotToken.Location = new System.Drawing.Point(104, 2);
            this.tbBotToken.Name = "tbBotToken";
            this.tbBotToken.Size = new System.Drawing.Size(320, 20);
            this.tbBotToken.TabIndex = 14;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(320, 95);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 24);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Telegram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 141);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnTeleTest);
            this.Controls.Add(this.btnTSave);
            this.Controls.Add(this.tbDestID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbBotToken);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Telegram";
            this.Text = "Configure Telegram Notification";
            this.Load += new System.EventHandler(this.Telegram_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTeleTest;
        private System.Windows.Forms.Button btnTSave;
        private System.Windows.Forms.TextBox tbDestID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbBotToken;
        private System.Windows.Forms.Button btnClose;
    }
}