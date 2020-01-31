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
    public partial class FrmQuery : Form
    {
        public FrmQuery()
        {
            InitializeComponent();
            this.cmb_Zone.Items.Add("冷却水区域");
            this.cmb_Zone.Items.Add("压缩空气区域");
            this.cmb_Zone.SelectedIndex = 0;

        }

        /// <summary>
        /// 报表变量的名称集合
        /// </summary>
        public List<string> listVarNameIsReport = new List<string>();

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.listVarNameIsReport.Clear();

            //如果为按区域选择
            if (this.rdo_ZoneSel.Checked == true)
            {
                //选择冷却水区域
                if (this.cmb_Zone.Text  == "冷却水区域")
                {
                    listVarNameIsReport.Add("LQT_Level");
                    listVarNameIsReport.Add("LQT_InPre");
                    listVarNameIsReport.Add("LQT_InTemp");
                    listVarNameIsReport.Add("LQT_OutPre");
                    listVarNameIsReport.Add("LQT_OutTemp");
                    listVarNameIsReport.Add("LQT_BSPre");
                    listVarNameIsReport.Add("LQB1_Current");
                    listVarNameIsReport.Add("LQB1_Fre");
                    listVarNameIsReport.Add("LQB2_Current");
                    listVarNameIsReport.Add("LQB2_Fre");
                }
                //选择压缩空气区域
                else if (this.cmb_Zone.Text == "压缩空气区域")
                {
                    listVarNameIsReport.Add("KYJ1_OutTemp");
                    listVarNameIsReport.Add("KYJ2_OutTemp");
                    listVarNameIsReport.Add("KYJ3_OutTemp");
                    listVarNameIsReport.Add("CQG1_OutPre");
                    listVarNameIsReport.Add("CQG2_OutPre");
                    listVarNameIsReport.Add("CQG3_OutPre");
                    listVarNameIsReport.Add("Env_Temp");
                    listVarNameIsReport.Add("FQG_Temp");
                    listVarNameIsReport.Add("FQG_Pre");
                }
            }

            //如果为默认配置选择
            else if (this.rdo_Certain.Checked == true)
            {
                listVarNameIsReport.Add("LQT_Level");
                listVarNameIsReport.Add("LQT_InPre");
                listVarNameIsReport.Add("LQT_InTemp");
                listVarNameIsReport.Add("LQT_OutPre");
                listVarNameIsReport.Add("LQT_OutTemp");
                listVarNameIsReport.Add("LQT_BSPre");
                listVarNameIsReport.Add("LQB1_Current");
                listVarNameIsReport.Add("LQB1_Fre");
                listVarNameIsReport.Add("LQB2_Current");
                listVarNameIsReport.Add("LQB2_Fre");
            }
            //如果为自定义配置选择
            else if (this.rdo_SelfSet.Checked == true)
            {

            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 打开报表配置窗口，自定义选择需要报表的变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReportSet_Click(object sender, EventArgs e)
        {
            Frm_ReportSet objFrm = new Frm_ReportSet();//实例化一个报表配置窗口
            DialogResult Res = objFrm.ShowDialog();
            if (Res == DialogResult.OK)
            {
                listVarNameIsReport = objFrm.listVarName;//获取报表配置窗口中选择的需要进行报表的变量
            }
        }

        /// <summary>
        /// 自定义配置的勾选与报表配置按钮使能绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdo_SelfSet_CheckedChanged(object sender, EventArgs e)
        {
            this.btn_ReportSet.Enabled = this.rdo_SelfSet.Checked;
        }
    }
}
