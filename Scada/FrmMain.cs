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
using DAL;
using System.IO.Ports;
using MyControl;
using System.Threading;

namespace Scada
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            objFrmView = new FrmView(this.objPlcService);

            this.timerUpdateTime.Interval = 1000;
            this.timerUpdateTime.Start();

            //打开界面窗体
            this.btnView_Click(null, null);

            //开始读取
            this.objPlcService.StartRead();

            this.timerShowValue.Interval = 1000;
            this.timerShowValue.Start();

            //开始进行数据存储
            this.objInsert.StartInsert = true;

            //开启报警检测定时器
            this.timerAlarm.Interval = 1000;
            this.timerAlarm.Start();
        }

        #region 字段
        /// <summary>
        /// PLC访问对象
        /// </summary>
        public PLCService objPlcService = new PLCService();

        public AlarmDataService objAlarmService = new AlarmDataService();

        /// <summary>
        /// 实例化数据存储服务对象
        /// </summary>
        DataInsert objInsert = new DataInsert();

        /// <summary>
        /// 控制流程窗体
        /// </summary>
        public FrmView objFrmView;

        private static readonly object lockShow = new object();

        public Dictionary<string, float> lastAlarmValue = new Dictionary<string, float>();

        #endregion

        #region timerUpdateTime
        /// <summary>
        /// timer1，定时更新日期时间，通讯状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblDate.Text = DateTime.Now.ToShortDateString();
            this.lblTime.Text = DateTime.Now.ToLongTimeString();

            if (objPlcService.CommState == true)
            {
                this.btnCommu.Text = "通讯正常";
                this.btnCommu.ForeColor = Color.Black;
            }
            else
            {
                this.btnCommu.Text = "通讯故障";
                this.btnCommu.ForeColor = Color.Red;

                //这行代码存疑
                //objPlcService.objModbus.OpenMyCom(objPlcService.objModbusEnitity.Port, objPlcService.objModbusEnitity.Paud, objPlcService.objModbusEnitity.DataBit, objPlcService.objModbusEnitity.IParity, objPlcService.objModbusEnitity.IStopBit);
            }

        }


        #endregion

        #region timerShowValue
        /// <summary>
        /// 定时更新 TextShow值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerShowValue_Tick(object sender, EventArgs e)
        {
            lock (lockShow)
            {
                if (objFrmView.Visible)
                {

                    var linq1 = from Control item in objFrmView.Controls where item is TextShow || item is CoolingPump || item is Tap select item;

                    foreach (var item in linq1)
                    {
                        if (item is TextShow)
                        {
                            if (PLCService.CurrentValue.ContainsKey(((TextShow)item).VarName))
                            {
                                ((TextShow)item).VarValue = PLCService.CurrentValue[((TextShow)item).VarName];
                            }
                        }
                        else if (item is CoolingPump)
                        {
                            if (PLCService.CurrentValue.ContainsKey(((CoolingPump)item).VarName))
                            {
                                ((CoolingPump)item).VarValue = PLCService.CurrentValue[((CoolingPump)item).VarName];
                            }
                        }
                        else if (item is Tap)
                        {
                            if (PLCService.CurrentValue.ContainsKey(((Tap)item).VarName))
                            {
                                ((Tap)item).VarValue = PLCService.CurrentValue[((Tap)item).VarName];
                            }
                        }

                    }
                }
            }

        }
        #endregion

        #region timerAlarm
        private void timerAlarm_Tick(object sender, EventArgs e)
        {
            if (this.lastAlarmValue.Count == 0)
            {
                this.InitialAlarm();
            }
            else if (objPlcService.CommState==true)
            {
                this.CheckAlarm();
            }
        }
        #endregion

        #region 嵌入子窗体
        private void OpenSubForm(Form objFrm)
        {
            CloseExistedForm();


            objFrm.TopLevel = false;    //将子窗体设置成非顶级控件
            objFrm.FormBorderStyle = FormBorderStyle.None;  //去掉窗体的边框
            objFrm.Parent = this.splitContainer2.Panel1;    //指定子窗体显示的容器
            objFrm.Dock = DockStyle.Fill;
            objFrm.Show();
        }

        private void CloseExistedForm()
        {
            foreach (Control item in this.splitContainer2.Panel1.Controls)
            {
                if (item is Form)
                {
                    ((Form)item).Close();
                }
            }
        }
        #endregion

        #region 按钮打开窗体
        private void btnView_Click(object sender, EventArgs e)
        {

            objFrmView = new FrmView();
            this.OpenSubForm(objFrmView);

        }

        private void btnParamSet_Click(object sender, EventArgs e)
        {
            this.OpenSubForm(new FrmParamSet());
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            this.OpenSubForm(new FrmAlarm());
        }

        private void btnTrend_Click(object sender, EventArgs e)
        {
            this.OpenSubForm(new FrmTrend());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.OpenSubForm(new FrmReport());
        }

        private void btnHardConfig_Click(object sender, EventArgs e)
        {
            this.OpenSubForm(new FrmHardConfig());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 初始化lastAlarmValue

       

        /// <summary>
        ///  初始化报警变量值，获取报警变量的值存如lastAlarmValue中
        /// </summary>
        private void InitialAlarm()
        {
            //遍历报警变量集合，获取当前报警变量的值，存入LastValue中
            foreach (VarAlarm_Modbus item in PLCService.listVarAlarm)
            {
                if (!lastAlarmValue.ContainsKey(item.VarName) && PLCService.CurrentValue.ContainsKey(item.VarName))
                {
                    string Value = PLCService.CurrentValue[item.VarName];
                    float FloatValue = 0.0f;
                    if (float.TryParse(Value, out FloatValue))
                    {
                        lastAlarmValue.Add(item.VarName, FloatValue);
                    }
                }
            }
        }
        #endregion

        #region  报警检测判断
        /// <summary>
        /// 检测报警变量，写入数据库
        /// </summary>
        private void CheckAlarm()
        {
            //遍历报警变量
            foreach (VarAlarm_Modbus var in PLCService.listVarAlarm)
            {
                float AlarmValue = var.AlarmValue;
                int Priority = var.Priority;
                string AlarmType = var.AlarmType;
                string VarName = var.VarName;
                string Note = var.Note;

                if (lastAlarmValue.Count > 0)
                {
                    if (AlarmType == "High")    //如果该报警变量的类型为High
                    {
                        float currentValue = float.Parse(PLCService.CurrentValue[VarName]);
                        //如果上一次的值小于报警值且当前值大于报警值，写入ACK
                        if (lastAlarmValue[VarName] < var.AlarmValue && currentValue >= AlarmValue)
                        {
                            objAlarmService.InsertAlarmData(VarName, "ACK", Priority, AlarmType, currentValue, AlarmValue, PLCService.objAdmins.LoginName, Note);
                        }
                        //如果上一次的值>=报警值且当前值小于报警值，写入UNACK
                        if (lastAlarmValue[VarName] >= AlarmValue && currentValue < AlarmValue)
                        {
                            objAlarmService.InsertAlarmData(VarName, "UNACK", Priority, AlarmType, currentValue, AlarmValue, PLCService.objAdmins.LoginName, Note);
                        }
                    }
                    else if (AlarmType == "HiHi")   //如果该报警变量的类型为HiHi
                    {
                        float currentValue = float.Parse(PLCService.CurrentValue[VarName]);
                        if (lastAlarmValue[VarName] < AlarmValue && currentValue >= AlarmValue)
                        {
                            objAlarmService.InsertAlarmData(VarName, "ACK", Priority, AlarmType, currentValue, AlarmValue, PLCService.objAdmins.LoginName, Note);
                        }
                        if (lastAlarmValue[VarName] >= AlarmValue && currentValue < AlarmValue)
                        {
                            objAlarmService.InsertAlarmData(VarName, "UNACK", Priority, AlarmType, currentValue, AlarmValue, PLCService.objAdmins.LoginName, Note);
                        }
                    }
                    else if (AlarmType == "Low")
                    {
                        float currentValue = float.Parse(PLCService.CurrentValue[VarName]);
                        //如果上一次的值>=报警值且当前值小于报警值
                        if (lastAlarmValue[VarName] >= AlarmValue && currentValue < AlarmValue)
                        {
                            objAlarmService.InsertAlarmData(VarName, "ACK", Priority, AlarmType, currentValue, AlarmValue, PLCService.objAdmins.LoginName, Note);
                        }
                        if (lastAlarmValue[VarName] < AlarmValue && currentValue >= AlarmValue)
                        {
                            objAlarmService.InsertAlarmData(VarName, "UNACK", Priority, AlarmType, currentValue, AlarmValue, PLCService.objAdmins.LoginName, Note);
                        }
                    }
                    else if (AlarmType == "LoLo")
                    {
                        float currentValue = float.Parse(PLCService.CurrentValue[VarName]);
                        if (lastAlarmValue[VarName] >= AlarmValue && currentValue < AlarmValue)
                        {
                            objAlarmService.InsertAlarmData(VarName, "ACK", Priority, AlarmType, currentValue, AlarmValue, PLCService.objAdmins.LoginName, Note);
                        }
                        if (lastAlarmValue[VarName] < AlarmValue && currentValue >= AlarmValue)
                        {
                            objAlarmService.InsertAlarmData(VarName, "UNACK", Priority, AlarmType, currentValue, AlarmValue, PLCService.objAdmins.LoginName, Note);
                        }
                    }
                }
            }

            UpdateAlarm();
        }
        #endregion

        #region 更新lastAlarmValue
        private void UpdateAlarm()
        {
            foreach (VarAlarm_Modbus item in PLCService.listVarAlarm)
            {
                if (lastAlarmValue.ContainsKey(item.VarName))
                {
                    string Value = PLCService.CurrentValue[item.VarName];
                    lastAlarmValue[item.VarName] = float.Parse(Value);
                }
            }
        }
        #endregion

        /// <summary>
        /// 释放定时器资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            this.timerAlarm.Dispose();
            this.timerShowValue.Dispose();
            this.timerUpdateTime.Dispose();
        }

        /// <summary>
        /// 窗体关闭前，应该先将一些线程资源释放，特别是对数据库的存储服务，因该先将其停掉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.objInsert.StartInsert = false;
        }

        #region 已在PLCService中重构

        #region  初始化文件信息 已在PLCService中重构

        /// <summary>
        /// 将串口信息、变量信息、存储区信息、报警变量信息读取到XMLService中
        /// </summary>
        //private void InitialTxt()
        //{
        //    //第一步  读取串口属性信息

        //    CommMethods.CommInfo = XMLService.GetPortSet();  //CommMethods.GetFactor(Path);

        //    PLCService.listPortSet = XMLService.GetPortSet();

        //    //第二步  读取变量对象
        //    VarModbusList = XMLService.GetLIstVar();

        //    //第三步  读取存储区
        //    CommMethods.StoreModbusList = XMLService.GetLIstStoreArea();

        //    PLCService.listStoreArea = XMLService.GetLIstStoreArea();

        //    //第四步  读取报警变量
        //    CommMethods.ListVarAlarm = XMLService.GetListVarAlarm();

        //    PLCService.listVarAlarm = XMLService.GetListVarAlarm();


        //    //第五步  变量分类、初始化CurrentValue,将变量按存储区进行分类后的变量信息存在了窗体的通讯线程对象中
        //    foreach (Variable_Modbus item in VarModbusList)
        //    {
        //        if (!CommMethods.CurrentValue.ContainsKey(item.VarName))
        //        {
        //            CommMethods.CurrentValue.Add(item.VarName, "");
        //            CommMethods.CurrentVarAdd.Add(item.VarName, item.Note);
        //        }
        //        if (item.StoreType == "01 Coil Status(0x)")
        //        {
        //            objComm.List_0x.Add(item);
        //        }
        //        if (item.StoreType == "02 Input Status(1x)")
        //        {
        //            objComm.List_1x.Add(item);
        //        }
        //        if (item.StoreType == "03 Holding Register(4x)")
        //        {
        //            objComm.List_4x.Add(item);
        //        }
        //        if (item.StoreType == "04 Input Registers(3x)")
        //        {
        //            objComm.List_3x.Add(item);
        //        }
        //        if (item.IsFiling == "1")
        //        {
        //            CommMethods.FileVarModbusList.Add(item);
        //            PLCService.listVarIsFiling.Add(item);
        //        }
        //        if (item.IsReport == "1")
        //        {
        //            CommMethods.ReportVarModbusList.Add(item);
        //            PLCService.listVarIsReport.Add(item);
        //        }
        //    }

        //}
        #endregion

        #region 初始化串口信息及打开串口，已在PLCService中重构
        /// <summary>
        /// 初始化串口信息及打开串口
        /// </summary>
        //private void InitialPortInfo()
        //{
        //    #region 重构部分
        //    //设置PLCService中串口属性信息和从站地址
        //    PLCService.objModbusEnitity.Port = PLCService.listPortSet[0];
        //    PLCService.objModbusEnitity.Paud = Convert.ToInt32(PLCService.listPortSet[2]);
        //    PLCService.objModbusEnitity.DataBit = Convert.ToInt32(PLCService.listPortSet[4]);
        //    PLCService.slaveAddress = Convert.ToInt32(PLCService.listPortSet[1]);//从站地址

        //    #region 设置奇偶校验位和停止位
        //    switch (PLCService.listPortSet[3])
        //    {
        //        case "None":
        //            PLCService.objModbusEnitity.IParity = Parity.None;
        //            break;
        //        case "Even":
        //            PLCService.objModbusEnitity.IParity = Parity.Even;
        //            break;
        //        case "Odd":
        //            PLCService.objModbusEnitity.IParity = Parity.Odd;
        //            break;
        //        default:
        //            PLCService.objModbusEnitity.IParity = Parity.None;
        //            break;
        //    }
        //    switch (PLCService.listPortSet[5])
        //    {
        //        case "1":
        //            PLCService.objModbusEnitity.IStopBit = StopBits.One;
        //            break;
        //        case "2":
        //            PLCService.objModbusEnitity.IStopBit = StopBits.Two;
        //            break;
        //        default:
        //            PLCService.objModbusEnitity.IStopBit = StopBits.One;
        //            break;
        //    }
        //    #endregion


        //    //打开串口，开启通讯线程
        //    //if (PLCService.objModbus.OpenMyCom(PLCService.objModbusEnitity.Port, PLCService.objModbusEnitity.Paud, PLCService.objModbusEnitity.DataBit, PLCService.objModbusEnitity.IParity, PLCService.objModbusEnitity.IStopBit))
        //    //{
        //    //    //如果串口打开成功，则设置通讯位，启动线程
        //    //    PLCService.CommState = true;
        //    //    this.objComm.t.Start();
        //    //}
        //    //else
        //    //{
        //    //    PLCService.CommState = false;
        //    //}

        //    #endregion


        //    CommMethods.objModbusEnitity.Port = CommMethods.CommInfo[0];
        //    CommMethods.Address = Convert.ToInt32(CommMethods.CommInfo[1]);
        //    CommMethods.objModbusEnitity.Paud = Convert.ToInt32(CommMethods.CommInfo[2]);
        //    CommMethods.objModbusEnitity.DataBit = Convert.ToInt32(CommMethods.CommInfo[4]);
        //    switch (CommMethods.CommInfo[3])
        //    {
        //        case "None":
        //            CommMethods.objModbusEnitity.IParity = Parity.None;
        //            break;
        //        case "Even":
        //            CommMethods.objModbusEnitity.IParity = Parity.Even;
        //            break;
        //        case "Odd":
        //            CommMethods.objModbusEnitity.IParity = Parity.Odd;
        //            break;
        //        default:
        //            CommMethods.objModbusEnitity.IParity = Parity.None;
        //            break;
        //    }
        //    switch (CommMethods.CommInfo[5])
        //    {
        //        case "1":
        //            CommMethods.objModbusEnitity.IStopBit = StopBits.One;
        //            break;
        //        case "2":
        //            CommMethods.objModbusEnitity.IStopBit = StopBits.Two;
        //            break;
        //        default:
        //            CommMethods.objModbusEnitity.IStopBit = StopBits.One;
        //            break;
        //    }

        //    if (CommMethods.objMod.OpenMyCom(CommMethods.objModbusEnitity.Port, CommMethods.objModbusEnitity.Paud, CommMethods.objModbusEnitity.DataBit, CommMethods.objModbusEnitity.IParity, CommMethods.objModbusEnitity.IStopBit))
        //    {
        //        CommMethods.CommOK = true;
        //        objComm.threadReadMessage.Start();

        //    }
        //    else
        //    {
        //        CommMethods.CommOK = false;
        //    }
        //}

        #endregion

        #endregion
    }
}

