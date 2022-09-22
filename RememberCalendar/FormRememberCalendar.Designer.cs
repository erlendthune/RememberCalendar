namespace RememberCalendar
{
    partial class FormRememberCalendar
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.upcomingAppointmentTextBox = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textboxIcsUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxBaseURL = new System.Windows.Forms.TextBox();
            this.labelUpcomingAppointment = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.buttonDismiss = new System.Windows.Forms.Button();
            this.labelUpcomingAppointmentSummary = new System.Windows.Forms.Label();
            this.labelUpcomingAppointmentTime = new System.Windows.Forms.Label();
            this.buttonSnooze = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get calendar RestSharp";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_ClickAsync);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(23, 435);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(988, 80);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // upcomingAppointmentTextBox
            // 
            this.upcomingAppointmentTextBox.Location = new System.Drawing.Point(25, 275);
            this.upcomingAppointmentTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.upcomingAppointmentTextBox.Name = "upcomingAppointmentTextBox";
            this.upcomingAppointmentTextBox.Size = new System.Drawing.Size(986, 126);
            this.upcomingAppointmentTextBox.TabIndex = 3;
            this.upcomingAppointmentTextBox.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(259, 10);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(183, 22);
            this.button2.TabIndex = 4;
            this.button2.Text = "Get calendar HttpClient";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textboxIcsUrl
            // 
            this.textboxIcsUrl.Location = new System.Drawing.Point(147, 70);
            this.textboxIcsUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textboxIcsUrl.Name = "textboxIcsUrl";
            this.textboxIcsUrl.Size = new System.Drawing.Size(826, 23);
            this.textboxIcsUrl.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ics Relative URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 417);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Debug output";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Appointments that will give alerts";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "BaseURL";
            // 
            // textBoxBaseURL
            // 
            this.textBoxBaseURL.Location = new System.Drawing.Point(147, 44);
            this.textBoxBaseURL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxBaseURL.Name = "textBoxBaseURL";
            this.textBoxBaseURL.Size = new System.Drawing.Size(303, 23);
            this.textBoxBaseURL.TabIndex = 10;
            // 
            // labelUpcomingAppointment
            // 
            this.labelUpcomingAppointment.AutoSize = true;
            this.labelUpcomingAppointment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelUpcomingAppointment.Location = new System.Drawing.Point(28, 130);
            this.labelUpcomingAppointment.Name = "labelUpcomingAppointment";
            this.labelUpcomingAppointment.Size = new System.Drawing.Size(245, 15);
            this.labelUpcomingAppointment.TabIndex = 11;
            this.labelUpcomingAppointment.Text = "Relax, no immediate upcoming appointment";
            this.labelUpcomingAppointment.Click += new System.EventHandler(this.label5_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBox1.ForeColor = System.Drawing.Color.Brown;
            this.textBox1.Location = new System.Drawing.Point(515, 5);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(458, 61);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "DISCLAIMER: Do not blame me if you miss an appointment. Instead you can log an is" +
    "sue or make a pull request in the project on GitHUB.";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.linkLabel1.Location = new System.Drawing.Point(528, 44);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(288, 15);
            this.linkLabel1.TabIndex = 14;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/erlendthune/RememberCalendar";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // buttonDismiss
            // 
            this.buttonDismiss.Location = new System.Drawing.Point(428, 123);
            this.buttonDismiss.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDismiss.Name = "buttonDismiss";
            this.buttonDismiss.Size = new System.Drawing.Size(82, 22);
            this.buttonDismiss.TabIndex = 15;
            this.buttonDismiss.Text = "Dismiss";
            this.buttonDismiss.UseVisualStyleBackColor = true;
            this.buttonDismiss.Click += new System.EventHandler(this.buttonDismiss_Click);
            // 
            // labelUpcomingAppointmentSummary
            // 
            this.labelUpcomingAppointmentSummary.AutoSize = true;
            this.labelUpcomingAppointmentSummary.Location = new System.Drawing.Point(28, 179);
            this.labelUpcomingAppointmentSummary.Name = "labelUpcomingAppointmentSummary";
            this.labelUpcomingAppointmentSummary.Size = new System.Drawing.Size(0, 15);
            this.labelUpcomingAppointmentSummary.TabIndex = 16;
            this.labelUpcomingAppointmentSummary.Click += new System.EventHandler(this.label5_Click_1);
            // 
            // labelUpcomingAppointmentTime
            // 
            this.labelUpcomingAppointmentTime.AutoSize = true;
            this.labelUpcomingAppointmentTime.Location = new System.Drawing.Point(28, 208);
            this.labelUpcomingAppointmentTime.Name = "labelUpcomingAppointmentTime";
            this.labelUpcomingAppointmentTime.Size = new System.Drawing.Size(0, 15);
            this.labelUpcomingAppointmentTime.TabIndex = 17;
            // 
            // buttonSnooze
            // 
            this.buttonSnooze.Location = new System.Drawing.Point(323, 124);
            this.buttonSnooze.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSnooze.Name = "buttonSnooze";
            this.buttonSnooze.Size = new System.Drawing.Size(82, 22);
            this.buttonSnooze.TabIndex = 18;
            this.buttonSnooze.Text = "Snooze";
            this.buttonSnooze.UseVisualStyleBackColor = true;
            this.buttonSnooze.Click += new System.EventHandler(this.buttonSnooze_Click);
            // 
            // FormRememberCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 538);
            this.Controls.Add(this.buttonSnooze);
            this.Controls.Add(this.labelUpcomingAppointmentTime);
            this.Controls.Add(this.labelUpcomingAppointmentSummary);
            this.Controls.Add(this.buttonDismiss);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelUpcomingAppointment);
            this.Controls.Add(this.textBoxBaseURL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textboxIcsUrl);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.upcomingAppointmentTextBox);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormRememberCalendar";
            this.Text = "Remember Calendar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private RichTextBox richTextBox1;
        private RichTextBox upcomingAppointmentTextBox;
        private Button button2;
        private TextBox textboxIcsUrl;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBoxBaseURL;
        private Label labelUpcomingAppointment;
        private TextBox textBox1;
        private LinkLabel linkLabel1;
        private Button buttonDismiss;
        private Label labelUpcomingAppointmentSummary;
        private Label labelUpcomingAppointmentTime;
        private Button buttonSnooze;
    }
}