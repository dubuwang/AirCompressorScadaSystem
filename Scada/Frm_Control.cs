using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;

namespace Scada
{
    public partial class Frm_Control : Form
    {
        public Frm_Control()
        {
            InitializeComponent();
        }
       
        public Frm_Control(string OperationString, bool value, string varName ,PLCService p):this()
        {
            this.objPLCService = p;
           
            this.operationString = OperationString;
            this.value = value;
            this.varName = varName;

            this.lbl_Operation.Text = "是否" + this.operationString + "?";
            //通过变量名寻址变量地址
            foreach (Variable_Modbus item in PLCService.listVar)
            {
                if (item.VarName == this.varName)
                {
                    this.address = item.Address;
                }
            }
        }

      
        
        //操作字符串
        string operationString = string.Empty;
        //写入数值
        bool value;
        //连接变量
        string varName = string.Empty;
        //变量地址
        string address = string.Empty;

        private PLCService objPLCService;

        private void btnOK_Click(object sender, EventArgs e)
        {
            //获取地址
            long Address = Convert.ToInt64(this.address);
            //通过当前数值来确定需要写入的数值，如果true，则取反false，如果false,则取反true
            if (value)
            {
                //将写入的标志位置1
                PLCService.IsWriting = true;
                //这里挂起线程是为了更好地将读取数据停止
                Thread.Sleep(500);
                bool res = objPLCService.objModbus.ForceOn(objPLCService.slaveAddress, Address);
                PLCService.IsWriting = false;
                if (res)
                {
                    MessageBox.Show(operationString + "成功！", "操作提示");
                }
                else
                {
                    MessageBox.Show(operationString + "失败！", "操作提示");
                }
            }
            else
            {
                PLCService.IsWriting = true;
                Thread.Sleep(500);
                bool res = objPLCService.objModbus.ForceOff(objPLCService.slaveAddress, Address);
                PLCService.IsWriting = false;
                if (res)
                {
                    MessageBox.Show(operationString + "成功！", "操作提示");
                }
                else
                {
                    MessageBox.Show(operationString + "失败！", "操作提示");
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
