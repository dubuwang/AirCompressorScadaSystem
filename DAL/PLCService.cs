using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Threading;
using System.IO.Ports;
using Common;

namespace DAL
{
    /// <summary>
    /// 和PLC进行通讯的类
    /// </summary>
    public class PLCService
    {

        public PLCService()
        {
            #region 从XML文件中读取变量的相关信息，XMLService类的一些静态字段存储区的实例化
            //  读取ini文件中串口的属性配置
            PLCService.listPortSet = XMLService.GetPortSet();

            //  读取变量对象
            PLCService.listVar = XMLService.GetLIstVar();

            //  读取存储区
            PLCService.listStoreArea = XMLService.GetLIstStoreArea();

            //  读取报警变量
            PLCService.listVarAlarm = XMLService.GetListVarAlarm();

            //  初始化CurrentValue,将变量按存储区进行分类
            foreach (Variable_Modbus item in PLCService.listVar)
            {
                if (!PLCService.CurrentValue.ContainsKey(item.VarName))
                {
                    PLCService.CurrentValue.Add(item.VarName, "");
                    PLCService.CurrentVarNote.Add(item.VarName, item.Note);
                }
                if (item.StoreType == "01 Coil Status(0x)")
                {
                    PLCService.List_0x.Add(item);
                }
                if (item.StoreType == "02 Input Status(1x)")
                {
                    PLCService.List_1x.Add(item);
                }
                if (item.StoreType == "03 Holding Register(4x)")
                {
                    PLCService.List_4x.Add(item);
                }
                if (item.StoreType == "04 Input Registers(3x)")
                {
                    PLCService.List_3x.Add(item);
                }
                if (item.IsFiling == "1")
                {
                    PLCService.listVarIsFiling.Add(item);
                }
                if (item.IsReport == "1")
                {
                    PLCService.listVarIsReport.Add(item);
                }
            }

            #endregion

            #region 根据listPortSet中的串口信息，封装Modbus串口参数对象，设置从站地址

            this.objModbusEnitity.Port = PLCService.listPortSet[0];
            this.objModbusEnitity.Paud = Convert.ToInt32(PLCService.listPortSet[2]);
            this.objModbusEnitity.DataBit = Convert.ToInt32(PLCService.listPortSet[4]);
            switch (PLCService.listPortSet[3])
            {
                case "None":
                    this.objModbusEnitity.IParity = Parity.None;
                    break;
                case "Even":
                    this.objModbusEnitity.IParity = Parity.Even;
                    break;
                case "Odd":
                    this.objModbusEnitity.IParity = Parity.Odd;
                    break;
                default:
                    this.objModbusEnitity.IParity = Parity.None;
                    break;
            }
            switch (PLCService.listPortSet[5])
            {
                case "1":
                    this.objModbusEnitity.IStopBit = StopBits.One;
                    break;
                case "2":
                    this.objModbusEnitity.IStopBit = StopBits.Two;
                    break;
                default:
                    this.objModbusEnitity.IStopBit = StopBits.One;
                    break;
            }

            this.slaveAddress = Convert.ToInt32(PLCService.listPortSet[1]);//从站地址

            //
            //if (PLCService.objModbus.OpenMyCom(PLCService.objModbusEnitity.Port, PLCService.objModbusEnitity.Paud, PLCService.objModbusEnitity.DataBit, PLCService.objModbusEnitity.IParity, PLCService.objModbusEnitity.IStopBit))
            //{
            //    //如果串口打开成功，则设置通讯位，启动线程
            //    PLCService.CommState = true;
            //    this.objComm.t.Start();
            //}
            //else
            //{
            //    PLCService.CommState = false;
            //}

            #endregion



            //实例化读取报文线程
            this.threadReadMessage = new Thread(new ThreadStart(ReadMessage));
            this.threadReadMessage.IsBackground = true;     //将线程设置为后台线程
        }

        #region 字段

        /// <summary>
        /// 变量对象集合
        /// </summary>
        public static List<Variable_Modbus> listVar;
        /// <summary>
        /// 报警变量对象集合
        /// </summary>
        public static List<VarAlarm_Modbus> listVarAlarm;
        /// <summary>
        /// 存储区集合
        /// </summary>
        public static List<StoreArea> listStoreArea;
        /// <summary>
        /// 归档变量集合
        /// </summary>
        public static List<Variable_Modbus> listVarIsFiling = new List<Variable_Modbus>();
        /// <summary>
        /// 报表变量集合
        /// </summary>
        public static List<Variable_Modbus> listVarIsReport = new List<Variable_Modbus>();

        /// <summary>
        /// 存储区域类型为0X区的变量集合
        /// </summary>
        public static List<Variable_Modbus> List_0x = new List<Variable_Modbus>();
        //1x区域的变量集合
        public static List<Variable_Modbus> List_1x = new List<Variable_Modbus>();
        //3x区域的变量集合
        public static List<Variable_Modbus> List_3x = new List<Variable_Modbus>();
        //4x区域的变量集合
        public static List<Variable_Modbus> List_4x = new List<Variable_Modbus>();

        /// <summary>
        /// 串口设置信息
        /// </summary>
        public static List<string> listPortSet = new List<string>();

        /// <summary>
        /// 读取报文线程
        /// </summary>
        Thread threadReadMessage;

        /// <summary>
        /// 变量名和变量数值的字典集合
        /// </summary>
        public static Dictionary<string, string> CurrentValue = new Dictionary<string, string>();

        /// <summary>
        /// 变量名和其注释的字典集合
        /// </summary>
        public static Dictionary<string, string> CurrentVarNote = new Dictionary<string, string>();

