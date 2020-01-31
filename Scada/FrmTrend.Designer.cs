namespace Scada
{
    partial class FrmTrend
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cboZoneSet = new System.Windows.Forms.ComboBox();
            this.cboTrendType = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnquery = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtSecond = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_count = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFinish = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.timerShow = new System.Windows.Forms.Timer(this.components);
            this.Chk1_1 = new System.Windows.Forms.CheckBox();
            this.Chk5_1 = new System.Windows.Forms.CheckBox();
            this.Chk6_1 = new System.Windows.Forms.CheckBox();
            this.Chk4_1 = new System.Windows.Forms.CheckBox();
            this.Chk3_1 = new System.Windows.Forms.CheckBox();
            this.Chk2_1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "label5";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
            this.splitContainer1.Panel1.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel1.Controls.Add(this.btnOK);
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Controls.Add(this.cboZoneSet);
            this.splitContainer1.Panel1.Controls.Add(this.cboTrendType);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(167)))), ((int)(((byte)(175)))));
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1924, 763);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(128, 720);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 39);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(24, 720);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(85, 40);
            this.btnOK.TabIndex = 41;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 128);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(248, 560);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
            this.tabPage1.Controls.Add(this.Chk2_1);
            this.tabPage1.Controls.Add(this.Chk3_1);
            this.tabPage1.Controls.Add(this.Chk4_1);
            this.tabPage1.Controls.Add(this.Chk6_1);
            this.tabPage1.Controls.Add(this.Chk5_1);
            this.tabPage1.Controls.Add(this.Chk1_1);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(240, 523);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "冷却水区域";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
            this.tabPage2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(240, 523);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "压缩空气区域";
            // 
            // cboZoneSet
            // 
            this.cboZoneSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
            this.cboZoneSet.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cboZoneSet.ForeColor = System.Drawing.SystemColors.Info;
            this.cboZoneSet.FormattingEnabled = true;
            this.cboZoneSet.Location = new System.Drawing.Point(24, 80);
            this.cboZoneSet.Margin = new System.Windows.Forms.Padding(4);
            this.cboZoneSet.Name = "cboZoneSet";
            this.cboZoneSet.Size = new System.Drawing.Size(188, 31);
            this.cboZoneSet.TabIndex = 42;
            // 
            // cboTrendType
            // 
            this.cboTrendType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
            this.cboTrendType.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cboTrendType.ForeColor = System.Drawing.SystemColors.Info;
            this.cboTrendType.FormattingEnabled = true;
            this.cboTrendType.Location = new System.Drawing.Point(24, 24);
            this.cboTrendType.Margin = new System.Windows.Forms.Padding(4);
            this.cboTrendType.Name = "cboTrendType";
            this.cboTrendType.Size = new System.Drawing.Size(188, 31);
            this.cboTrendType.TabIndex = 41;
            this.cboTrendType.SelectedIndexChanged += new System.EventHandler(this.cboTrendType_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chart1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(44)))), ((int)(((byte)(75)))));
            this.splitContainer2.Panel2.Controls.Add(this.btnquery);
            this.splitContainer2.Panel2.Controls.Add(this.btnUpdate);
            this.splitContainer2.Panel2.Controls.Add(this.txtSecond);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.txt_count);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.dtpFinish);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Panel2.Controls.Add(this.dtpStart);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitContainer2.Size = new System.Drawing.Size(1673, 763);
            this.splitContainer2.SplitterDistance = 660;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(167)))), ((int)(((byte)(175)))));
            chartArea7.AxisX.Interval = 1D;
            chartArea7.AxisX.Title = "时间";
            chartArea7.AxisX.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea7.AxisY.TitleFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(167)))), ((int)(((byte)(175)))));
            chartArea7.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea7);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend7.Name = "Legend1";
            this.chart1.Legends.Add(legend7);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            this.chart1.Series.Add(series7);
            this.chart1.Size = new System.Drawing.Size(1673, 660);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // btnquery
            // 
            this.btnquery.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnquery.Location = new System.Drawing.Point(1448, 40);
            this.btnquery.Margin = new System.Windows.Forms.Padding(4);
            this.btnquery.Name = "btnquery";
            this.btnquery.Size = new System.Drawing.Size(127, 41);
            this.btnquery.TabIndex = 40;
            this.btnquery.Text = "历史查询";
            this.btnquery.UseVisualStyleBackColor = true;
            this.btnquery.Click += new System.EventHandler(this.btnquery_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpdate.Location = new System.Drawing.Point(1296, 40);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(127, 41);
            this.btnUpdate.TabIndex = 39;
            this.btnUpdate.Text = "停止更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtSecond
            // 
            this.txtSecond.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSecond.Location = new System.Drawing.Point(824, 48);
            this.txtSecond.Margin = new System.Windows.Forms.Padding(4);
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Size = new System.Drawing.Size(75, 31);
            this.txtSecond.TabIndex = 38;
            this.txtSecond.Text = "10";
            this.txtSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(712, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 26);
            this.label5.TabIndex = 37;
            this.label5.Text = "时间间隙(s):";
            // 
            // txt_count
            // 
            this.txt_count.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_count.Location = new System.Drawing.Point(1040, 48);
            this.txt_count.Margin = new System.Windows.Forms.Padding(4);
            this.txt_count.Name = "txt_count";
            this.txt_count.Size = new System.Drawing.Size(75, 31);
            this.txt_count.TabIndex = 36;
            this.txt_count.Text = "12";
            this.txt_count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(936, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 26);
            this.label4.TabIndex = 35;
            this.label4.Text = "显示数目:";
            // 
            // dtpFinish
            // 
            this.dtpFinish.CalendarFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpFinish.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpFinish.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpFinish.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFinish.Location = new System.Drawing.Point(456, 48);
            this.dtpFinish.Name = "dtpFinish";
            this.dtpFinish.Size = new System.Drawing.Size(232, 31);
            this.dtpFinish.TabIndex = 9;
            this.dtpFinish.Value = new System.DateTime(2019, 12, 13, 7, 34, 29, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(368, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 26);
            this.label3.TabIndex = 8;
            this.label3.Text = "结束时间：";
            // 
            // dtpStart
            // 
            this.dtpStart.CalendarFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStart.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpStart.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(112, 48);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(232, 31);
            this.dtpStart.TabIndex = 7;
            this.dtpStart.Value = new System.DateTime(2019, 12, 13, 7, 34, 29, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(24, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 26);
            this.label2.TabIndex = 6;
            this.label2.Text = "开始时间：";
            // 
            // timerShow
            // 
            this.timerShow.Tick += new System.EventHandler(this.timerShow_Tick);
            // 
            // Chk1_1
            // 
            this.Chk1_1.AutoSize = true;
            this.Chk1_1.BackColor = System.Drawing.Color.Transparent;
            this.Chk1_1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Chk1_1.ForeColor = System.Drawing.Color.White;
            this.Chk1_1.Location = new System.Drawing.Point(32, 24);
            this.Chk1_1.Margin = new System.Windows.Forms.Padding(4);
            this.Chk1_1.Name = "Chk1_1";
            this.Chk1_1.Size = new System.Drawing.Size(117, 27);
            this.Chk1_1.TabIndex = 2;
            this.Chk1_1.Tag = "LQT_Level";
            this.Chk1_1.Text = "冷却塔液位";
            this.Chk1_1.UseVisualStyleBackColor = false;
            // 
            // Chk5_1
            // 
            this.Chk5_1.AutoSize = true;
            this.Chk5_1.BackColor = System.Drawing.Color.Transparent;
            this.Chk5_1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Chk5_1.ForeColor = System.Drawing.Color.White;
            this.Chk5_1.Location = new System.Drawing.Point(32, 68);
            this.Chk5_1.Margin = new System.Windows.Forms.Padding(4);
            this.Chk5_1.Name = "Chk5_1";
            this.Chk5_1.Size = new System.Drawing.Size(151, 27);
            this.Chk5_1.TabIndex = 6;
            this.Chk5_1.Tag = "LQT_InPre";
            this.Chk5_1.Text = "冷却塔回水压力";
            this.Chk5_1.UseVisualStyleBackColor = false;
            // 
            // Chk6_1
            // 
            this.Chk6_1.AutoSize = true;
            this.Chk6_1.BackColor = System.Drawing.Color.Transparent;
            this.Chk6_1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Chk6_1.ForeColor = System.Drawing.Color.White;
            this.Chk6_1.Location = new System.Drawing.Point(32, 112);
            this.Chk6_1.Margin = new System.Windows.Forms.Padding(4);
            this.Chk6_1.Name = "Chk6_1";
            this.Chk6_1.Size = new System.Drawing.Size(151, 27);
            this.Chk6_1.TabIndex = 7;
            this.Chk6_1.Tag = "LQT_InTemp";
            this.Chk6_1.Text = "冷却塔回水温度";
            this.Chk6_1.UseVisualStyleBackColor = false;
            // 
            // Chk4_1
            // 
            this.Chk4_1.AutoSize = true;
            this.Chk4_1.BackColor = System.Drawing.Color.Transparent;
            this.Chk4_1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Chk4_1.ForeColor = System.Drawing.Color.White;
            this.Chk4_1.Location = new System.Drawing.Point(32, 156);
            this.Chk4_1.Margin = new System.Windows.Forms.Padding(4);
            this.Chk4_1.Name = "Chk4_1";
            this.Chk4_1.Size = new System.Drawing.Size(151, 27);
            this.Chk4_1.TabIndex = 8;
            this.Chk4_1.Tag = "LQT_OutTemp";
            this.Chk4_1.Text = "冷却塔供水温度";
            this.Chk4_1.UseVisualStyleBackColor = false;
            // 
            // Chk3_1
            // 
            this.Chk3_1.AutoSize = true;
            this.Chk3_1.BackColor = System.Drawing.Color.Transparent;
            this.Chk3_1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Chk3_1.ForeColor = System.Drawing.Color.White;
            this.Chk3_1.Location = new System.Drawing.Point(32, 200);
            this.Chk3_1.Margin = new System.Windows.Forms.Padding(4);
            this.Chk3_1.Name = "Chk3_1";
            this.Chk3_1.Size = new System.Drawing.Size(151, 27);
            this.Chk3_1.TabIndex = 9;
            this.Chk3_1.Tag = "LQT_OutPre";
            this.Chk3_1.Text = "冷却塔供水压力";
            this.Chk3_1.UseVisualStyleBackColor = false;
            // 
            // Chk2_1
            // 
            this.Chk2_1.AutoSize = true;
            this.Chk2_1.BackColor = System.Drawing.Color.Transparent;
            this.Chk2_1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Chk2_1.ForeColor = System.Drawing.Color.White;
            this.Chk2_1.Location = new System.Drawing.Point(32, 244);
            this.Chk2_1.Margin = new System.Windows.Forms.Padding(4);
            this.Chk2_1.Name = "Chk2_1";
            this.Chk2_1.Size = new System.Drawing.Size(151, 27);
            this.Chk2_1.TabIndex = 10;
            this.Chk2_1.Tag = "LQT_BSPre";
            this.Chk2_1.Text = "冷却塔补水压力";
            this.Chk2_1.UseVisualStyleBackColor = false;
            // 
            // FrmTrend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 763);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTrend";
            this.Text = "FrmTrend";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmTrend_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFinish;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSecond;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_count;
        private System.Windows.Forms.Button btnquery;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cboZoneSet;
        private System.Windows.Forms.ComboBox cboTrendType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer timerShow;
        private System.Windows.Forms.CheckBox Chk2_1;
        private System.Windows.Forms.CheckBox Chk3_1;
        private System.Windows.Forms.CheckBox Chk4_1;
        private System.Windows.Forms.CheckBox Chk6_1;
        private System.Windows.Forms.CheckBox Chk5_1;
        private System.Windows.Forms.CheckBox Chk1_1;
    }
}