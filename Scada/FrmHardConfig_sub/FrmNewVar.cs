using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;

namespace Scada
{
    public partial class FrmNewVar : Form
    {
        public FrmNewVar()
        {
            InitializeComponent();

            this.InitialComboBox();
        }


        // 变量对象
        public Variable_Modbus objVar=null;
        // 报警变量对象集合
        public List<VarAlarm_Modbus> listVarAlarm = null;

        public Action<Variable_Modbus, List<VarAlarm_Modbus>> actionTrans;

        /// <summary>
        /// 按下确认按钮时，获取窗口数据，封装对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                #region 封装变量对象
                //封装变量对象
                this.objVar = new Variable_Modbus();
                objVar.VarName = this.txtVarName.Text.Trim();
                objVar.StoreType = this.cboStoreArea.Text.Trim();
                objVar.DataType = this.cboDataType.Text.Trim();
                objVar.Address = this.txtAddress.Text.Trim();
                objVar.IsFiling = this.chkIsFilling.Checked ? "1" : "0";
                objVar.IsAlarm = this.chkIsAlarm.Checked ? "1" : "0";
                objVar.IsReport = this.chkIsReport.Checked ? "1" : "0";
                objVar.Note = this.txtNote.Text.Trim();
                #endregion

                #region 封装报警变量集合
                if (objVar.IsAlarm=="1")
                {
                    this.listVarAlarm = new List<VarAlarm_Modbus>();
                    //封装报警变量集合
                    if (this.chkIsAlarm.Checked)
                    {
                        
                        //封装修改后新的集合
                        if (chk_High.Checked)
                        {
                            listVarAlarm.Add(new VarAlarm_Modbus()
                            {
                                VarName = this.txtVarName.Text.Trim(),
                                Priority = Convert.ToInt16(this.txt_Priority_High.Text.Trim()),
                                AlarmType = "High",
                                AlarmValue = float.Parse(this.txt_Alarm_High.Text.Trim()),
                                Note = this.txt_Note_High.Text.Trim()
                            });
                        }
                        if (chk_HiHi.Checked)
                        {
                            listVarAlarm.Add(new VarAlarm_Modbus()
                            {
                                VarName = this.txtVarName.Text.Trim(),
                                Priority = Convert.ToInt16(this.txt_Priority_HiHi.Text.Trim()),
                                AlarmType = "HiHi",
                                AlarmValue = float.Parse(this.txt_Alarm_HiHi.Text.Trim()),
                                Note = this.txt_Note_HiHi.Text.Trim()
                            });
                        }
                        if (chk_Low.Checked)
                        {
                            listVarAlarm.Add(new VarAlarm_Modbus()
                            {
                                VarName = this.txtVarName.Text.Trim(),
                                Priority = Convert.ToInt16(this.txt_Priority_Low.Text.Trim()),
                                AlarmType = "Low",
                                AlarmValue = float.Parse(this.txt_Alarm_Low.Text.Trim()),
                                Note = this.txt_Note_Low.Text.Trim()
                            });
                        }
                        if (chk_LoLo.Checked)
                        {
                            listVarAlarm.Add(new VarAlarm_Modbus()
                            {
                                VarName = this.txtVarName.Text.Trim(),
                                Priority = Convert.ToInt16(this.txt_Priority_LoLo.Text.Trim()),
                                AlarmType = "LoLo",
                                AlarmValue = float.Parse(this.txt_Alarm_LoLo.Text.Trim()),
                                Note = this.txt_Note_LoLo.Text.Trim()
                            });

                        }
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {

                MessageBox.Show("变量数据输入错误："+ex.Message);
                return;
            }

            //调用其委托
            actionTrans(objVar,listVarAlarm);
        }

        /// <summary>
        /// 初始化ComboBox下拉框
        /// </summary>
        private void InitialComboBox()
        {
            this.cboDataType.Items.Add("Bool");
            this.cboDataType.Items.Add("Signed");
            this.cboDataType.Items.Add("Unsigned");
            this.cboDataType.Items.Add("Hex");
            this.cboDataType.Items.Add("Long");
            this.cboDataType.Items.Add("Long Inverse");
            this.cboDataType.Items.Add("Float");
            this.cboDataType.Items.Add("Float Inverse");
            this.cboDataType.Items.Add("Double");
            this.cboDataType.Items.Add("Double Inverse");
            this.cboDataType.SelectedIndex = 6;

            this.cboStoreArea.Items.Add("01 Coil Status(0x)");
            this.cboStoreArea.Items.Add("02 Input Status(1x)");
            this.cboStoreArea.Items.Add("03 Holding Register(4x)");
            this.cboStoreArea.Items.Add("04 Input Registers(3x)");
            this.cboStoreArea.SelectedIndex = 2;
        }

        #region CheckBox选中改变时，开启或关闭txt使能

        private void chk_LoLo_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_LoLo.Checked == true)
            {
                txt_Alarm_LoLo.Enabled = true;
                txt_Priority_LoLo.Enabled = true;
                txt_Note_LoLo.Enabled = true;
            }
            else
            {
                txt_Alarm_LoLo.Enabled = false;
                txt_Priority_LoLo.Enabled = false;
                txt_Note_LoLo.Enabled = false;
            }
        }

        private void chk_Low_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Low.Checked == true)
            {
                txt_Alarm_Low.Enabled = true;
                txt_Priority_Low.Enabled = true;
                txt_Note_Low.Enabled = true;
            }
            else
            {
                txt_Alarm_Low.Enabled = false;
                txt_Priority_Low.Enabled = false;
                txt_Note_Low.Enabled = false;
            }
        }

        private void chk_HiHi_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_HiHi.Checked == true)
            {
                txt_Alarm_HiHi.Enabled = true;
                txt_Priority_HiHi.Enabled = true;
                txt_Note_HiHi.Enabled = true;
            }
            else
            {
                txt_Alarm_HiHi.Enabled = false;
                txt_Priority_HiHi.Enabled = false;
                txt_Note_HiHi.Enabled = false;
            }
        }

        private void chk_High_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_High.Checked == true)
            {
                txt_Alarm_High.Enabled = true;
                txt_Priority_High.Enabled = true;
                txt_Note_High.Enabled = true;
            }
            else
            {
                txt_Alarm_High.Enabled = false;
                txt_Priority_High.Enabled = false;
                txt_Note_High.Enabled = false;
            }
        }

        #endregion

        private void FrmNewVar_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmIOVarManage.objFrmNew = null;
        }
    }
}
