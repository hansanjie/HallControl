namespace MainApp.Component
{
    partial class AppPanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStartPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtPad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConfig = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cbOldVersion = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(601, 221);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 31);
            this.button1.TabIndex = 14;
            this.button1.Text = "删除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Delete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "启动文件名：";
            // 
            // txtStartPath
            // 
            this.txtStartPath.Location = new System.Drawing.Point(151, 80);
            this.txtStartPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtStartPath.Name = "txtStartPath";
            this.txtStartPath.Size = new System.Drawing.Size(395, 22);
            this.txtStartPath.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "应用程序名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(151, 24);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(395, 22);
            this.txtName.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(469, 133);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 31);
            this.button2.TabIndex = 19;
            this.button2.Text = "选择...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.PadFile_Click);
            // 
            // txtPad
            // 
            this.txtPad.Location = new System.Drawing.Point(151, 133);
            this.txtPad.Margin = new System.Windows.Forms.Padding(4);
            this.txtPad.Name = "txtPad";
            this.txtPad.Size = new System.Drawing.Size(305, 22);
            this.txtPad.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 140);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "pad端安装文件：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 188);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "pad端配置文件：";
            // 
            // txtConfig
            // 
            this.txtConfig.Location = new System.Drawing.Point(151, 184);
            this.txtConfig.Margin = new System.Windows.Forms.Padding(4);
            this.txtConfig.Name = "txtConfig";
            this.txtConfig.Size = new System.Drawing.Size(305, 22);
            this.txtConfig.TabIndex = 21;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(469, 183);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 31);
            this.button3.TabIndex = 22;
            this.button3.Text = "选择...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.PadConfig_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(551, 87);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 17);
            this.label8.TabIndex = 23;
            this.label8.Text = "*（包括后缀名）";
            // 
            // cbOldVersion
            // 
            this.cbOldVersion.AutoSize = true;
            this.cbOldVersion.Checked = true;
            this.cbOldVersion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOldVersion.Location = new System.Drawing.Point(30, 230);
            this.cbOldVersion.Name = "cbOldVersion";
            this.cbOldVersion.Size = new System.Drawing.Size(110, 21);
            this.cbOldVersion.TabIndex = 24;
            this.cbOldVersion.Text = "兼容L02旧版";
            this.cbOldVersion.UseVisualStyleBackColor = true;
            this.cbOldVersion.Visible = false;
            // 
            // AppPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbOldVersion);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtConfig);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtPad);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStartPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AppPanel";
            this.Size = new System.Drawing.Size(692, 273);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStartPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtPad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtConfig;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbOldVersion;
    }
}
