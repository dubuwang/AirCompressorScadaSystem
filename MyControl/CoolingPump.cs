using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MyControl
{
    public partial class CoolingPump : UserControl
    {
        public CoolingPump()
        {
            InitializeComponent();

          

            this.Load += CoolingPump_Load;
            //初始化定时器
            timer = new Timer();
            timer.Interval = this.SetMs;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
        }

        void CoolingPump_Load(object sender, EventArgs e)
        {
            this.pcbMain.Tag = this.VarName;
        }



        private Timer timer;

        /// <summary>
        /// 设定timer的interval，用于转动速度控制
        /// </summary>
        private int setMs = 40;
        public int SetMs
        {
            get { return setMs; }
            set
            {
                if (this.SetMs != value)
                {
                    setMs = value;
                    this.timer.Interval = this.setMs;
                }

            }
        }

        /// <summary>
        /// 连接变量名
        /// </summary>
        public string VarName { get; set; }
       
        /// <summary>
        /// 连接变量的值
        /// </summary>
        public string VarValue { get; set; }

        
        /// <summary>
        /// 是否开始转动,封装timer的enabled
        /// </summary>
        public bool Start
        {
            get { return this.timer.Enabled; }
            set { this.timer.Enabled = value; }
        }

        private int frame = 1;
        private void Change()
        {
            switch (frame)
            {
                case 1:
                    this.pcbMain.Image = Properties.Resources._1;
                    break;
                case 2:
                    this.pcbMain.Image = Properties.Resources._2;
                    break;
                case 3:
                    this.pcbMain.Image = Properties.Resources._3;
                    break;
                case 4:
                    this.pcbMain.Image = Properties.Resources._4;
                    break;
                case 5:
                    this.pcbMain.Image = Properties.Resources._5;
                    break;
                case 6:
                    this.pcbMain.Image = Properties.Resources._6;
                    break;
                case 7:
                    this.pcbMain.Image = Properties.Resources._7;
                    break;
                case 8:
                    this.pcbMain.Image = Properties.Resources._8;
                    break;
                case 9:
                    this.pcbMain.Image = Properties.Resources._9;
                    break;
                case 10:
                    this.pcbMain.Image = Properties.Resources._10;
                    break;
                case 11:
                    this.pcbMain.Image = Properties.Resources._11;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 定时更换图片，实现转动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            if ( this.VarValue == "1")
            {
                //如果变量值为1，则执行转动
                if (frame++ <= 11)
                {
                    this.Change();
                    RecMemory.ClearMemory();
                }
                else
                {
                    frame = 1;
                    this.Change();
                    RecMemory.ClearMemory();
                }
            }
        }


        //创建委托
        public delegate void BtnClickDelegate(object sender, EventArgs e);
        //创建事件
        public event BtnClickDelegate UserControlClick;
        
        /// <summary>
        /// 当PictureBox双击事件触发时，执行控件自建的委托UserControlClick所注册的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcbMain_DoubleClick(object sender, EventArgs e)
        {
            if (UserControlClick != null)
            {
                UserControlClick(sender, new EventArgs());
            }
        }


    }


}

