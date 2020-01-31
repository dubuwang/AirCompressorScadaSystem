using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scada
{
    public partial class FrmPortSet : Form
    {
        public FrmPortSet()
        {
            InitializeComponent();

            //初始化下拉框

            //Port
            for (int i = 0; i < 20; i++)
            {
                this.cmb_Port.Items.Add("COM" + i.ToString());
            }
            this.cmb_Port.SelectedIndex = 0;
            //Paud
            this.cmb_Paud.Items.Add("4800");
            this.cmb_Paud.Items.Add("9600");
            this.cmb_Paud.Items.Add("14400");
            this.cmb_Paud.Items.Add("19200");
            this.cmb_Paud.Items.Add("38400");
            this.cmb_Paud.SelectedIndex = 1;
            //DataBit
            this.cmb_DataBits.Items.Add("7");
            this.cmb_DataBits.Items.Add("8");
            this.cmb_DataBits.SelectedIndex = 1;
            //StopBit
            this.cmb_StopBit.Items.Add("1");
            this.cmb_StopBit.Items.Add("2");
            this.cmb_StopBit.SelectedIndex = 0;
            //Parity
            this.cmb_Parity.Items.Add("None");
            this.cmb_Parity.Items.Add("Odd");
            this.cmb_Parity.Items.Add("Even");
            this.cmb_Parity.SelectedIndex = 0;
            //Address
            for (int i = 0; i < 10; i++)
            {
                this.cmb_Address.Items.Add(i.ToString());
            }
            this.cmb_Address.SelectedIndex = 1;
        }

        //定义MODBUS协议信息存储路径
        private string pathModbus = System.Windows.Forms.Application.StartupPath + "\\ConfigFile\\" + "ModbusPortSet.ini";


        private void btn_Save_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream(pathModbus, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                {
                    //保存协议信息
                    sw.WriteLine(this.cmb_Port.Text.Trim());
                    sw.WriteLine(this.cmb_Address.Text.Trim());
                    sw.WriteLine(this.cmb_Paud.Text.Trim());
                    sw.WriteLine(this.cmb_Parity.Text.Trim());
                    sw.WriteLine(this.cmb_DataBits.Text.Trim());
                    sw.WriteLine(this.cmb_StopBit.Text.Trim());
                }
            }

            MessageBox.Show("协议信息保存成功!", "保存提示");
        }



        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