        /// <summary>
        /// Moubus通讯对象
        /// </summary>
        public Modbus objModbus = new Modbus();

        /// <summary>
        /// Modbus从站地址,进行通讯的从站地址
        /// </summary>
        public int slaveAddress;

        /// <summary>
        /// 通讯错误次数
        /// </summary>
        public static int timesError = 0;

        /// <summary>
        /// 0x区域的起始寄存器地址，？应该就是存储区对象的起始地址啊。。。。。。。。。。。。。。。
        /// </summary>
        public int StartReg_0x = 0;
        public int StartReg_4x = 0;
        /// <summary>
        /// 读写标志位
        /// </summary>
        public static bool IsWriting = false;

        //定义登录用户
        public static SysAdmin objAdmins = new SysAdmin();

        /// <summary>
        /// 定义通讯状态位，可以通过设置通讯状态位，来控制是否从设备读取数据
        /// </summary>
        public bool CommState = false;

        /// <summary>
        /// Modbus通讯参数(封装了串口的属性)
        /// </summary>
        public ModbusEnitity objModbusEnitity = new ModbusEnitity();

        #endregion

        /// <summary>
        /// 开始读取报文
        /// </summary>
        public void StartRead()
        {

            if (this.objModbus.OpenMyCom(this.objModbusEnitity.Port, this.objModbusEnitity.Paud, this.objModbusEnitity.DataBit, this.objModbusEnitity.IParity, this.objModbusEnitity.IStopBit))
            {
                //如果串口打开成功，则设置通讯位，启动线程
                this.CommState = true;
                this.threadReadMessage.Start();
            }
            else
            {
                this.CommState = false;
            }
        }

        /// <summary>
        /// 读取PLC存储区的信息
        /// </summary>
        public void ReadMessage()
        {

            //循环读取
            while (true)
            {
                if (!PLCService.IsWriting && this.CommState == true)    
                {
                    byte[] receivceByte = null;     //数组是引用类型

                    foreach (StoreArea item in PLCService.listStoreArea)
                    {
                        switch (item.StoreType)
                        {
                            //如果存储区类型为0区，则读取线圈状态(数字量输出)
                            case "01 Coil Status(0x)":

                                receivceByte = this.objModbus.ReadOutputStatus(this.slaveAddress, item.StartReg, item.Length);

                                if (receivceByte != null)
                                {
                                    PLCService.timesError += 1;

                                    //处理线圈状态字节,获取变量对应的值存到了字典中
                                    this.AnalyseData_0x(receivceByte);
                                }
                                else
                                {
                                    PLCService.timesError += 1;
                                }

                                break;

                            //如果存储区类型为4区，则读取保持寄存器
                            case "03 Holding Register(4x)":

                                receivceByte = this.objModbus.ReadKeepReg(this.slaveAddress, item.StartReg, item.Length);

                                if (receivceByte != null)
                                {
                                    PLCService.timesError -= 1;
                                    //处理保持寄存器字节，float类型数据对应2个保持寄存器(即4个字节)，存储变量值
                                    //receivceByte[0]=40001H receivceByte[1]=40001L receivceByte[2]=40002H receivceByte[3]=40002L
                                    this.AnalyseData_4x(receivceByte);
                                }
                                else
                                {
                                    PLCService.timesError += 1;
                                }

                                break;
                            default:
                                break;
                        }
                    }

                    //通过判断通讯错误次数是否大于20次，来确定通讯是否正常
                    if (timesError > 20)
                    {
                        this.CommState = false;
                    }
                    else
                    {
                        this.CommState = true;
                    }
                }
            }
        }


        /// <summary>
        /// 处理读取到的线圈状态字节
        /// </summary>
        /// <param name="recByte"></param>
        private void AnalyseData_0x(byte[] recByte)
        {
            //遍历存储区类型为0X区的变量
            foreach (Variable_Modbus item in PLCService.List_0x)
            {
                //将该变量的地址减去
                int totalIndex = int.Parse(item.Address) - StartReg_4x;
                //算出该变量处于第几个字节
                int byteIndex = totalIndex / 8;
                //算出该变量处于字节的第几位
                int index = totalIndex % 8;

                if (item.DataType == "Bool")
                {
                    string str = Convert.ToString(recByte[byteIndex], 2).PadLeft(8, '0');

                    //将该变量名所对应的值存到字典中
                    PLCService.CurrentValue[item.VarName] = str.Substring(7 - index, 1);

                }
            }
        }


        private void AnalyseData_4x(byte[] recByte)
        {
            byte[] res;
            int startByte;
            if (recByte != null)
            {
                foreach (Variable_Modbus item in List_4x)
                {

                    switch (item.DataType)
                    {

                        case "Float":   //float数据类型应读取4个字节 recByte[0]=40001H recByte[1]=40001L recByte[2]=40002H recByte[3]=40002L
                            startByte = int.Parse(item.Address) * 2;
                            res = new byte[4] { recByte[startByte], recByte[startByte + 1], recByte[startByte + 2], recByte[startByte + 3] };

                            //将该变量名所对应的值存到字典中
                            PLCService.CurrentValue[item.VarName] = Convert.ToDouble(Common.Double.BytetoFloatByPoint(res)).ToString("f1");
                            break;

                    }
                }
            }
        }

        /// <summary>
        /// 调换寄存器位置
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private byte[] Reverse(byte[] buff)
        {
            byte[] res = new byte[buff.Length];
            if (buff.Length == 4)
            {
                res[0] = buff[2];
                res[1] = buff[3];
                res[2] = buff[0];
                res[3] = buff[1];
            }
            return res;
        }
    }
}
