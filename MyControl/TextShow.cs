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
    public partial class TextShow : UserControl
    {
        public TextShow()
        {
            InitializeComponent();


        }

        /// <summary>
        /// 连接变量名
        /// </summary>
        public string VarName { get; set; }

        private string varValue;
        public string VarValue
        {
            get { return varValue; }

            set
            {
                this.varValue = value;
                this.lblData.Text = this.VarValue;
            }
        }

        private string unit = "Mpa";
        public string Unit
        {
            get { return unit; }

            set
            {
                this.unit = value;
                this.lblUnit.Text = this.Unit;
            }
        }

        //给TextShow创建了一个自定义的双击事件
        public event EventHandler UserControlClick;

        /// <summary>
        /// lblData的双击事件所注册的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblData_Click(object sender, EventArgs e)
        {
            if (UserControlClick != null)
            {
                UserControlClick(sender, new EventArgs());
            }
        }
    }
}
