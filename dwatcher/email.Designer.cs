namespace dwatcher
{
    partial class emailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(emailForm));
            this.label1 = new System.Windows.Forms.Label();
            this.SMTPBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.emailFrom = new System.Windows.Forms.TextBox();
            this.emailTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SMTPport = new System.Windows.Forms.TextBox();
            this.SMTPuid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SMTPpw = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.timeoutBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SMTP Server hostname";
            // 
            // SMTPBox
            // 
            this.SMTPBox.Location = new System.Drawing.Point(157, 9);
            this.SMTPBox.Name = "SMTPBox";
            this.SMTPBox.Size = new System.Drawing.Size(390, 20);
            this.SMTPBox.TabIndex = 1;
            this.SMTPBox.TextChanged += new System.EventHandler(this.SMTPBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "EMail (from)";
            // 
            // emailFrom
            // 
            this.emailFrom.Location = new System.Drawing.Point(158, 40);
            this.emailFrom.Name = "emailFrom";
            this.emailFrom.Size = new System.Drawing.Size(388, 20);
            this.emailFrom.TabIndex = 3;
            // 
            // emailTo
            // 
            this.emailTo.Location = new System.Drawing.Point(157, 78);
            this.emailTo.Name = "emailTo";
            this.emailTo.Size = new System.Drawing.Size(390, 20);
            this.emailTo.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "EMail (to)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "SMTP Server port#";
            // 
            // SMTPport
            // 
            this.SMTPport.Location = new System.Drawing.Point(157, 117);
            this.SMTPport.Name = "SMTPport";
            this.SMTPport.Size = new System.Drawing.Size(73, 20);
            this.SMTPport.TabIndex = 7;
            // 
            // SMTPuid
            // 
            this.SMTPuid.Location = new System.Drawing.Point(157, 160);
            this.SMTPuid.Name = "SMTPuid";
            this.SMTPuid.Size = new System.Drawing.Size(267, 20);
            this.SMTPuid.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "SMTP User-ID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "SMTP Password";
            // 
            // SMTPpw
            // 
            this.SMTPpw.Location = new System.Drawing.Point(158, 198);
            this.SMTPpw.Name = "SMTPpw";
            this.SMTPpw.PasswordChar = '*';
            this.SMTPpw.Size = new System.Drawing.Size(266, 20);
            this.SMTPpw.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 28);
            this.button1.TabIndex = 12;
            this.button1.Text = "Use and Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(208, 251);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 27);
            this.button2.TabIndex = 13;
            this.button2.Text = "Send test message";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(390, 253);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 24);
            this.button3.TabIndex = 14;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(305, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Timeout (minutes)";
            // 
            // timeoutBox
            // 
            this.timeoutBox.Location = new System.Drawing.Point(422, 117);
            this.timeoutBox.Name = "timeoutBox";
            this.timeoutBox.Size = new System.Drawing.Size(72, 20);
            this.timeoutBox.TabIndex = 16;
            this.timeoutBox.TextChanged += new System.EventHandler(this.timeoutBox_TextChanged);
            this.timeoutBox.Leave += new System.EventHandler(this.timeoutBox_Leave);
            // 
            // emailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 285);
            this.Controls.Add(this.timeoutBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SMTPpw);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SMTPuid);
            this.Controls.Add(this.SMTPport);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.emailTo);
            this.Controls.Add(this.emailFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SMTPBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "emailForm";
            this.Text = "Configure Email Notification";
            this.Load += new System.EventHandler(this.email_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SMTPBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox emailFrom;
        private System.Windows.Forms.TextBox emailTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SMTPport;
        private System.Windows.Forms.TextBox SMTPuid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox SMTPpw;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox timeoutBox;
    }
}