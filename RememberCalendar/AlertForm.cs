using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RememberCalendar
{
    public partial class AlertForm : Form
    {
        public FormRememberCalendar parentForm{ get; set; }
        public System.Windows.Forms.Screen screen { get; set; }
        public AlertForm(FormRememberCalendar parentForm, System.Windows.Forms.Screen screen)
        {
            this.parentForm = parentForm;
            this.screen = screen;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AlertForm_Load(object sender, EventArgs e)
        {
            this.Location = screen.WorkingArea.Location;
            this.TopMost = true;

        }

        private void AlertForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentForm.AlertFormClosed(this);
        }
    }
}
