using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Common;

namespace Scada
{
    public partial class FrmReport : Form
    {
        public FrmReport()
        {
            InitializeComponent();

            //初始化ComboBox下拉菜单
            this.cmb_ClassSel.Items.Add("早班");
            this.cmb_ClassSel.Items.Add("中班");
            this.cmb_ClassSel.Items.Add("夜班");
            this.cmb_ClassSel.SelectedIndex = 0;

            this.cmb_ReportType.Items.Add("年度报表");
            this.cmb_ReportType.Items.Add("季度报表");
            this.cmb_ReportType.Items.Add("月度报表");
            this.cmb_ReportType.Items.Add("周报表");
            this.cmb_ReportType.Items.Add("日报表");
            this.cmb_ReportType.Items.Add("班报表");
            this.cmb_ReportType.SelectedIndex = 5;

            this.dgvData.AutoGenerateColumns = false;
            Common.DataGridViewStyle.DoubleBuffered(this.dgvData, true);    //开启双缓冲

            this.SetDgvDataColumns();
        }

        private List<string> listVarNameIsReport = new List<string>();

        private ReportDataService objReportService = new ReportDataService();

        #region 实例化DataGridView的列

        System.Windows.Forms.DataGridViewTextBoxColumn InsertTime = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn LQT_Level = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn LQT_BSPre = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn LQT_InPre = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn LQT_OutPre = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn LQT_InTemp = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn LQT_OutTemp = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn LQB1_Current = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn LQB1_Fre = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn LQB2_Current = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn LQB2_Fre = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn KYJ1_OutTemp = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn KYJ2_OutTemp = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn KYJ3_OutTemp = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn CQG1_OutPre = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn CQG2_OutPre = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn CQG3_OutPre = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn Env_Temp = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn FQG_Temp = new DataGridViewTextBoxColumn();
        System.Windows.Forms.DataGridViewTextBoxColumn FQG_Pre = new DataGridViewTextBoxColumn();

        #endregion

        #region 设置dgvData的列
        /// <summary>
        /// 设置GridView的列
        /// </summary>
        /// <param name="grdView"></param>
        /// <returns></returns>
        public void SetDgvDataColumns()
        {

            this.dgvData.Columns.Clear();

            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                InsertTime,
                LQT_Level,
                LQT_BSPre,
                LQT_InPre,
                LQT_OutPre,
                LQT_InTemp,
                LQT_OutTemp,
                LQB1_Current,
                LQB1_Fre,
                LQB2_Current,
                LQB2_Fre,
                KYJ1_OutTemp,
                KYJ2_OutTemp,
                KYJ3_OutTemp,
                CQG1_OutPre,
                CQG2_OutPre,
                CQG3_OutPre,
                Env_Temp,
                FQG_Pre,
                FQG_Temp,
            });

            this.InsertTime.DataPropertyName = "InsertTime";
            this.InsertTime.HeaderText = "日期时间";
            this.InsertTime.Name = "InsertTime";
            this.InsertTime.ReadOnly = true;
            this.InsertTime.Width = 130;

            this.LQT_Level.DataPropertyName = "LQT_Level";
            this.LQT_Level.HeaderText = "冷却塔液位";
            this.LQT_Level.Name = "LQT_Level";
            this.LQT_Level.ReadOnly = true;
            this.LQT_Level.Width = 150;

            this.LQT_BSPre.DataPropertyName = "LQT_BSPre";
            this.LQT_BSPre.HeaderText = "补水压力";
            this.LQT_BSPre.Name = "LQT_BSPre";
            this.LQT_BSPre.ReadOnly = true;
            this.LQT_BSPre.Width = 150;

            this.LQT_InPre.DataPropertyName = "LQT_InPre";
            this.LQT_InPre.HeaderText = "回水压力";
            this.LQT_InPre.Name = "LQT_InPre";
            this.LQT_InPre.ReadOnly = true;
            this.LQT_InPre.Width = 150;

            this.LQT_InTemp.DataPropertyName = "LQT_InTemp";
            this.LQT_InTemp.HeaderText = "回水温度";
            this.LQT_InTemp.Name = "LQT_InTemp";
            this.LQT_InTemp.ReadOnly = true;
            this.LQT_InTemp.Width = 150;

            this.LQT_OutPre.DataPropertyName = "LQT_OutPre";
            this.LQT_OutPre.HeaderText = "供水压力";
            this.LQT_OutPre.Name = "LQT_OutPre";
            this.LQT_OutPre.ReadOnly = true;
            this.LQT_OutPre.Width = 150;

