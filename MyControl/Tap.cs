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
    public partial class Tap : UserControl
    {
        public Tap()
        {
            InitializeComponent();
            this.Load += Tap_Load;
            
        }

        void Tap_Load(object sender, EventArgs e)
        {
            this.pcbMain.Tag = this.VarName;
        }

        /// <summary>
        /// 连接变量名
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// 连接变量的值
        /// </summary>
        private string varValue;
        public string VarValue
        {
            get { return varValue; }
            set
            {
                varValue = value;
                //每当设置varValue值后，根据varValue的值更换图片
                this.pcbMain.Image = this.VarValue == "1" ? Properties.Resources.TapOn : Properties.Resources.TapOff;
            }
        }

        //给tap阀门创建了一个自定义的双击事件
        public event EventHandler TapDoubleClick;

        /// <summary>
        /// picturebox的双击事件所注册的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pcbMain_DoubleClick(object sender, EventArgs e)
        {
            if (TapDoubleClick != null)
            {
                TapDoubleClick(sender, new EventArgs());
            }
        }


    }
}
