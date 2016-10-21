namespace PointApp
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
            this.rtb_data = new System.Windows.Forms.RichTextBox();
            this.cbStartType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbStartPath = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbLan = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // rtb_data
            // 
            this.rtb_data.Location = new System.Drawing.Point(25, 23);
            this.rtb_data.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rtb_data.Name = "rtb_data";
            this.rtb_data.Size = new System.Drawing.Size(336, 167);
            this.rtb_data.TabIndex = 0;
            this.rtb_data.Text = "";
            this.rtb_data.Visible = false;
            // 
            // cbStartType
            // 
            this.cbStartType.FormattingEnabled = true;
            this.cbStartType.Items.AddRange(new object[] {
            "video",
            "ppt",
            "webpage",
            "app",
            "image",
            "flash"});
            this.cbStartType.Location = new System.Drawing.Point(107, 211);
            this.cbStartType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbStartType.Name = "cbStartType";
            this.cbStartType.Size = new System.Drawing.Size(160, 24);
            this.cbStartType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 216);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "启动类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 252);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "启动路径：";
            // 
            // lbStartPath
            // 
            this.lbStartPath.AutoSize = true;
            this.lbStartPath.Location = new System.Drawing.Point(111, 253);
            this.lbStartPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbStartPath.Name = "lbStartPath";
            this.lbStartPath.Size = new System.Drawing.Size(132, 17);
            this.lbStartPath.TabIndex = 4;
            this.lbStartPath.Text = "点击选择启动路径...";
            this.lbStartPath.Click += new System.EventHandler(this.SelectDir_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(300, 311);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 31);
            this.button1.TabIndex = 5;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.UpdateStartInfo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 285);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "启动语言：";
            // 
            // cbLan
            // 
            this.cbLan.FormattingEnabled = true;
            this.cbLan.Items.AddRange(new object[] {
            "EN",
            "CN"});
            this.cbLan.Location = new System.Drawing.Point(107, 283);
            this.cbLan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbLan.Name = "cbLan";
            this.cbLan.Size = new System.Drawing.Size(160, 24);
            this.cbLan.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 349);
            this.Controls.Add(this.cbLan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbStartPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbStartType);
            this.Controls.Add(this.rtb_data);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "展点主窗口";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_data;
        private System.Windows.Forms.ComboBox cbStartType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbStartPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLan;
    }
}