            this.LQT_OutTemp.DataPropertyName = "LQT_OutTemp";
            this.LQT_OutTemp.HeaderText = "供水温度";
            this.LQT_OutTemp.Name = "LQT_OutTemp";
            this.LQT_OutTemp.ReadOnly = true;
            this.LQT_OutTemp.Width = 150;

            this.LQB1_Current.DataPropertyName = "LQB1_Current";
            this.LQB1_Current.HeaderText = "1#冷却泵电流";
            this.LQB1_Current.Name = "LQB1_Current";
            this.LQB1_Current.ReadOnly = true;
            this.LQB1_Current.Width = 160;

            this.LQB1_Fre.DataPropertyName = "LQB1_Fre";
            this.LQB1_Fre.HeaderText = "1#冷却泵频率";
            this.LQB1_Fre.Name = "LQB1_Fre";
            this.LQB1_Fre.ReadOnly = true;
            this.LQB1_Fre.Width = 160;

            this.LQB2_Current.DataPropertyName = "LQB2_Current";
            this.LQB2_Current.HeaderText = "2#冷却泵电流";
            this.LQB2_Current.Name = "LQB2_Current";
            this.LQB2_Current.ReadOnly = true;
            this.LQB2_Current.Width = 160;

            this.LQB2_Fre.DataPropertyName = "LQB2_Fre";
            this.LQB2_Fre.HeaderText = "2#冷却泵频率";
            this.LQB2_Fre.Name = "LQB2_Fre";
            this.LQB2_Fre.ReadOnly = true;
            this.LQB2_Fre.Width = 160;

            this.KYJ1_OutTemp.DataPropertyName = "KYJ1_OutTemp";
            this.KYJ1_OutTemp.HeaderText = "1#空压回水温度";
            this.KYJ1_OutTemp.Name = "KYJ1_OutTemp";
            this.KYJ1_OutTemp.ReadOnly = true;
            this.KYJ1_OutTemp.Width = 180;

            this.KYJ2_OutTemp.DataPropertyName = "KYJ2_OutTemp";
            this.KYJ2_OutTemp.HeaderText = "2#空压回水温度";
            this.KYJ2_OutTemp.Name = "KYJ2_OutTemp";
            this.KYJ2_OutTemp.ReadOnly = true;
            this.KYJ2_OutTemp.Width = 180;

            this.KYJ3_OutTemp.DataPropertyName = "KYJ3_OutTemp";
            this.KYJ3_OutTemp.HeaderText = "3#空压回水温度";
            this.KYJ3_OutTemp.Name = "KYJ3_OutTemp";
            this.KYJ3_OutTemp.ReadOnly = true;
            this.KYJ3_OutTemp.Width = 180;

            this.CQG1_OutPre.DataPropertyName = "CQG1_OutPre";
            this.CQG1_OutPre.HeaderText = "1#储气罐出口压力";
            this.CQG1_OutPre.Name = "CQG1_OutPre";
            this.CQG1_OutPre.ReadOnly = true;
            this.CQG1_OutPre.Width = 200;

            this.CQG2_OutPre.DataPropertyName = "CQG2_OutPre";
            this.CQG2_OutPre.HeaderText = "2#储气罐出口压力";
            this.CQG2_OutPre.Name = "CQG2_OutPre";
            this.CQG2_OutPre.ReadOnly = true;
            this.CQG2_OutPre.Width = 200;

            this.CQG3_OutPre.DataPropertyName = "CQG3_OutPre";
            this.CQG3_OutPre.HeaderText = "3#储气罐出口压力";
            this.CQG3_OutPre.Name = "CQG3_OutPre";
            this.CQG3_OutPre.ReadOnly = true;
            this.CQG3_OutPre.Width = 200;

            this.Env_Temp.DataPropertyName = "Env_Temp";
            this.Env_Temp.HeaderText = "露点温度";
            this.Env_Temp.Name = "Env_Temp";
            this.Env_Temp.ReadOnly = true;
            this.Env_Temp.Width = 150;

            this.FQG_Temp.DataPropertyName = "FQG_Temp";
            this.FQG_Temp.HeaderText = "分汽缸温度";
            this.FQG_Temp.Name = "FQG_Temp";
            this.FQG_Temp.ReadOnly = true;
            this.FQG_Temp.Width = 180;

