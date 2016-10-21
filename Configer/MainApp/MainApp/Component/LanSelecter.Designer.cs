namespace MainApp.Component
{
    partial class LanSelecter
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
            this.ck_CN = new System.Windows.Forms.CheckBox();
            this.ck_En = new System.Windows.Forms.CheckBox();
            this.ck_Fr = new System.Windows.Forms.CheckBox();
            this.ck_Sp = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ck_CN
            // 
            this.ck_CN.AutoSize = true;
            this.ck_CN.Location = new System.Drawing.Point(9, 10);
            this.ck_CN.Name = "ck_CN";
            this.ck_CN.Size = new System.Drawing.Size(48, 16);
            this.ck_CN.TabIndex = 0;
            this.ck_CN.Tag = "cn";
            this.ck_CN.Text = "中文";
            this.ck_CN.UseVisualStyleBackColor = true;
            this.ck_CN.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            // 
            // ck_En
            // 
            this.ck_En.AutoSize = true;
            this.ck_En.Location = new System.Drawing.Point(70, 10);
            this.ck_En.Name = "ck_En";
            this.ck_En.Size = new System.Drawing.Size(48, 16);
            this.ck_En.TabIndex = 1;
            this.ck_En.Tag = "en";
            this.ck_En.Text = "英文";
            this.ck_En.UseVisualStyleBackColor = true;
            this.ck_En.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            // 
            // ck_Fr
            // 
            this.ck_Fr.AutoSize = true;
            this.ck_Fr.Location = new System.Drawing.Point(132, 10);
            this.ck_Fr.Name = "ck_Fr";
            this.ck_Fr.Size = new System.Drawing.Size(48, 16);
            this.ck_Fr.TabIndex = 2;
            this.ck_Fr.Tag = "fr";
            this.ck_Fr.Text = "法国";
            this.ck_Fr.UseVisualStyleBackColor = true;
            this.ck_Fr.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            // 
            // ck_Sp
            // 
            this.ck_Sp.AutoSize = true;
            this.ck_Sp.Location = new System.Drawing.Point(194, 10);
            this.ck_Sp.Name = "ck_Sp";
            this.ck_Sp.Size = new System.Drawing.Size(48, 16);
            this.ck_Sp.TabIndex = 3;
            this.ck_Sp.Tag = "sp";
            this.ck_Sp.Text = "西语";
            this.ck_Sp.UseVisualStyleBackColor = true;
            this.ck_Sp.CheckedChanged += new System.EventHandler(this.ck_CheckedChanged);
            // 
            // LanSelecter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ck_Sp);
            this.Controls.Add(this.ck_Fr);
            this.Controls.Add(this.ck_En);
            this.Controls.Add(this.ck_CN);
            this.Name = "LanSelecter";
            this.Size = new System.Drawing.Size(535, 35);
            this.Load += new System.EventHandler(this.LanSelecter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ck_CN;
        private System.Windows.Forms.CheckBox ck_En;
        private System.Windows.Forms.CheckBox ck_Fr;
        private System.Windows.Forms.CheckBox ck_Sp;
    }
}
