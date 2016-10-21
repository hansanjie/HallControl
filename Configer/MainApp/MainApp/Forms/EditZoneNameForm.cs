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
    public partial class EditZoneNameForm : Form
    {
        private ZoneInfo _zoneInfo;
        public string ZoneName { get; set; }
        public EditZoneNameForm(ZoneInfo info)
        {
            InitializeComponent();
            _zoneInfo = info;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
                _zoneInfo.Name = zName;
                this.DialogResult = DialogResult.OK;
                return;
            }
        }

        private void EditZoneNameForm_Load(object sender, EventArgs e)
        {
            if (_zoneInfo != null)
            {
                this.textBox1.Text = _zoneInfo.Name;
            }
        }
    }
}