            this.FQG_Pre.DataPropertyName = "FQG_Pre";
            this.FQG_Pre.HeaderText = "分汽缸压力";
            this.FQG_Pre.Name = "FQG_Pre";
            this.FQG_Pre.ReadOnly = true;
            this.FQG_Pre.Width = 180;

            
        }
        #endregion

        #region 使dgvData的所有列可见
        private void ShowAllColumns()
        {
            foreach (DataGridViewColumn item in this.dgvData.Columns)
            {
                item.Visible = true;
            }
        }
        #endregion

        #region 移除DataGridView数据为空的列
        public void RemoveEmptyColumns()
        {
            for (int i = 0; i < this.dgvData.ColumnCount; i++)//遍历所有列
            {
                bool isShow = true;
                foreach (DataGridViewRow row in this.dgvData.Rows)//遍历第i列下所有的单元格
                {
                    if (row.Cells[i].Value != null && row.Cells[i].Value.ToString().Length != 0)
                    {
                        isShow = false;
                    }
                    if (!isShow)    //只要有一个单元格为空，则将该列设置为不可见，跳出循环
                    {
                        this.dgvData.Columns[i].Visible = false;
                        break;
                    }
                }
            }
        }
        #endregion


        /// <summary>
        /// 当报表类型下拉框改变时，设置DateTime_Query的日期格式，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_ReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cmb_ReportType.Text)
            {
                case "年度报表":
                    this.DateTime_Query.CustomFormat = "yyyy-01-01 00:00:00";
                    break;
                case "季度报表":
                    this.DateTime_Query.CustomFormat = "yyyy-MM-01 00:00:00";
                    break;
                case "月度报表":
                    this.DateTime_Query.CustomFormat = "yyyy-MM-01 00:00:00";
                    break;
                case "周报表":
                    this.DateTime_Query.CustomFormat = "yyyy-MM-dd 00:00:00";
                    break;
                case "日报表":
                    this.DateTime_Query.CustomFormat = "yyyy-MM-dd 00:00:00";
                    break;
                
                case "班报表":
                    switch (this.cmb_ClassSel.Text)
                    {
                        case "早班":
                            this.DateTime_Query.CustomFormat = "yyyy-MM-dd 08:00:00";
                            break;
                        case "中班":
                            this.DateTime_Query.CustomFormat = "yyyy-MM-dd 16:00:00";
                            break;
                        case "夜班":
                            this.DateTime_Query.CustomFormat = "yyyy-MM-dd 00:00:00";
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    this.DateTime_Query.CustomFormat = "yyyy-MM-dd HH:mm:ss";
                    break;
            }
        }

        /// <summary>
        /// 当班组类型下拉框改变时，设置DateTime_Query的格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_ClassSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmb_ReportType.Text == "班报表")
            {
                switch (this.cmb_ClassSel.Text)
                {
                    case "早班":
                        this.DateTime_Query.CustomFormat = "yyyy-MM-dd 08:00:00";
                        break;
                    case "中班":
                        this.DateTime_Query.CustomFormat = "yyyy-MM-dd 16:00:00";
                        break;
                    case "夜班":
                        this.DateTime_Query.CustomFormat = "yyyy-MM-dd 00:00:00";
                        break;
                    default:
                        this.DateTime_Query.CustomFormat = "yyyy-MM-dd HH:mm:ss";
                        break;
                }

            }
        }

        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dgvData, e);
        }

        /// <summary>
        /// 点击查询按钮，先选择需要报表的变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            FrmQuery objFrm = new FrmQuery();
            DialogResult Res = objFrm.ShowDialog();

            if (Res == DialogResult.OK)
            {
                this.listVarNameIsReport = objFrm.listVarNameIsReport;
            }

            DateTime DTstart = Convert.ToDateTime(this.DateTime_Query.Text.Trim());

           
            this.SetDgvDataColumns();   //重新设置dgv的所有列
            this.ShowAllColumns();      //使所有列可见

            this.dgvData.DataSource = null;
            this.dgvData.DataSource=this.objReportService.GetReportDataByCondition(this.listVarNameIsReport, DTstart, this.cmb_ReportType.Text);

            this.RemoveEmptyColumns();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            // PrintDGV.Print_DataGridView(this.dgvData);
        }

        private void btn_Preview_Click(object sender, EventArgs e)
        {
            if (new Toexcel().DataGridviewShowToExcel(this.dgvData, true) == false)
            {
                MessageBox.Show("预览失败，请检查是否有数据或office是否安装", "预览提示");
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            new Toexcel().DataGridViewToExcel1(this.dgvData);
        }

    }
}
