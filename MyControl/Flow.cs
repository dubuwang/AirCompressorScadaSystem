using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace Flow
{
    public partial class Flow : UserControl
    {
        public Flow()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            Timer myTimer = new Timer();
            myTimer.Enabled = true;
            myTimer.Interval = 1000;
            myTimer.Tick += myTimer_Tick;
        }

        void myTimer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        /// <summary>
        /// 连接变量名
        /// </summary>
        public string VarName { get; set; }

        public string VarValue { get; set; }


        //定义画布
        private Graphics g;
        //定义宽度
        private int width;
        //定义高度
        private int height;
        //定义是否流动
        private bool isActive;
        //如果变量值为1，则可以流动
        public bool IsActive
        {
            get { return this.VarValue=="1"; }
            set { isActive = value; }
        }

        //定义流动颜色
        Color runColor = Color.FromArgb(128, 128, 255);
        public Color RunColor
        {
            get { return runColor; }
            set { runColor = value; }
        }
        //定义边界矩形框颜色
        Color startColor = Color.FromArgb(73, 73, 73);
        public Color StartColor
        {
            get { return startColor; }
            set { startColor = value; }
        }
        //定义边界内部颜色
        Color endColor = Color.FromArgb(247, 247, 247);
        public Color EndColor
        {
            get { return endColor; }
            set { endColor = value; }
        }
        //定义内部矩形长度
        int length = 10;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        //定义矩形宽度占总宽度百分比
        double extent = 0.75;
        public double Extent
        {
            get { return extent; }
            set { extent = value; }
        }
        //定义内部矩形间距大小
        int space = 5;
        public int Space
        {
            get { return space; }
            set { space = value; }
        }
        //定义流动速度
        int speed = 3;
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        //是否竖直
        bool isVertical = false;

        public bool IsVertical
        {
            get { return isVertical; }
            set { isVertical = value; }
        }

        //定义流动数值 1：从左到右  2：从右到左  3：从上到下  4：从下到上
        int flowNum = 1;
        public int FlowNum
        {
            get { return flowNum; }
            set { flowNum = value; }
        }
        int X = 0;//定义一个全局变量
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.SmoothingMode = SmoothingMode.HighQuality;
            width = this.Width;
            height = this.Height;

            LinearGradientBrush brush;

            if (isVertical)
            {
                if (width / 2 == 0)
                {
                    RectangleF rec1 = new RectangleF(0, 0, (float)(width / 2), height);
                    brush = new LinearGradientBrush(rec1, startColor, endColor, LinearGradientMode.Horizontal);
                    g.FillRectangle(brush, rec1);
                    RectangleF rec2 = new RectangleF((float)(width / 2), 0, (float)(width / 2), height);
                    brush = new LinearGradientBrush(rec2, endColor, startColor, LinearGradientMode.Horizontal);
                    g.FillRectangle(brush, rec2);
                }
                else
                {
                    RectangleF rec1 = new RectangleF(0, 0, (float)(width / 2), height);
                    brush = new LinearGradientBrush(rec1, startColor, endColor, LinearGradientMode.Horizontal);
                    g.FillRectangle(brush, rec1);
                    RectangleF rec2 = new RectangleF((float)(width / 2 - 0.5), 0, (float)(width / 2 + 0.5), height);
                    brush = new LinearGradientBrush(rec2, endColor, startColor, LinearGradientMode.Horizontal);
                    g.FillRectangle(brush, rec2);
                }
            }
            else
            {
                if (height / 2 == 0)
                {
                    RectangleF rec1 = new RectangleF(0, 0, width, (float)(height / 2));
                    brush = new LinearGradientBrush(rec1, startColor, endColor, LinearGradientMode.Vertical);
                    g.FillRectangle(brush, rec1);
                    RectangleF rec2 = new RectangleF(0, (float)(height / 2), width, height);
                    brush = new LinearGradientBrush(rec2, endColor, startColor, LinearGradientMode.Vertical);
                    g.FillRectangle(brush, rec2);
                }
                else
                {
                    RectangleF rec1 = new RectangleF(0, 0, width, (float)(height / 2));
                    brush = new LinearGradientBrush(rec1, startColor, endColor, LinearGradientMode.Vertical);
                    g.FillRectangle(brush, rec1);
                    RectangleF rec2 = new RectangleF(0, (float)(height / 2 - 0.5), width, (float)(height / 2 + 0.5));
                    brush = new LinearGradientBrush(rec2, endColor, startColor, LinearGradientMode.Vertical);
                    g.FillRectangle(brush, rec2);
                }
            }

            SolidBrush sb;
            //从左到右
            if (FlowNum == 1)
            {
                if (IsActive)
                {
                    g.TranslateTransform(1, 1);
                    sb = new SolidBrush(RunColor);
                    if (X > Length / Speed)//X决定什么时候从头返回
                    {
                        X = 0;
                    }
                    for (int i = 0; i < (width - 2) / (Length + space) + 2; i++)//i决定总共会容纳多少个方块
                    {
                        g.FillRectangle(sb, (Length + Space) * i + Speed * X, (int)((height - 2) * ((1 - Extent) / 2)), Length, (int)((height - 2) * Extent));
                    }
                    X++;
                }
            }
            //从右到左
            else if (FlowNum == 2)
            {
                if (IsActive)
                {
                    g.TranslateTransform(width - 1, 1);
                    sb = new SolidBrush(RunColor);
                    if (X > (Length / Speed))
                    {
                        X = 0;
                    }
                    for (int i = 0; i > (-1) * (width - 2) / (Length + space) - 2; i--)
                    {
                        g.FillRectangle(sb, Length * (-1) + i * (space + Length) - Speed * X, (int)((height - 2) * (1 - Extent) / 2), Length, (int)((height - 2) * Extent));
                    }
                    X++;
                }
            }
            //从上到下
            else if (FlowNum == 3)
            {
                if (IsActive)
                {
                    g.TranslateTransform(1, 1);
                    sb = new SolidBrush(RunColor);
                    if (X > Length / Speed)
                    {
                        X = 0;
                    }
                    for (int i = 0; i < (height - 2) / (Length + space) + 2; i++)
                    {
                        g.FillRectangle(sb, (int)((width - 2) * ((1 - Extent) / 2)), (Length + Space) * i + Speed * X, (int)((width - 2) * Extent), Length);

                    }
                    X++;
                }
            }
            //从下到上
            else if (FlowNum == 4)
            {
                if (IsActive)
                {
                    g.TranslateTransform(1, height - 1);
                    sb = new SolidBrush(RunColor);
                    if (X > Length / Speed)
                    {
                        X = 0;
                    }
                    for (int i = 0; i > (-1) * (height - 2) / (Length + space) - 2; i--)
                    {
                        g.FillRectangle(sb, (int)((width - 2) * (1 - Extent) / 2), Length * (-1) + i * (space + Length) - Speed * X, (int)((width - 2) * Extent), Length);
                    }
                    X++;
                }

            }
        }



    }
}
