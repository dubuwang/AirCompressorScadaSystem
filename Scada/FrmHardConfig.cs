using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scada
{
    public partial class FrmHardConfig : Form
    {
        public FrmHardConfig()
        {
            InitializeComponent();
        }

        #region 嵌入子窗体
        private void OpenSubForm(Form objFrm)
        {
            CloseExistedForm();


            objFrm.TopLevel = false;    //将子窗体设置成非顶级控件
            objFrm.FormBorderStyle = FormBorderStyle.None;  //去掉窗体的边框
            objFrm.Parent = this.splitContainer1.Panel2;    //指定子窗体显示的容器
            objFrm.Dock = DockStyle.Fill;
            objFrm.Show();
        }

        private void CloseExistedForm()
        {
            foreach (Control item in this.splitContainer1.Panel2.Controls)
            {
                if (item is Form)
                {
                    ((Form)item).Close();
                }
            }
        }
        #endregion

        /// <summary>
        /// 打开Modbus的串口设置窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProptocalConfig_Click(object sender, EventArgs e)
        {
            new FrmPortSet().ShowDialog();
        }

        private void btnIOConfig_Click(object sender, EventArgs e)
        {
            OpenSubForm(new FrmIOVarManage());
        }

        private void FrmHardConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseExistedForm();
        }
    }
}
