namespace RememberCalendar
{
    partial class AlertForm
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
            this.labelAlert = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelAlert
            // 
            this.labelAlert.AutoSize = true;
            this.labelAlert.Location = new System.Drawing.Point(100, 69);
            this.labelAlert.Name = "labelAlert";
            this.labelAlert.Size = new System.Drawing.Size(50, 20);
            this.labelAlert.TabIndex = 0;
            this.labelAlert.Text = "label1";
            this.labelAlert.Click += new System.EventHandler(this.label1_Click);
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.labelAlert);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AlertForm";
            this.Text = "AlertForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AlertForm_FormClosed);
            this.Load += new System.EventHandler(this.AlertForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelAlert;
    }
}