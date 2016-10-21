namespace MainApp
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbZone = new System.Windows.Forms.ListBox();
            this.lbPoint = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnZoneAdd = new System.Windows.Forms.Button();
            this.btnZoneMinus = new System.Windows.Forms.Button();
            this.btnPointMinus = new System.Windows.Forms.Button();
            this.btnAddPoint = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lbMsg = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHallName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbZone
            // 
            this.lbZone.AllowDrop = true;
            this.lbZone.FormattingEnabled = true;
            this.lbZone.ItemHeight = 12;
            this.lbZone.Location = new System.Drawing.Point(27, 79);
            this.lbZone.Name = "lbZone";
            this.lbZone.Size = new System.Drawing.Size(294, 316);
            this.lbZone.TabIndex = 0;
            this.lbZone.DragDrop += new System.Windows.Forms.DragEventHandler(this.LB_DragDrop);
            this.lbZone.DragOver += new System.Windows.Forms.DragEventHandler(this.LB_DragOver);
            this.lbZone.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LB_MouseDoubleClick);
            this.lbZone.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LB_MouseDown);
            // 
            // lbPoint
            // 
            this.lbPoint.AllowDrop = true;
            this.lbPoint.FormattingEnabled = true;
            this.lbPoint.ItemHeight = 12;
            this.lbPoint.Location = new System.Drawing.Point(382, 79);
            this.lbPoint.Name = "lbPoint";
            this.lbPoint.Size = new System.Drawing.Size(311, 316);
            this.lbPoint.TabIndex = 1;
            this.lbPoint.DragDrop += new System.Windows.Forms.DragEventHandler(this.LB_DragDrop);
            this.lbPoint.DragOver += new System.Windows.Forms.DragEventHandler(this.LB_DragOver);
            this.lbPoint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LB_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "展区";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(387, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "展点";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(717, 427);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Update_Click);
            // 
            // btnZoneAdd
            // 
            this.btnZoneAdd.Location = new System.Drawing.Point(247, 398);
            this.btnZoneAdd.Name = "btnZoneAdd";
            this.btnZoneAdd.Size = new System.Drawing.Size(33, 23);
            this.btnZoneAdd.TabIndex = 5;
            this.btnZoneAdd.Text = "+";
            this.btnZoneAdd.UseVisualStyleBackColor = true;
            this.btnZoneAdd.Click += new System.EventHandler(this.btnZoneAdd_Click);
            // 
            // btnZoneMinus
            // 
            this.btnZoneMinus.Location = new System.Drawing.Point(286, 398);
            this.btnZoneMinus.Name = "btnZoneMinus";
            this.btnZoneMinus.Size = new System.Drawing.Size(33, 23);
            this.btnZoneMinus.TabIndex = 6;
            this.btnZoneMinus.Text = "-";
            this.btnZoneMinus.UseVisualStyleBackColor = true;
            this.btnZoneMinus.Click += new System.EventHandler(this.btnZoneMinus_Click);
            // 
            // btnPointMinus
            // 
            this.btnPointMinus.Location = new System.Drawing.Point(660, 397);
            this.btnPointMinus.Name = "btnPointMinus";
            this.btnPointMinus.Size = new System.Drawing.Size(33, 23);
            this.btnPointMinus.TabIndex = 8;
            this.btnPointMinus.Text = "-";
            this.btnPointMinus.UseVisualStyleBackColor = true;
            this.btnPointMinus.Click += new System.EventHandler(this.btnPointMinus_Click);
            // 
            // btnAddPoint
            // 
            this.btnAddPoint.Location = new System.Drawing.Point(621, 397);
            this.btnAddPoint.Name = "btnAddPoint";
            this.btnAddPoint.Size = new System.Drawing.Size(33, 23);
            this.btnAddPoint.TabIndex = 7;
            this.btnAddPoint.Text = "+";
            this.btnAddPoint.UseVisualStyleBackColor = true;
            this.btnAddPoint.Click += new System.EventHandler(this.btnAddPoint_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(556, 397);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "编辑";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Edit_Click);
            // 
            // lbMsg
            // 
            this.lbMsg.AutoSize = true;
            this.lbMsg.ForeColor = System.Drawing.Color.Red;
            this.lbMsg.Location = new System.Drawing.Point(17, 435);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Size = new System.Drawing.Size(0, 12);
            this.lbMsg.TabIndex = 10;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(724, 386);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "展厅名称：";
            // 
            // txtHallName
            // 
            this.txtHallName.Location = new System.Drawing.Point(85, 18);
            this.txtHallName.Name = "txtHallName";
            this.txtHallName.Size = new System.Drawing.Size(150, 21);
            this.txtHallName.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 455);
            this.Controls.Add(this.txtHallName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lbMsg);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnPointMinus);
            this.Controls.Add(this.btnAddPoint);
            this.Controls.Add(this.btnZoneMinus);
            this.Controls.Add(this.btnZoneAdd);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbPoint);
            this.Controls.Add(this.lbZone);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "展厅内容中控交互平台系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbZone;
        private System.Windows.Forms.ListBox lbPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnZoneAdd;
        private System.Windows.Forms.Button btnZoneMinus;
        private System.Windows.Forms.Button btnPointMinus;
        private System.Windows.Forms.Button btnAddPoint;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lbMsg;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHallName;
    }
}

