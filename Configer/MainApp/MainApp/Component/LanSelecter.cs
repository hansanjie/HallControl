using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainApp.Component
{
    public partial class LanSelecter : UserControl
    {
        private bool _isChanged = false;
        public string SelectedLan
        {
            get 
            {
                StringBuilder lanStringSB = new StringBuilder();
                foreach (var cr in this.Controls)
                {
                    CheckBox lanCk = cr as CheckBox;
                    if (lanCk != null)
                    {
                        if (lanCk.Checked)
                        {
                            lanStringSB.Append(lanCk.Tag.ToString() + ",");
                        }
                    }
                }
                string lanString = lanStringSB.ToString().TrimEnd(',');
                return lanString;
            }
        }

        public LanSelecter()
        {
            InitializeComponent();
        }

        private void LanSelecter_Load(object sender, EventArgs e)
        {
            this.ck_CN.Checked = true;
            this.ck_En.Checked = true;
            _isChanged = false;
        }

        private void ck_CheckedChanged(object sender, EventArgs e)
        {
            _isChanged = true;
        }

        internal bool IsChange()
        {
            return _isChanged;
        }
    }
}
