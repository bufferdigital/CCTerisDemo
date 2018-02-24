namespace SuperAraneid
{
    partial class FrmStrat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStrat));
            this.txt_url = new System.Windows.Forms.TextBox();
            this.btn_shoudong = new System.Windows.Forms.Button();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.lbl_piccount = new System.Windows.Forms.Label();
            this.txt_usedurl = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_About = new System.Windows.Forms.Button();
            this.lbl_downingcount = new System.Windows.Forms.Label();
            this.lbl_startcount = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fbd_url = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(12, 20);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(374, 21);
            this.txt_url.TabIndex = 0;
            this.txt_url.Text = " http://www.heisiwang.com";
            // 
            // btn_shoudong
            // 
            this.btn_shoudong.Location = new System.Drawing.Point(392, 17);
            this.btn_shoudong.Name = "btn_shoudong";
            this.btn_shoudong.Size = new System.Drawing.Size(100, 23);
            this.btn_shoudong.TabIndex = 1;
            this.btn_shoudong.Text = "开始";
            this.btn_shoudong.UseVisualStyleBackColor = true;
            this.btn_shoudong.Click += new System.EventHandler(this.btn_shoudong_Click);
            // 
            // txt_result
            // 
            this.txt_result.Location = new System.Drawing.Point(6, 20);
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_result.Size = new System.Drawing.Size(347, 502);
            this.txt_result.TabIndex = 2;
            // 
            // lbl_piccount
            // 
            this.lbl_piccount.AutoSize = true;
            this.lbl_piccount.Location = new System.Drawing.Point(605, 14);
            this.lbl_piccount.Name = "lbl_piccount";
            this.lbl_piccount.Size = new System.Drawing.Size(0, 12);
            this.lbl_piccount.TabIndex = 3;
            // 
            // txt_usedurl
            // 
            this.txt_usedurl.Location = new System.Drawing.Point(5, 20);
            this.txt_usedurl.Multiline = true;
            this.txt_usedurl.Name = "txt_usedurl";
            this.txt_usedurl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_usedurl.Size = new System.Drawing.Size(399, 502);
            this.txt_usedurl.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_About);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_downingcount);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_startcount);
            this.splitContainer1.Panel1.Controls.Add(this.txt_url);
            this.splitContainer1.Panel1.Controls.Add(this.btn_shoudong);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_piccount);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(785, 601);
            this.splitContainer1.SplitterDistance = 63;
            this.splitContainer1.TabIndex = 7;
            // 
            // btn_About
            // 
            this.btn_About.Location = new System.Drawing.Point(498, 17);
            this.btn_About.Name = "btn_About";
            this.btn_About.Size = new System.Drawing.Size(100, 23);
            this.btn_About.TabIndex = 6;
            this.btn_About.Text = "关于";
            this.btn_About.UseVisualStyleBackColor = true;
            this.btn_About.Click += new System.EventHandler(this.btn_About_Click);
            // 
            // lbl_downingcount
            // 
            this.lbl_downingcount.AutoSize = true;
            this.lbl_downingcount.Location = new System.Drawing.Point(727, 22);
            this.lbl_downingcount.Name = "lbl_downingcount";
            this.lbl_downingcount.Size = new System.Drawing.Size(0, 12);
            this.lbl_downingcount.TabIndex = 5;
            // 
            // lbl_startcount
            // 
            this.lbl_startcount.AutoSize = true;
            this.lbl_startcount.Location = new System.Drawing.Point(605, 35);
            this.lbl_startcount.Name = "lbl_startcount";
            this.lbl_startcount.Size = new System.Drawing.Size(0, 12);
            this.lbl_startcount.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(785, 534);
            this.splitContainer2.SplitterDistance = 365;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_result);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 531);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图片下载日志";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_usedurl);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 531);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已经爬过的地址";
            // 
            // fbd_url
            // 
            this.fbd_url.Description = "请选择图片存放地址！如果不选文件将存在本程序默认文件夹";
            // 
            // FrmStrat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 601);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStrat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lorna网站图片爬虫 测试版 V1.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmStrat_FormClosed);
            this.Load += new System.EventHandler(this.FrmStrat_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.Button btn_shoudong;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Label lbl_piccount;
        private System.Windows.Forms.TextBox txt_usedurl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_startcount;
        private System.Windows.Forms.Label lbl_downingcount;
        private System.Windows.Forms.FolderBrowserDialog fbd_url;
        private System.Windows.Forms.Button btn_About;
    }
}

