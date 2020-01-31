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
    public partial class FrmModifyVar : Form
    {
        public FrmModifyVar()
        {
            InitializeComponent();

            //初始化cbo下拉框
            InitialComboBox();
        }

        /// <summary>
        /// 构造方法传入一个要修改的变量和一个报警变量对象集合
        /// </summary>
        /// <param name="var"></param>
        /// <param name="list"></param>
        public FrmModifyVar(Variable_Modbus var, List<VarAlarm_Modbus> list)
            : this()
        {
            this.objVar = var;
            this.listVarAlarm = list;

            ShowVariable();
        }

        // 要修改的变量对象
        public Variable_Modbus objVar = null;
        // 报警变量对象集合
        public List<VarAlarm_Modbus> listVarAlarm = null;

        /// <summary>
        /// 显示要修改的变量内容
        /// </summary>
        private void ShowVariable()
        {
            this.txtVarName.Text = objVar.VarName;
            this.txtAddress.Text = objVar.Address;
            this.txtNote.Text = objVar.Note;
            this.cboDataType.Text = objVar.DataType;
            this.cboStoreArea.Text = objVar.StoreType;
            this.chkIsFilling.Checked = objVar.IsFiling == "1" ? true : false;
            this.chkIsAlarm.Checked = objVar.IsAlarm == "1" ? true : false;
            this.chkIsReport.Checked = objVar.IsReport == "1" ? true : false;

            //显示报警变量
            if (this.listVarAlarm != null && objVar.IsAlarm == "1")
            {
                foreach (VarAlarm_Modbus item in this.listVarAlarm)
                {
                    switch (item.AlarmType)
                    {
                        case "High":
                            this.txt_Alarm_High.Text = item.AlarmValue.ToString();
                            this.txt_Priority_High.Text = item.Priority.ToString();
                            this.txt_Note_High.Text = item.Note.ToString();
                            this.chk_High.Checked = true;
                            break;
                        case "HiHi":
                            this.txt_Alarm_HiHi.Text = item.AlarmValue.ToString();
                            this.txt_Priority_HiHi.Text = item.Priority.ToString();
                            this.txt_Note_HiHi.Text = item.Note.ToString();
                            this.chk_HiHi.Checked = true;
                            break;
                        case "Low":
                            this.txt_Alarm_Low.Text = item.AlarmValue.ToString();
                            this.txt_Priority_Low.Text = item.Priority.ToString();
                            this.txt_Note_Low.Text = item.Note.ToString();
                            this.chk_Low.Checked = true;
                            break;
                        case "LoLo":
                            this.txt_Alarm_LoLo.Text = item.AlarmValue.ToString();
                            this.txt_Priority_LoLo.Text = item.Priority.ToString();
                            this.txt_Note_LoLo.Text = item.Note.ToString();
                            this.chk_LoLo.Checked = true;
                            break;
                        default:
                            break;
                    }
                }
            }
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

            this.cboStoreArea.Items.Add("01 Coil Status(0x)");
            this.cboStoreArea.Items.Add("02 Input Status(1x)");
            this.cboStoreArea.Items.Add("03 Holding Register(4x)");
            this.cboStoreArea.Items.Add("04 Input Registers(3x)");
        }

        /// <summary>
        /// 按下确认按钮时，获取窗体中的数据，封装成变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            #region 封装变量对象
            //封装变量对象

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
            if (objVar.IsAlarm == "1")    //如果该变量是设置为报警的
            {
                this.listVarAlarm = new List<VarAlarm_Modbus>();
                
                //封装报警变量集合
                if (this.chkIsAlarm.Checked)
                { 
                    if (chk_High.Checked)
                    {
                        listVarAlarm.Add(new VarAlarm_Modbus()
                        {
                            VarName = objVar.VarName,
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
                            VarName = objVar.VarName,
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
                            VarName = objVar.VarName,
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
                            VarName = objVar.VarName,
                            Priority = Convert.ToInt16(this.txt_Priority_LoLo.Text.Trim()),
                            AlarmType = "LoLo",
                            AlarmValue = float.Parse(this.txt_Alarm_LoLo.Text.Trim()),
                            Note = this.txt_Note_LoLo.Text.Trim()
                        });

                    }
                }
            }
            #endregion

            this.DialogResult = DialogResult.OK;
            this.Close();
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
    }
}
