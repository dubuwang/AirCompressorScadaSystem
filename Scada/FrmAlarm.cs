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


namespace Scada
{
    public partial class FrmAlarm : Form
    {
        public FrmAlarm()
        {
            InitializeComponent();

            //Combox初始化
            this.cboTrendType.Items.Add("实时报警");
            this.cboTrendType.Items.Add("历史报警");
            this.cboTrendType.SelectedIndex = 0;    //初始为实时报警模式

            this.isActual = true;
            this.showNum = Convert.ToInt32(this.txt_count.Text);

            //设置dgv
            this.dataGridView1.AutoGenerateColumns = false;
            Common.DataGridViewStyle.DoubleBuffered(this.dataGridView1, true);

            //启动查询定时器
            this.timerQuery.Interval = 1000;
            this.timerQuery.Start();
           
        }

        #region 字段
        //显示数目
        int showNum = 0;

        /// <summary>
        /// 报警类型标志位,是否为实时报警
        /// </summary>
        bool isActual ;

        private AlarmDataService objAlarmDataService = new AlarmDataService();
        #endregion



        /// <summary>
        /// BackgroundWorker类的DoWork事件所注册的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = objAlarmDataService.QueryAlarmDataByNum(this.showNum);
        }

        /// <summary>
        /// BackgroundWorker类的RunWorkerCompleted事件所注册的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //将查询出的结果绑定到dgv
            this.dataGridView1.DataSource = (DataTable)e.Result;

            if (this.dataGridView1.RowCount > 0)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "ACK")
                    {
                        this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 128, 128);
                        this.dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        this.dataGridView1.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 128);
                        this.dataGridView1.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                    else
                    {
                        this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.CadetBlue;
                        this.dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        this.dataGridView1.Rows[i].DefaultCellStyle.SelectionBackColor = Color.CadetBlue;
                        this.dataGridView1.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                }
            }
        }


        /// <summary>
        /// 定时调用backgroundWorkder的后台方法，执行查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerQuery_Tick(object sender, EventArgs e)
        {
            if (this.cboTrendType.Text == "历史报警") return;
            
            if (this.backgroundWorker1.IsBusy) return;

            this.backgroundWorker1.RunWorkerAsync();
        }


        private void FrmAlarm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.timerQuery.Stop();
        }

        /// <summary>
        /// 实时更新控制按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.cboTrendType.Text=="实时报警")
            {
                if (this.isActual)
                {
                    this.timerQuery.Enabled = false;
                    this.isActual = false;
                    this.btnUpdate.Text = "实时更新";
                }
                else
                {
                    this.timerQuery.Enabled = true;
                    this.isActual = true;
                    this.btnUpdate.Text = "停止更新";
                }
            }
        }
        
        /// <summary>
        ///  查询历史报警数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.cboTrendType.Text=="历史报警")
            {
                DateTime t1 = Convert.ToDateTime(this.dtpStart.Text);
                DateTime t2 = Convert.ToDateTime(this.dtpFinish.Text);

                if (t1 > t2)
                {
                    MessageBox.Show("开始时间与结束时间不符合,请检查!", "查询提示");
                    return;
                }

                TimeSpan ts = t2 - t1;
                if (ts.TotalDays > 7.0)
                {
                    MessageBox.Show("查询时间范围太大!", "查询提示");
                    return;
                }


                //调用数据访问类的方法，进行查询

                this.dataGridView1.DataSource = objAlarmDataService.QueryAlarmDataByDate(t1, t2, this.showNum);
            }
            else
            {
                MessageBox.Show("查询请先切换历史报警模式！", "查询提示");
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Common.DataGridViewStyle.DgvRowPostPaint(this.dataGridView1, e);
        }



        private void txt_count_TextChanged(object sender, EventArgs e)
        {
            this.showNum = Convert.ToInt32(this.txt_count.Text);
        }


        private void cboTrendType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboTrendType.Text=="历史报警")
            {
                this.timerQuery.Stop();
                this.isActual = false;
                this.btnUpdate.Text = "实时更新";
            }
        }

    }
}
