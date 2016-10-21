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
    public partial class AddPointForm : Form
    {
        private PointInfo _pointInfo;
        public AddPointForm()
        {
            InitializeComponent();
        }

        public PointInfo PointInfo { get { return _pointInfo; } }

        private void Add_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                string pName = textBox1.Text.Trim();
                string id = IDCreater.CreateID();
                _pointInfo = new PointInfo { Name = pName, ID = id };
                this.DialogResult = DialogResult.OK;
                return;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
