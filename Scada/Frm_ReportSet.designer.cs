namespace Scada
{
    partial class Frm_ReportSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ReportSet));
            this.label2 = new System.Windows.Forms.Label();
            this.lstSelected = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstUnSelect = new System.Windows.Forms.ListBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.btn_DelAll = new System.Windows.Forms.Button();
            this.btn_Del = new System.Windows.Forms.Button();
            this.btn_AddAll = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(369, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 24);
            this.label2.TabIndex = 37;
            this.label2.Text = "已选择参数";
            // 
            // lstSelected
            // 
            this.lstSelected.BackColor = System.Drawing.SystemColors.Control;
            this.lstSelected.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstSelected.FormattingEnabled = true;
            this.lstSelected.ItemHeight = 23;
            this.lstSelected.Location = new System.Drawing.Point(365, 55);
            this.lstSelected.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstSelected.Name = "lstSelected";
            this.lstSelected.Size = new System.Drawing.Size(220, 326);
            this.lstSelected.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 24);
            this.label1.TabIndex = 33;
            this.label1.Text = "未选择参数";
            // 
            // lstUnSelect
            // 
            this.lstUnSelect.BackColor = System.Drawing.SystemColors.Control;
            this.lstUnSelect.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstUnSelect.FormattingEnabled = true;
            this.lstUnSelect.ItemHeight = 23;
            this.lstUnSelect.Location = new System.Drawing.Point(21, 55);
            this.lstUnSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstUnSelect.Name = "lstUnSelect";
            this.lstUnSelect.Size = new System.Drawing.Size(220, 326);
            this.lstUnSelect.TabIndex = 32;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Cancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Cancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_Cancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Image")));
            this.btn_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Cancel.Location = new System.Drawing.Point(331, 409);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Cancel.Size = new System.Drawing.Size(105, 39);
            this.btn_Cancel.TabIndex = 41;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Sure
            // 
            this.btn_Sure.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Sure.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Sure.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Sure.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_Sure.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Sure.Image = ((System.Drawing.Image)(resources.GetObject("btn_Sure.Image")));
            this.btn_Sure.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Sure.Location = new System.Drawing.Point(184, 409);
            this.btn_Sure.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Sure.Size = new System.Drawing.Size(105, 39);
            this.btn_Sure.TabIndex = 40;
            this.btn_Sure.Text = "确认";
            this.btn_Sure.UseVisualStyleBackColor = false;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
            // 
            // btn_DelAll
            // 
            this.btn_DelAll.BackColor = System.Drawing.SystemColors.Control;
            this.btn_DelAll.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_DelAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_DelAll.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btn_DelAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_DelAll.Image = ((System.Drawing.Image)(resources.GetObject("btn_DelAll.Image")));
            this.btn_DelAll.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_DelAll.Location = new System.Drawing.Point(263, 299);
            this.btn_DelAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_DelAll.Name = "btn_DelAll";
            this.btn_DelAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_DelAll.Size = new System.Drawing.Size(85, 44);
            this.btn_DelAll.TabIndex = 39;
            this.btn_DelAll.Text = "<<";
            this.btn_DelAll.UseVisualStyleBackColor = false;
            this.btn_DelAll.Click += new System.EventHandler(this.btn_DelAll_Click);
            // 
            // btn_Del
            // 
            this.btn_Del.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Del.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Del.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Del.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btn_Del.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Del.Image = ((System.Drawing.Image)(resources.GetObject("btn_Del.Image")));
            this.btn_Del.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Del.Location = new System.Drawing.Point(263, 248);
            this.btn_Del.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Del.Size = new System.Drawing.Size(85, 44);
            this.btn_Del.TabIndex = 38;
            this.btn_Del.Text = "<";
            this.btn_Del.UseVisualStyleBackColor = false;
            this.btn_Del.Click += new System.EventHandler(this.btn_Del_Click);
            // 
            // btn_AddAll
            // 
            this.btn_AddAll.BackColor = System.Drawing.SystemColors.Control;
            this.btn_AddAll.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_AddAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_AddAll.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btn_AddAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_AddAll.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddAll.Image")));
            this.btn_AddAll.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_AddAll.Location = new System.Drawing.Point(263, 155);
            this.btn_AddAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_AddAll.Name = "btn_AddAll";
            this.btn_AddAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_AddAll.Size = new System.Drawing.Size(85, 44);
            this.btn_AddAll.TabIndex = 35;
            this.btn_AddAll.Text = ">>";
            this.btn_AddAll.UseVisualStyleBackColor = false;
            this.btn_AddAll.Click += new System.EventHandler(this.btn_AddAll_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Add.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Add.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btn_Add.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Add.Image = ((System.Drawing.Image)(resources.GetObject("btn_Add.Image")));
            this.btn_Add.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Add.Location = new System.Drawing.Point(263, 104);
            this.btn_Add.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Add.Size = new System.Drawing.Size(85, 44);
            this.btn_Add.TabIndex = 34;
            this.btn_Add.Text = ">";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // Frm_ReportSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 466);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Sure);
            this.Controls.Add(this.btn_DelAll);
            this.Controls.Add(this.btn_Del);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstSelected);
            this.Controls.Add(this.btn_AddAll);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstUnSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frm_ReportSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数选择";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btn_Cancel;
        protected System.Windows.Forms.Button btn_Sure;
        protected System.Windows.Forms.Button btn_DelAll;
        protected System.Windows.Forms.Button btn_Del;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstSelected;
        protected System.Windows.Forms.Button btn_AddAll;
        protected System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstUnSelect;
    }
}