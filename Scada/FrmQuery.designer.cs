namespace Scada
{
    partial class FrmQuery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuery));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_ReportSet = new System.Windows.Forms.Button();
            this.cmb_Zone = new System.Windows.Forms.ComboBox();
            this.rdo_Certain = new System.Windows.Forms.RadioButton();
            this.rdo_SelfSet = new System.Windows.Forms.RadioButton();
            this.rdo_ZoneSel = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_ReportSet);
            this.groupBox1.Controls.Add(this.cmb_Zone);
            this.groupBox1.Controls.Add(this.rdo_Certain);
            this.groupBox1.Controls.Add(this.rdo_SelfSet);
            this.groupBox1.Controls.Add(this.rdo_ZoneSel);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(20, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(377, 261);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择查询条件";
            // 
            // btn_ReportSet
            // 
            this.btn_ReportSet.Enabled = false;
            this.btn_ReportSet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ReportSet.Location = new System.Drawing.Point(203, 195);
            this.btn_ReportSet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_ReportSet.Name = "btn_ReportSet";
            this.btn_ReportSet.Size = new System.Drawing.Size(137, 35);
            this.btn_ReportSet.TabIndex = 30;
            this.btn_ReportSet.Text = "报表配置";
            this.btn_ReportSet.UseVisualStyleBackColor = true;
            this.btn_ReportSet.Click += new System.EventHandler(this.btn_ReportSet_Click);
            // 
            // cmb_Zone
            // 
            this.cmb_Zone.FormattingEnabled = true;
            this.cmb_Zone.Location = new System.Drawing.Point(203, 54);
            this.cmb_Zone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_Zone.Name = "cmb_Zone";
            this.cmb_Zone.Size = new System.Drawing.Size(136, 31);
            this.cmb_Zone.TabIndex = 29;
            // 
            // rdo_Certain
            // 
            this.rdo_Certain.AutoSize = true;
            this.rdo_Certain.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdo_Certain.Location = new System.Drawing.Point(39, 126);
            this.rdo_Certain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdo_Certain.Name = "rdo_Certain";
            this.rdo_Certain.Size = new System.Drawing.Size(116, 28);
            this.rdo_Certain.TabIndex = 28;
            this.rdo_Certain.TabStop = true;
            this.rdo_Certain.Text = "按默认配置";
            this.rdo_Certain.UseVisualStyleBackColor = true;
            // 
            // rdo_SelfSet
            // 
            this.rdo_SelfSet.AutoSize = true;
            this.rdo_SelfSet.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdo_SelfSet.Location = new System.Drawing.Point(39, 198);
            this.rdo_SelfSet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdo_SelfSet.Name = "rdo_SelfSet";
            this.rdo_SelfSet.Size = new System.Drawing.Size(116, 28);
            this.rdo_SelfSet.TabIndex = 27;
            this.rdo_SelfSet.TabStop = true;
            this.rdo_SelfSet.Text = "自定义配置";
            this.rdo_SelfSet.UseVisualStyleBackColor = true;
            this.rdo_SelfSet.CheckedChanged += new System.EventHandler(this.rdo_SelfSet_CheckedChanged);
            // 
            // rdo_ZoneSel
            // 
            this.rdo_ZoneSel.AutoSize = true;
            this.rdo_ZoneSel.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdo_ZoneSel.Location = new System.Drawing.Point(39, 58);
            this.rdo_ZoneSel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdo_ZoneSel.Name = "rdo_ZoneSel";
            this.rdo_ZoneSel.Size = new System.Drawing.Size(116, 28);
            this.rdo_ZoneSel.TabIndex = 26;
            this.rdo_ZoneSel.TabStop = true;
            this.rdo_ZoneSel.Text = "按区域选择";
            this.rdo_ZoneSel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.Location = new System.Drawing.Point(81, 299);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnOK.Size = new System.Drawing.Size(100, 39);
            this.btnOK.TabIndex = 30;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(252, 299);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCancel.Size = new System.Drawing.Size(100, 39);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 355);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FrmQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询条件";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_ReportSet;
        private System.Windows.Forms.ComboBox cmb_Zone;
        internal System.Windows.Forms.RadioButton rdo_Certain;
        internal System.Windows.Forms.RadioButton rdo_SelfSet;
        internal System.Windows.Forms.RadioButton rdo_ZoneSel;
        protected System.Windows.Forms.Button btnOK;
        protected System.Windows.Forms.Button btnCancel;
    }
}