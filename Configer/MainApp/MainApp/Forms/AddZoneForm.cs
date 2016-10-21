using MainApp.Helper;
using MainApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainApp.Forms
{
    public partial class AddZoneForm : Form
    {
        private ZoneInfo _zone;
        public ZoneInfo Zone
        {
            get { return _zone; }
        }
        public AddZoneForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                string zName = textBox1.Text.Trim();
                string id = IDCreater.CreateID();
                _zone = new ZoneInfo { Name = zName, ID=id };
                this.DialogResult = DialogResult.OK;
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
