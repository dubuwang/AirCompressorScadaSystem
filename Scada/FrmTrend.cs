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
using System.Windows.Forms.DataVisualization.Charting;


namespace Scada
{
    public partial class FrmTrend : Form
    {
        public FrmTrend()
        {
            InitializeComponent();

            //初始化cbo
            this.cboTrendType.Items.Add("实时趋势");
            this.cboTrendType.Items.Add("历史趋势");
            this.cboTrendType.SelectedIndex = 0;
            this.cboZoneSet.Items.Add("冷却水区域");
            this.cboZoneSet.Items.Add("压缩空气区域");
            this.cboZoneSet.SelectedIndex = 0;

            //初始化listTrend集合
            this.listTrend.Add("LQT_Level");

            //初始化BackgroundWorker
            this.bw.DoWork += bw_DoWork;
            this.bw.RunWorkerCompleted += bw_RunWorkerCompleted;

            //启动timer
            this.timerShow.Interval = 1000;
            this.timerShow.Start();

        }


        #region 字段


        //显示数目
        int count = 0;
        //时间间隔
        int second = 0;

        /// <summary>
        /// 实时更新标志位
        /// </summary>
        bool isMonitor;

        BackgroundWorker bw = new BackgroundWorker();

        /// <summary>
        /// 要展示趋势的变量的变量名集合
        /// </summary>
        List<string> listTrend = new List<string>();

        ActualDataService objDataService = new ActualDataService();

        #endregion

        /// <summary>
        /// 将从数据库中查询出的趋势变量datatable绑定到chart展示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //清空chart的series集合
                this.chart1.Series.Clear();
                //获取要显示的list集合
                List<DataTable> listDTShow = (List<DataTable>)e.Result;

                if (listDTShow.Count != this.listTrend.Count) return;

                //遍历趋势变量名集合
                for (int i = 0; i < this.listTrend.Count; i++)
                {
                    //实例化一个图表序列，名称为该变量的注释名
                    Series objSeries = new Series(PLCService.CurrentVarNote[this.listTrend[i]]);
                    DataTable dt = listDTShow[i];

                    //将该图表序列的点集合绑定datatable
                    objSeries.Points.DataBind(dt.AsEnumerable(), "Time", "Value", null);
                    objSeries.ChartType = SeriesChartType.Spline;
                    objSeries.XValueType = ChartValueType.DateTime;
                    
                    objSeries.BorderWidth = 3;

                    if (int.TryParse(this.txt_count.Text.Trim(), out count))
                    {
                        if (objSeries.Points.Count < count)
                        {
                            objSeries.IsValueShownAsLabel = true;
                        }
                    }

                    this.chart1.Series.Add(objSeries);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 从数据库中查询出趋势变量的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataTable> list = new List<DataTable>();
            List<DateTime> timeList = (List<DateTime>)e.Argument;

            //遍历变量集合，查询出每个变量在时间范围内的值，以datatable形式存入list中
            for (int i = 0; i < this.listTrend.Count; i++)
            {

                DataTable d1 = objDataService.GetActualDataBetweenTimePeriod(this.listTrend[i], timeList[0], timeList[1]);
                if (d1 != null)
                {
                    list.Add(d1);
                }

            }
            e.Result = list;
        }

        /// <summary>
        /// 定时执行bw的dowork方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerShow_Tick(object sender, EventArgs e)
        {

            if (int.TryParse(this.txtSecond.Text.Trim(), out this.second))
            {
                //获取时间间隔，封装到list中
                List<DateTime> listDateTime = new List<DateTime>()
                {
                    DateTime.Now.AddSeconds(-this.second),  //开始时间
                    DateTime.Now                            //结束时间
              };

                if (bw.IsBusy) return;
                bw.RunWorkerAsync(listDateTime);
            }
        }

        /// <summary>
        /// 更新要展示趋势的变量集合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.timerShow.Stop();

            this.listTrend.Clear();

            foreach (Control item in this.tabPage1.Controls)
            {
                if (((CheckBox)item).Checked==true)
                {
                    string varName = ((CheckBox)item).Tag.ToString();
                    listTrend.Add(varName);
                }
                
            }

            this.timerShow.Start();
        }

        /// <summary>
        /// 取消所选的checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            var linq1 = from Control item in this.tabPage1.Controls select item;

            foreach (Control item in linq1)
            {
                ((CheckBox)item).Checked =false;
            }
        }



        private void FrmTrend_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.timerShow.Stop();
        }


        private void cboTrendType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboTrendType.Text=="历史趋势")
            {
                this.timerShow.Stop();
                this.isMonitor = false;
                this.btnUpdate.Text = "实时更新";

            }
        }

        /// <summary>
        /// 点击实时更新按钮时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!this.isMonitor)
            {
                //开始实时更新
                if (this.listTrend.Count == 0)
                {
                    MessageBox.Show("请检查变量因子是否选中", "更新提示");
                    return;
                }
                this.isMonitor = true;
                this.timerShow.Enabled = true;
                this.btnUpdate.Text = "停止更新";
            }
            else
            {
                //停止实时更新
                this.isMonitor = false;
                this.timerShow.Enabled = false;
                this.btnUpdate.Text = "实时更新";
            }
        }

        /// <summary>
        /// 历史查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnquery_Click(object sender, EventArgs e)
        {
            if (this.cboTrendType.SelectedIndex == 0)
            {
                MessageBox.Show("请先选择历史趋势,再进行查询!", "查询提示");
                return;
            }
            DateTime t1 = Convert.ToDateTime(this.dtpStart.Text);
            DateTime t2 = Convert.ToDateTime(this.dtpFinish.Text);
            if (t1 > t2)
            {
                MessageBox.Show("开始时间与结束时间不符合,请检查!", "查询提示");
                return;
            }
            TimeSpan ts = t2 - t1;
            if (ts.TotalHours > 6.0)
            {
                MessageBox.Show("查询时间范围太大!", "查询提示");
                return;
            }
            List<DateTime> time = new List<DateTime>() { t1, t2 };
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync(time);
            }
        }

        



    }
}
