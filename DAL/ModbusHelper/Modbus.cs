using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Timers;


namespace DAL
{
    /// <summary>
    /// MODBUS通讯类库,可读写保持寄存器及输出线圈等 最大支持16个Modbus设备
    /// </summary>
    public class Modbus
    {
        #region 字段
        /// <summary>
        /// 串口对象
        /// </summary>
        private SerialPort MyCom;
        
        /// <summary>
        /// 当前通讯设备的地址
        /// </summary>
        private int CurrentAddr;//定义当前通讯设备的地址
        
        /// <summary>
        /// 接收数据的字节数组
        /// </summary>
        private byte[] bData = new byte[1024];
        
        /// <summary>
        /// 错误信息字符串
        /// </summary>
        public string strErrMsg;//错误信息
        
        /// <summary>
        /// 上行报文字符串
        /// </summary>
        public string strUpData;
        
        /// <summary>
        /// 下行报文字符串
        /// </summary>
        public string strDownData;
        
        /// <summary>
        /// 临时字符串
        /// </summary>
        private string mTempStr;
        
        /// <summary>
        /// RTU(2进制)传送标志位
        /// </summary>
        public bool mRtuFlag = true;
        
        /// <summary>
        /// 接收到的字节
        /// </summary>
        private byte mReceiveByte;
        
        /// <summary>
        /// 接收到的字节数
        /// </summary>
        private int mReceiveByteCount;
        
        /// <summary>
        /// 保持寄存器或输入寄存器起始地址
        /// </summary>
        public long iMWordStartAddr;
        
        /// <summary>
        /// 输出线圈的起始地址
        /// </summary>
        public long iMBitStartAddr;
        
        /// <summary>
        /// 保持寄存器或输入寄存器数量
        /// </summary>
        public int iMWordLen;//保持寄存器及输出线圈长度

        /// <summary>
        /// 输出线圈状态字节数
        /// </summary>
        public int iMBitLen;
       
        /// <summary>
        /// 保持寄存器二维数组
        /// </summary>
        public UInt16[,] MWordVaue = new UInt16[16, 256];//定义最大保持寄存器二维数组 
        
        /// <summary>
        /// 输出线圈二维数组
        /// </summary>
        public bool[,] MBitVaue = new bool[16, 256];//定义输出线圈二维数组
       
        private bool bCommWell;
        
        /// <summary>
        /// 通讯状态标志数组
        /// </summary>
        public bool[] bCommFlag = new bool[16];//false 通讯正常  true 通讯异常 最大16个设备
        
        /// <summary>
        /// 串口忙碌标志
        /// </summary>
        public bool comBusying;  
        
        /// <summary>
        /// CRC16校验码高8位字节
        /// </summary>
        private byte ucCRCHi = 0xFF;
        /// <summary>
        /// CRC16校验码低8位字节
        /// </summary>
        private byte ucCRCLo = 0xFF;

        #endregion

        /// <summary>
        /// 构造方法，实例化串口对象，设置为RTU传送模式
        /// </summary>
        public Modbus()
        {
            MyCom = new SerialPort();
            mRtuFlag = true;
        }

        #region LRC校验
        /// <summary>
        /// LRC校验
        /// </summary>
        /// <param name="strLRC"></param>
        /// <returns></returns>
        private string LRC(string strLRC)
        {
            int d_lrc = 0;
            string h_lrc = "";
            int l = strLRC.Length;
            for (int c = 0; c < l; c = c + 2)
            {
                string c_data = strLRC.Substring(c, 2);
                d_lrc = d_lrc + (Int32)Convert.ToByte(c_data, 16);
                //d_lrc = d_lrc + Convert.ToInt32(c_data);
            }
            if (d_lrc >= 255)
                d_lrc = d_lrc % 0x100;
            h_lrc = Convert.ToInt32(~d_lrc + 1).ToString("X");
            if (h_lrc.Length > 2)
                h_lrc = h_lrc.Substring(h_lrc.Length - 2, 2);
            return h_lrc;
        }
        #endregion

        #region  CRC16逆序校验 查表法。modbud的crc16校验的多项式为8005的逆序A001
        /// <summary>
        /// CRC移位异或的计算结果表，高8位
        /// </summary>
        private static readonly byte[] aucCRCHi = {
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
             0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40
         };
        /// <summary>
        /// CRC移位异或的结果表，低8位
        /// </summary>
        private static readonly byte[] aucCRCLo = {
             0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7,
             0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
             0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9,
             0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
             0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
             0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
             0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D,
             0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 
             0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF,
             0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
             0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1,
             0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
             0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 
             0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
             0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
             0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
             0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97,
             0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
             0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89,
             0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
             0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83,
             0x41, 0x81, 0x80, 0x40
         };

        /// <summary>
        /// CRC16逆序校验查表法
        /// </summary>
        /// <param name="pucFrame">需校验的字节数组</param>
        /// <param name="usLen">字节数</param>
        private void Crc16(byte[] pucFrame, int usLen)
        {
            int i = 0;
            ucCRCHi = 0xFF;
            ucCRCLo = 0xFF;
            UInt16 iIndex = 0x0000;

            while (usLen-- > 0)
            {
                iIndex = (UInt16)(ucCRCLo ^ pucFrame[i++]);
                ucCRCLo = (byte)(ucCRCHi ^ aucCRCHi[iIndex]);
                ucCRCHi = aucCRCLo[iIndex];
            }

        }

        /// <summary>
        /// CRC16逆序校验计算法
        /// </summary>
        /// <param name="data">要校验的字节数组</param>
        /// <returns></returns>
        public static byte[] CRC16(byte[] data)
        {
            int len = data.Length;
            if (len > 0)
            {
                ushort crc = 0xFFFF;

                for (int i = 0; i < len; i++)
                {
                    crc = (ushort)(crc ^ (data[i]));
                    for (int j = 0; j < 8; j++)
                    {
                        crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
                    }
                }
                byte hi = (byte)((crc & 0xFF00) >> 8);  //CRC16校验码高8位字节，高位置
                byte lo = (byte)(crc & 0x00FF);         //CRC16校验码低8位字节，低位置

                return new byte[] { hi, lo };
            }
            return new byte[] { 0, 0 };
        }

        #endregion

        #region 设置串口并打开串口，关闭串口
        /// <summary>
        /// 设置串口并打开串口
        /// </summary>
        /// <param name="strPortNo">端口名</param>
        /// <param name="iBaudRate">波特率</param>
        /// <param name="iDataBits">数据位数</param>
        /// <param name="iParity">奇偶校验位</param>
        /// <param name="iStopBits">停止位</param>
        /// <returns>是否打开成功</returns>
        public bool OpenMyCom(string strPortNo, int iBaudRate, int iDataBits, System.IO.Ports.Parity iParity, System.IO.Ports.StopBits iStopBits)
        {
            try
            {
                for (int i = 0; i <= 15; i++)
                {

                    bCommFlag[i] = false;
                }

                //先关闭已经打开的串口
                if (MyCom.IsOpen == true)
                {
                    MyCom.Close();
                }
                MyCom.BaudRate = iBaudRate;
                MyCom.PortName = strPortNo;
                MyCom.DataBits = iDataBits;
                MyCom.Parity = iParity;//System.IO.Ports.Parity.None;
                MyCom.StopBits = iStopBits; //System.IO.Ports.StopBits.One
                MyCom.ReceivedBytesThreshold = 1;
                MyCom.DataReceived += new SerialDataReceivedEventHandler(MyCom_DataReceived);//DataReceived事件委托

                //打开串口
                MyCom.Open();
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns>关闭是否成功</returns>
        public bool CloseMyCom()
        {

            if (MyCom.IsOpen)
            {
                MyCom.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region DataReceived
        /// <summary>
        /// SerialPort 对象表示的端口接收了数据。即DataReceived事件
        /// 此方法为DataReceived事件注册的委托所持有的方法 
        /// DataReceive是异步执行的，在辅助线程上执行
        /// 事件处理过程执行完毕后，才会触发下一个DataReceived事件
        /// 每个协议都需重写DataReceived 事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyCom_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int i;

                ///循环接收数据(读取接收缓冲区中的字节)
                while (MyCom.BytesToRead > 0)
                {
                    //从串口的输入缓冲区中同步读取一个字节
                    mReceiveByte = (byte)MyCom.ReadByte();
                    //将接收到的字节存入接收数据的字节数组中
                    bData[mReceiveByteCount] = mReceiveByte;
                    
                    //System.Diagnostics.Trace.WriteLine(bData[mReceiveByte].ToString()); 
                    
                    mReceiveByteCount = mReceiveByteCount + 1;//接收到的字节数+1
                    
                    ///缓冲区溢出复位
                    if (mReceiveByteCount >= 1024)
                    {
                        mReceiveByteCount = 0;
                        MyCom.DiscardInBuffer();//清输入缓冲区
                        return;
                    }
                }
                ///至此，读取了一帧报文，并存储到了bData[]中
                
                #region RTU接收方式解析
                if (mRtuFlag == true)//RTU接受方式
                {
                    ///判断接收到的字节数组功能码等，进行相应处理
                    //输入寄存器 功能码0x04
                    if (mReceiveByteCount >= (iMWordLen * 2 + 5) && bData[0] == CurrentAddr + 1 && bData[1] == 0x04)
                    {
                        mTempStr = "";
                        for (i = 0; i < (iMWordLen * 2 + 5); i++)
                        {
                            mTempStr = mTempStr + " " + bData[i].ToString("X2");
                        }
                        //将收到的报文字节数组转成字符串存入上行数据字符串中
                        strUpData = mTempStr;
                        MyCom.DiscardInBuffer();//清输入缓冲区

                    }
                    
                    //保持寄存器 功能码0x03
                    if (mReceiveByteCount >= (iMWordLen * 2 + 5) && bData[0] == CurrentAddr + 1 && bData[1] == 0x03)
                    {
                        mTempStr = "";
                        for (i = 0; i < (iMWordLen * 2 + 5); i++)
                        {
                            mTempStr = mTempStr + " " + bData[i].ToString("X2");
                        }

                        //将接收到的报文中的寄存器16位数据赋值给全局变量MWordValue中  从起始地址开始
                        int nDownPos;
                        nDownPos = 0;
                        for (i = 0; i < iMWordLen; i++)
                        {
                            MWordVaue[CurrentAddr, nDownPos] = (UInt16)(bData[3 + 2 * i] * 256 + bData[3 + 2 * i + 1]);
                            nDownPos = nDownPos + 1;
                        }
                        //将收到的报文字节数组所转换成的字符串存入上行数据字符串中
                        strUpData = mTempStr;
                        MyCom.DiscardInBuffer();//清输入缓冲区
                    }

                    //输出线圈 功能码0x01
                    if (mReceiveByteCount >= (iMBitLen + 5) && bData[0] == CurrentAddr + 1 && bData[1] == 0x01)
                    {
                        mTempStr = "";
                        for (i = 0; i < (iMBitLen + 5); i++)
                        {
                            mTempStr = mTempStr + " " + bData[i].ToString("X2");
                        }
                        //将报文中的线圈状态字节转换成bool量赋值给全局变量MBitVaue中
                        for (i = 0; i < bData[2]; i++)
                        {
                            ByteToBArray(bData[3 + i], CurrentAddr, i * 8);
                        }
                        ///将收到的报文字节数组转换成字符串存入上行数据字符串中
                        strUpData = mTempStr;
                        MyCom.DiscardInBuffer();//清输入缓冲区
                    }

                    //输入线圈 功能码0x02
                    if (mReceiveByteCount >= (iMBitLen + 5) && bData[0] == CurrentAddr + 1 && bData[1] == 0x02)
                    {
                        mTempStr = "";
                        for (i = 0; i < (iMBitLen + 5); i++)
                        {
                            mTempStr = mTempStr + " " + bData[i].ToString("X2");
                        }
                        ///将收到的报文字节数组转换成字符串存入上行数据字符串中
                        strUpData = mTempStr;
                        MyCom.DiscardInBuffer();//清输入缓冲区
                    }
                }
                #endregion

                #region ASCII协议解析
                else //ASCII协议解析
                {
                    //保持寄存器 功能码0x03
                    string strTmpAddr, strTmpFun;
                    strTmpAddr = System.Text.Encoding.ASCII.GetString(bData, 1, 2);
                    strTmpFun = System.Text.Encoding.ASCII.GetString(bData, 3, 2);
                    if (bData[0] == ':' && mReceiveByteCount >= (iMWordLen * 4 + 11) && strTmpAddr == (CurrentAddr + 1).ToString("X2") && strTmpFun == "03")
                    {
                        mTempStr = System.Text.Encoding.ASCII.GetString(bData, 0, iMWordLen * 4 + 11);
                        //赋值给全局MW变量  从起始地址开始
                        int nDownPos;
                        nDownPos = 0;
                        byte bTmpHi, bTmpLo;
                        string tmpData;
                        for (i = 0; i < iMWordLen; i++)
                        {

                            tmpData = System.Text.Encoding.ASCII.GetString(bData, 7 + i * 4, 2);
                            bTmpHi = Convert.ToByte(tmpData, 16);
                            tmpData = System.Text.Encoding.ASCII.GetString(bData, 9 + i * 4, 2);
                            bTmpLo = Convert.ToByte(tmpData, 16);

                            MWordVaue[CurrentAddr, nDownPos] = (UInt16)(bTmpHi * 256 + bTmpLo);
                            nDownPos = nDownPos + 1;
                        }
                        strUpData = mTempStr;
                        MyCom.DiscardInBuffer();//清输入缓冲区

                    }
                    //输入寄存器04
                    if (bData[0] == ':' && mReceiveByteCount >= (iMWordLen * 4 + 11) && strTmpAddr == (CurrentAddr + 1).ToString("X2") && strTmpFun == "04")
                    {
                        mTempStr = System.Text.Encoding.ASCII.GetString(bData, 0, iMWordLen * 4 + 11);
                        strUpData = mTempStr;
                        MyCom.DiscardInBuffer();//清输入缓冲区

                    }
                    //输出线圈 功能码0x01
                    if (bData[0] == ':' && mReceiveByteCount >= (iMBitLen * 2 + 11) && strTmpAddr == (CurrentAddr + 1).ToString("X2") && strTmpFun == "01")
                    {
                        byte bDataValue;
                        string tmpData;
                        mTempStr = "";
                        mTempStr = System.Text.Encoding.ASCII.GetString(bData, 0, iMBitLen * 2 + 11);
                        //赋值给全局变量
                        for (i = 0; i < iMBitLen; i++)
                        {
                            tmpData = System.Text.Encoding.ASCII.GetString(bData, 7 + i * 2, 2);
                            bDataValue = Convert.ToByte(tmpData, 16);
                            ByteToBArray(bDataValue, CurrentAddr, i * 8);
                        }
                        strUpData = mTempStr;
                        MyCom.DiscardInBuffer();//清输入缓冲区
                    }
                    //输入线圈 功能码0x02
                    if (bData[0] == ':' && mReceiveByteCount >= (iMBitLen * 2 + 11) && strTmpAddr == (CurrentAddr + 1).ToString("X2") && strTmpFun == "02")
                    {
                        //byte bDataValue;
                        //string tmpData;
                        mTempStr = "";
                        mTempStr = System.Text.Encoding.ASCII.GetString(bData, 0, iMBitLen * 2 + 11);
                        strUpData = mTempStr;
                        MyCom.DiscardInBuffer();//清输入缓冲区
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                strErrMsg = ex.Message.ToString();
            }
        }
        #endregion

        #region 读保持寄存器  功能码03
        // MODBUS读保持寄存器 iAddress 开始地址(0开始),iLength 寄存器数量
        //主站请求：01 03 00 00 00 06 70 08
        //地址    1字节
        //功能码  1字节   0x03
        //寄存器起始地址  2字节   报文中寄存器起始地址0000对应设备中40001地址
        //寄存器数量  2字节   0x01~0x06
        //CRC校验 2字节
        /// <summary>
        /// 功能码03，读保持寄存器
        /// </summary>
        /// <param name="iDevAdd">从站地址</param>
        /// <param name="iAddress">起始地址</param>
        /// <param name="iLength">读取寄存器的数量</param>
        /// <returns>返回寄存器数据字节数组</returns>
        public byte[] ReadKeepReg(int iDevAdd, long iAddress, int iLength)
        {
            byte[] ResByte = null;
            iMWordStartAddr = iAddress;//寄存器起始地址
            iMWordLen = iLength;//寄存器数量

            if (comBusying == true) Thread.Sleep(250);
            
            //设置发送报文
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;//当前设备通讯地址=从站地址-1
            SendCommand[0] = (byte)iDevAdd;//从站地址
            SendCommand[1] = 0x03;//功能码
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);//起始地址高8位字节
            SendCommand[3] = (byte)(iAddress % 256);//起始地址低8位字节
            SendCommand[4] = (byte)((iLength - iLength % 256) / 256);//寄存器数高8位字节
            SendCommand[5] = (byte)(iLength % 256);//寄存器数低8位字节
            Crc16(SendCommand, 6);
            SendCommand[6] = ucCRCLo;//CRC校验码高字节
            SendCommand[7] = ucCRCHi;//CRC校验码低字节
            
            try
            {
                //发送指令给从站
                MyCom.Write(SendCommand, 0, 8);
            }
            catch
            {
                return ResByte;
            }

            //复位接收字节数和上行数据字符串
            mReceiveByteCount = 0;
            strUpData = "";
           
            //strDownData = "";
            //for (i = 0; i < 8; i++)
            //{
            //    strDownData = strDownData + " " + SendCommand[i].ToString("X2");
            //}

            Thread.Sleep(300);
            ///等待一段时间，等dataReceived收到从站返回的上行报文，处理完报文后
            
            //从上行数据字符串中获取寄存器数据字节数组
            ResByte = HexStringToByteArray(this.strUpData);
            return ResByte;
        }
        #endregion

        #region 读输出状态  功能码01
        //MODBUS读输出状态 iAddress 开始地址(0开始),iLength 线圈数量
        //主站请求：01 01 00 00 00 06 70 08
        //地址    1字节
        //功能码  1字节   0x01
        //起始寄存器地址  2字节   报文中线圈起始地址0000对应设备中00001地址
        //寄存器数量  2字节   0x01~0x06
        //CRC校验 2字节
        //此报文的意思是读从站地址01的设备的输出线圈00001到00006共6个线圈状态
        /// <summary>
        /// 功能码01，读输出线圈-数字输出量
        /// </summary>
        /// <param name="iDevAdd">从站地址</param>
        /// <param name="iAddress">起始地址</param>
        /// <param name="iLength">读取线圈数</param>
        /// <returns>返回上行数据字节数组</returns>
        public byte[] ReadOutputStatus(int iDevAdd, long iAddress, int iLength)
        {

            byte[] ResByte = null;
            
            //一个字节代表8个位状态
            if (iLength % 8 == 0)
            {
                iMBitLen = iLength / 8;
            }
            else
            {
                iMBitLen = iLength / 8 + 1;
            }
            
            iMBitStartAddr = iAddress;
            
            if (comBusying == true) Thread.Sleep(250);
            
            //设置发送报文
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;//起始地址
            SendCommand[1] = 0x01;//功能码
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);//起始地址高位
            SendCommand[3] = (byte)(iAddress % 256);//起始地址低位
            SendCommand[4] = (byte)((iLength - iLength % 256) / 256);//线圈数高位
            SendCommand[5] = (byte)(iLength % 256);//线圈数低位
            Crc16(SendCommand, 6);
            SendCommand[6] = ucCRCLo;
            SendCommand[7] = ucCRCHi;
            
            try
            {
                //发送指令。
                MyCom.Write(SendCommand, 0, 8);
            }
            catch (Exception)
            {
                return ResByte;
            }

            //接收字节数和上行数据字符串清空
            mReceiveByteCount = 0;
            strUpData = "";
            
            //strDownData = "";
            //for (i = 0; i < 8; i++)
            //{
            //    strDownData = strDownData + " " + SendCommand[i].ToString("X2");
            //}

            Thread.Sleep(300);
            ///等待一段时间后，等dataReceived收到从站返回的上行报文，处理完报文后
            //从上行数据字符串中获取线圈状态数据字节数组
            
            ResByte = HexStringToByteArray(this.strUpData);
            //返回线圈状态数据字节数组
            return ResByte;
        }
        #endregion

        #region 写-强制单线圈置ON  功能码05
        // MODBUS强制单线圈 iAddress 开始地址(0开始)
        //主站请求：01 05 00 00 FF 00 70 08
        //地址    1字节     
        //功能码  1字节   0x05
        //线圈地址  2字节  00 00 对应设备中00001线圈 
        //断通标志  2字节  FF 00 置线圈ON  
        //CRC校验 2字节
        /// <summary>
        /// 强制单线圈
        /// </summary>
        /// <param name="iDevAdd">从站地址</param>
        /// <param name="iAddress">线圈地址</param>
        /// <returns></returns>
        public bool ForceOn(int iDevAdd, long iAddress)
        {
            bool Success = true;

            iMWordStartAddr = iAddress;
            //iMWordLen = 0;
            if (comBusying == true) Thread.Sleep(250);
            //设置发送报文
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;//从站地址
            SendCommand[1] = 0x05;//功能码
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);//线圈地址高8位字节
            SendCommand[3] = (byte)(iAddress % 256);//线圈地址低8位字节
            SendCommand[4] = 0xff;//断通标志
            SendCommand[5] = 0x00;//断通标志
            Crc16(SendCommand, 6);
            SendCommand[6] = ucCRCLo;
            SendCommand[7] = ucCRCHi;
            try
            {
                //发送指令。
                MyCom.Write(SendCommand, 0, 8);
                Thread.Sleep(100);
            }
            catch
            {
                return false;
            }
            mReceiveByteCount = 0;
            //strUpData = "";
            //strDownData = "";
            //for (i = 0; i < 8; i++)
            //{
            //    strDownData = strDownData + " " + SendCommand[i].ToString("X2");
            //}
            return Success;
        }
        #endregion

        #region 写-强制单线圈置OFF  功能码05
        //MODBUS复位单线圈 iAddress 开始地址(0开始)
        //主站请求：01 05 00 00 00 00 70 08
        //地址    1字节
        //功能码  1字节   0x05
        //线圈地址  2字节   00 00 对应设备中00001线圈
        //断通标志  2字节   00 00 置线圈OFF
        //CRC校验 2字节
        /// <summary>
        /// 复位单线圈
        /// </summary>
        /// <param name="iDevAdd">从站地址</param>
        /// <param name="iAddress">线圈地址</param>
        /// <returns></returns>
        public bool ForceOff(int iDevAdd, long iAddress)
        {
            bool Success = true;


            iMWordStartAddr = iAddress;
            //iMWordLen = 0;
            if (comBusying == true) Thread.Sleep(250);
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;
            SendCommand[1] = 0x05;
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);
            SendCommand[3] = (byte)(iAddress % 256);
            SendCommand[4] = 0x00;
            SendCommand[5] = 0x00;
            Crc16(SendCommand, 6);
            SendCommand[6] = ucCRCLo;
            SendCommand[7] = ucCRCHi;

            try
            {
                //发送指令。
                MyCom.Write(SendCommand, 0, 8);
                Thread.Sleep(100);
            }
            catch (Exception)
            {
                return false;
            }

            mReceiveByteCount = 0;
            //strUpData = "";
            //strDownData = "";
            //for (i = 0; i < 8; i++)
            //{
            //    strDownData = strDownData + " " + SendCommand[i].ToString("X2");
            //}
            return Success;
        }
        #endregion

        #region 预置单字寄存器  功能码06
        //MODBUS预置单字寄存器 iAddress 开始地址(0开始),iHiValue 数据
        //主站请求：01 06 00 00 03 9E 70 08
        //地址    1字节
        //功能码  1字节   0x06
        //寄存器地址  2字节   00 00 对应设备中40001寄存器
        //数据值  2字节   03-数据值高位 9E-数据值低位
        //CRC校验 2字节
        /// <summary>
        /// 预置单保持寄存器
        /// </summary>
        /// <param name="iDevAdd">从站地址</param>
        /// <param name="iAddress">寄存器地址</param>
        /// <param name="SetValue">写入数据值</param>
        public void PreSetKeepReg(int iDevAdd, long iAddress, UInt16 SetValue)
        {
            int i;
            iMWordStartAddr = iAddress;
            //iMWordLen = 0;
            if (comBusying == true) Thread.Sleep(250);
            
            //设置发送报文
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;
            SendCommand[1] = 0x06;
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);
            SendCommand[3] = (byte)(iAddress % 256);
            SendCommand[4] = (byte)((SetValue - SetValue % 256) / 256); ;
            SendCommand[5] = (byte)(SetValue % 256); ;
            Crc16(SendCommand, 6);
            SendCommand[6] = ucCRCLo;
            SendCommand[7] = ucCRCHi;
            //发送指令。
            MyCom.Write(SendCommand, 0, 8);
            mReceiveByteCount = 0;
            //strUpData = "";
            strDownData = "";
            for (i = 0; i < 8; i++)
            {
                strDownData = strDownData + " " + SendCommand[i].ToString("X2");
            }
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
        }
        #endregion

        #region 预置双字寄存器  功能码10
        //MODBUS预置双字寄存器 iAddress 开始地址(0开始)
        //主站请求：01 10 00 00 00 02 04 AA BB CC DD CRCL CRCH
        //地址    1字节   01
        //功能码  1字节   0x10
        //起始寄存器地址  2字节   00 00 对应设备中40001地址
        //寄存器数量  2字节   00 02 寄存器数量2
        //字节计数    1字节   04 字节数4
        //数据高位AA 数据低位BB 数据高位CC 数据低为CC
        //CRC校验 2字节
        // 数据AABBH写入40001寄存器，CCDDH写入40002寄存器
        /// <summary>
        /// 预置双字寄存器
        /// </summary>
        /// <param name="iDevAdd">从站地址</param>
        /// <param name="iAddress">寄存器起始地址</param>
        /// <param name="SetValue">写入的数据</param>
        /// <returns></returns>
        public bool PreSetFloatKeepReg(int iDevAdd, long iAddress, float SetValue)
        {
            bool Success = true;
            int i;
            
            byte[] bSetValue = new byte[4];
            
            //将float32位浮点数据以字节数组的形式返回
            bSetValue = BitConverter.GetBytes(SetValue);
            
            
            iMWordStartAddr = iAddress;
            //iMWordLen = 0;
            if (comBusying == true) Thread.Sleep(250);
            byte[] SendCommand = new byte[13];
            CurrentAddr = iDevAdd - 1;
            
            ///设置发送报文
            SendCommand[0] = (byte)iDevAdd;//从站地址
            SendCommand[1] = 0x10;//功能码
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);//起始寄存器地址高8位字节
            SendCommand[3] = (byte)(iAddress % 256);//起始寄存器地址低8位字节
            SendCommand[4] = 0x00;//寄存器数高位
            SendCommand[5] = 0x02;//寄存器数低位
            SendCommand[6] = 0x04;//字节计数
            SendCommand[7] = bSetValue[3];//数据高位
            SendCommand[8] = bSetValue[2];//数据低位
            SendCommand[9] = bSetValue[1];//数据高位
            SendCommand[10] = bSetValue[0];//数据低位
            Crc16(SendCommand, 11);
            SendCommand[11] = ucCRCLo;//校验码低位
            SendCommand[12] = ucCRCHi;//校验码高位
            try
            {
                //发送指令。
                MyCom.Write(SendCommand, 0, 13);
                Thread.Sleep(100);
            }
            catch (Exception)
            {
                return false;
            }
            mReceiveByteCount = 0;
            //strUpData = "";
            strDownData = "";
            for (i = 0; i < 13; i++)
            {
                strDownData = strDownData + " " + SendCommand[i].ToString("X2");
            }
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
            return Success;
        }
        #endregion

        #region 去除报文中地址和校验等，将报文中的数据转换成字节数组返回
        /// <summary>
        /// 去除包头地址和校验等，将报文中的数据转换成字节数组返回
        /// </summary>
        /// <param name="s">数据字符串</param>
        /// <returns>纯数据字节数组</returns>
        private byte[] HexStringToByteArray(string s)
        {
            string Result = string.Empty;
            byte[] buffer = null;
            if (s != null && s.Length > 1)
            {
                string[] str = s.Trim().Split(' ');
                string[] Res = new string[str.Length - 5];
                for (int i = 0; i < str.Length - 5; i++)
                {
                    Res[i] = str[i + 3];
                }
                for (int i = 0; i < Res.Length; i++)
                {
                    Result += Res[i] + " ";
                }

                string[] strArr = Result.Trim().Split(' ');
                buffer = new byte[strArr.Length];
                for (int i = 0; i < strArr.Length; i++)
                {
                    //Convert.ToByte("ff", 16),将"ff"以16进制数字符串形式转换成字节数据
                    buffer[i] = Convert.ToByte(strArr[i], 16);
                }
            }
            return buffer;
        }
        #endregion

        #region 字节变量赋值给bool型全局数组
        /// <summary>
        /// 将一个字节变量转换成对应的8位二进制字符串返回
        /// </summary>
        /// <param name="bValue">字节变量</param>
        /// <returns>对应的8位二进制字符串</returns>
        public string ByteToBinary(byte bValue)
        {
            string strTemp;
            int i, strLen;
            strTemp = System.Convert.ToString(bValue, 2);

            if (strTemp.Length < 8)
            {
                strLen = strTemp.Length;
                for (i = 0; i < 8 - strLen; i++)
                {
                    strTemp = "0" + strTemp;
                }
            }
            return strTemp;
        }
        
        /// <summary>
        /// 将字节变量的每一位转换成bool数据存入一个bool型数组MBitVaue中
        /// </summary>
        /// <param name="bValue">字节变量</param>
        /// <param name="iAddr">当前设备地址</param>
        /// <param name="pos">存储起始位置</param>
        public void ByteToBArray(byte bValue, int iAddr, int pos)
        {
            string strBinary;
            int i;
            strBinary = ByteToBinary(bValue);
            for (i = 0; i < 8; i++)
            {
                if (strBinary.Substring(7 - i, 1) == "1")
                {
                    MBitVaue[iAddr, pos + i] = true;
                }
                else
                {
                    MBitVaue[iAddr, pos + i] = false;
                }
            }
            i = 10;

        }
        #endregion

        #region 读输入状态-数字量输入  功能码02H
        // MODBUS读输入状态 iAddress 开始地址(0开始),iLength 寄存器数量
        //主站请求：01 02 00 00 00 08 CRCL CRCH
        //地址    1字节
        //功能码  1字节   0x02
        //起始地址  2字节   00 00 对应设备中10001地址
        //线圈数量  2字节   00 08 线圈数为8
        //CRC校验 2字节
        /// <summary>
        /// 读输入状态-数字量输入
        /// </summary>
        /// <param name="iDevAdd">从站地址</param>
        /// <param name="iAddress">起始地址</param>
        /// <param name="iLength">线圈数</param>
        public void ReadInputStatus(int iDevAdd, long iAddress, int iLength)
        {
            int i;
            
            //一个字节代表8个位状态
            if (iLength % 8 == 0)
            {
                iMBitLen = iLength / 8;
            }
            else
            {
                iMBitLen = iLength / 8 + 1;
            }

            iMBitStartAddr = iAddress;
            if (comBusying == true) Thread.Sleep(250);
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;

            //设置发送报文
            SendCommand[0] = (byte)iDevAdd;//从站地址
            SendCommand[1] = 0x02;//功能码
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);//起始地址高8位字节
            SendCommand[3] = (byte)(iAddress % 256);//起始地址低8位字节
            SendCommand[4] = (byte)((iLength - iLength % 256) / 256);//线圈数高8位字节
            SendCommand[5] = (byte)(iLength % 256);//线圈数低8位字节
            Crc16(SendCommand, 6);
            SendCommand[6] = ucCRCLo;
            SendCommand[7] = ucCRCHi;
            
            //发送报文指令
            MyCom.Write(SendCommand, 0, 8);
            mReceiveByteCount = 0;
           
            //strUpData = "";
            strDownData = "";
            for (i = 0; i < 8; i++)
            {
                strDownData = strDownData + " " + SendCommand[i].ToString("X2");
            }
            
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
        }
        #endregion

        #region 读输入寄存器(通常为模拟量输入)  功能码04
        // MODBUS读输入寄存器 iAddress 开始地址(0开始),iLength 寄存器数量
        //主站请求：01 04 00 00 00 01 70 08
        //地址    1字节
        //功能码  1字节   0x04
        //起始寄存器地址  2字节   00 00 对应设备中30001地址
        //寄存器数量  2字节   00 01 寄存器数为1
        //CRC校验 2字节
        /// <summary>
        /// 读输入寄存器(通常为模拟量输入)  功能码04
        /// </summary>
        /// <param name="iDevAdd">从站地址</param>
        /// <param name="iAddress">寄存器起始地址</param>
        /// <param name="iLength">寄存器数量</param>
        public void ReadInputReg(int iDevAdd, long iAddress, int iLength)
        {
            int i;
            iMWordStartAddr = iAddress;
            iMWordLen = iLength;
            if (comBusying == true) Thread.Sleep(250);
            
            //设置发送报文
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;
            SendCommand[1] = 0x04;
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);
            SendCommand[3] = (byte)(iAddress % 256);
            SendCommand[4] = (byte)((iLength - iLength % 256) / 256);
            SendCommand[5] = (byte)(iLength % 256);
            Crc16(SendCommand, 6);
            SendCommand[6] = ucCRCLo;
            SendCommand[7] = ucCRCHi;
           
            //发送报文指令。
            MyCom.Write(SendCommand, 0, 8);
            mReceiveByteCount = 0;
            
            //strUpData = "";
            strDownData = "";
            for (i = 0; i < 8; i++)
            {
                strDownData = strDownData + " " + SendCommand[i].ToString("X2");
            }
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
        }
        #endregion

        #region  ASCII方式(暂时不用)
        public void ReadKeepRegAscii(int iDevAdd, long iAddress, int iLength)
        {
            int i;
            string strLRC;
            string strSendCommand;
            iMWordStartAddr = iAddress;
            iMWordLen = iLength;
            if (comBusying == true) Thread.Sleep(250);
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;
            SendCommand[1] = 0x03;
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);
            SendCommand[3] = (byte)(iAddress % 256);
            SendCommand[4] = (byte)((iLength - iLength % 256) / 256);
            SendCommand[5] = (byte)(iLength % 256);
            strSendCommand = "";
            for (i = 0; i < 6; i++)
            {
                strSendCommand = strSendCommand + SendCommand[i].ToString("X2");
            }

            strLRC = LRC(strSendCommand);
            strSendCommand = ":" + strSendCommand + strLRC;
            strSendCommand = strSendCommand + "\r" + "\n";
            System.Diagnostics.Trace.WriteLine(strSendCommand);
            //发送指令。
            MyCom.Write(strSendCommand);
            mReceiveByteCount = 0;
            //strUpData = "";
            strDownData = "";
            strDownData = strSendCommand;
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
        }
        public void PreSetKeepRegAscii(int iDevAdd, long iAddress, UInt16 SetValue)
        {
            int i;
            string strSendCommand, strLRC;
            iMWordStartAddr = iAddress;
            //iMWordLen = 0;
            if (comBusying == true) Thread.Sleep(250);
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;
            SendCommand[1] = 0x06;
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);
            SendCommand[3] = (byte)(iAddress % 256);
            SendCommand[4] = (byte)((SetValue - SetValue % 256) / 256); ;
            SendCommand[5] = (byte)(SetValue % 256); ;
            strSendCommand = "";
            for (i = 0; i < 6; i++)
            {
                strSendCommand = strSendCommand + SendCommand[i].ToString("X2");
            }

            strLRC = LRC(strSendCommand);
            strSendCommand = ":" + strSendCommand + strLRC;
            strSendCommand = strSendCommand + "\r" + "\n";
            //System.Diagnostics.Trace.WriteLine(strSendCommand);
            //发送指令。
            MyCom.Write(strSendCommand);
            mReceiveByteCount = 0;
            //strUpData = "";
            strDownData = strSendCommand;
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
        }
        public void ForceOnAscii(int iDevAdd, long iAddress)
        {
            int i;
            string strSendCommand, strLRC;
            iMWordStartAddr = iAddress;
            //iMWordLen = 0;
            if (comBusying == true) Thread.Sleep(250);
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;
            SendCommand[1] = 0x05;
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);
            SendCommand[3] = (byte)(iAddress % 256);
            SendCommand[4] = 0xff;
            SendCommand[5] = 0x00;
            strSendCommand = "";
            for (i = 0; i < 6; i++)
            {
                strSendCommand = strSendCommand + SendCommand[i].ToString("X2");
            }

            strLRC = LRC(strSendCommand);
            strSendCommand = ":" + strSendCommand + strLRC;
            strSendCommand = strSendCommand + "\r" + "\n";
            //System.Diagnostics.Trace.WriteLine(strSendCommand);
            //发送指令。
            MyCom.Write(strSendCommand);
            mReceiveByteCount = 0;
            //strUpData = "";
            strDownData = strSendCommand;
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
        }
        public void ReadInputStatusAscii(int iDevAdd, long iAddress, int iLength)
        {
            int i;
            //一个字节代表8个位状态
            if (iLength % 8 == 0)
            {
                iMBitLen = iLength / 8;
            }
            else
            {
                iMBitLen = iLength / 8 + 1;
            }
            string strLRC;
            string strSendCommand;
            iMBitStartAddr = iAddress;
            if (comBusying == true) Thread.Sleep(250);
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;
            SendCommand[1] = 0x02;
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);
            SendCommand[3] = (byte)(iAddress % 256);
            SendCommand[4] = (byte)((iLength - iLength % 256) / 256);
            SendCommand[5] = (byte)(iLength % 256);
            strSendCommand = "";
            for (i = 0; i < 6; i++)
            {
                strSendCommand = strSendCommand + SendCommand[i].ToString("X2");
            }

            strLRC = LRC(strSendCommand);
            strSendCommand = ":" + strSendCommand + strLRC;
            strSendCommand = strSendCommand + "\r" + "\n";
            System.Diagnostics.Trace.WriteLine(strSendCommand);
            //发送指令。
            MyCom.Write(strSendCommand);
            mReceiveByteCount = 0;
            //strUpData = "";
            strDownData = "";
            strDownData = strSendCommand;
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
        }
        public void ForceOffAscii(int iDevAdd, long iAddress)
        {
            int i;
            string strSendCommand, strLRC;
            iMWordStartAddr = iAddress;
            //iMWordLen = 0;
            if (comBusying == true) Thread.Sleep(250);
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;
            SendCommand[1] = 0x05;
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);
            SendCommand[3] = (byte)(iAddress % 256);
            SendCommand[4] = 0x00;
            SendCommand[5] = 0x00;
            strSendCommand = "";
            for (i = 0; i < 6; i++)
            {
                strSendCommand = strSendCommand + SendCommand[i].ToString("X2");
            }

            strLRC = LRC(strSendCommand);
            strSendCommand = ":" + strSendCommand + strLRC;
            strSendCommand = strSendCommand + "\r" + "\n";
            //System.Diagnostics.Trace.WriteLine(strSendCommand);
            //发送指令。
            MyCom.Write(strSendCommand);
            mReceiveByteCount = 0;
            //strUpData = "";
            strDownData = strSendCommand;
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
        }
        public void PreSetFloatKeepRegAscii(int iDevAdd, long iAddress, float SetValue)
        {
            int i;
            string strSendCommand, strLRC;
            byte[] bSetValue = new byte[4];
            bSetValue = BitConverter.GetBytes(SetValue);
            //bSetValue = &SetValue;
            iMWordStartAddr = iAddress;
            //iMWordLen = 0;
            if (comBusying == true) Thread.Sleep(250);
            byte[] SendCommand = new byte[13];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;
            SendCommand[1] = 0x10;
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);
            SendCommand[3] = (byte)(iAddress % 256);
            SendCommand[4] = 0x00;
            SendCommand[5] = 0x02;
            SendCommand[6] = 0x04;
            SendCommand[7] = bSetValue[1];
            SendCommand[8] = bSetValue[0];
            SendCommand[9] = bSetValue[3];
            SendCommand[10] = bSetValue[2];
            strSendCommand = "";
            for (i = 0; i < 11; i++)
            {
                strSendCommand = strSendCommand + SendCommand[i].ToString("X2");
            }
            strLRC = LRC(strSendCommand);
            strSendCommand = ":" + strSendCommand + strLRC;
            strSendCommand = strSendCommand + "\r" + "\n";
            //System.Diagnostics.Trace.WriteLine(strSendCommand);
            //发送指令。
            MyCom.Write(strSendCommand);
            mReceiveByteCount = 0;
            //strUpData = "";
            strDownData = strSendCommand;
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
        }
        public void ReadInputRegAscii(int iDevAdd, long iAddress, int iLength)
        {
            int i;
            string strLRC;
            string strSendCommand;
            iMWordStartAddr = iAddress;
            iMWordLen = iLength;
            if (comBusying == true) Thread.Sleep(250);
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;
            SendCommand[1] = 0x04;
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);
            SendCommand[3] = (byte)(iAddress % 256);
            SendCommand[4] = (byte)((iLength - iLength % 256) / 256);
            SendCommand[5] = (byte)(iLength % 256);
            strSendCommand = "";
            for (i = 0; i < 6; i++)
            {
                strSendCommand = strSendCommand + SendCommand[i].ToString("X2");
            }

            strLRC = LRC(strSendCommand);
            strSendCommand = ":" + strSendCommand + strLRC;
            strSendCommand = strSendCommand + "\r" + "\n";
            System.Diagnostics.Trace.WriteLine(strSendCommand);
            //发送指令。
            MyCom.Write(strSendCommand);
            mReceiveByteCount = 0;
            //strUpData = "";
            strDownData = "";
            strDownData = strSendCommand;
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
        }
        public void ReadOutputStatusAscii(int iDevAdd, long iAddress, int iLength)
        {
            int i;
            //一个字节代表8个位状态
            //一个字节代表8个位状态
            if (iLength % 8 == 0)
            {
                iMBitLen = iLength / 8;
            }
            else
            {
                iMBitLen = iLength / 8 + 1;
            }
            string strLRC;
            string strSendCommand;
            iMBitStartAddr = iAddress;
            if (comBusying == true) Thread.Sleep(250);
            byte[] SendCommand = new byte[8];
            CurrentAddr = iDevAdd - 1;
            SendCommand[0] = (byte)iDevAdd;
            SendCommand[1] = 0x01;
            SendCommand[2] = (byte)((iAddress - iAddress % 256) / 256);
            SendCommand[3] = (byte)(iAddress % 256);
            SendCommand[4] = (byte)((iLength - iLength % 256) / 256);
            SendCommand[5] = (byte)(iLength % 256);
            strSendCommand = "";
            for (i = 0; i < 6; i++)
            {
                strSendCommand = strSendCommand + SendCommand[i].ToString("X2");
            }

            strLRC = LRC(strSendCommand);
            strSendCommand = ":" + strSendCommand + strLRC;
            strSendCommand = strSendCommand + "\r" + "\n";
            System.Diagnostics.Trace.WriteLine(strSendCommand);
            //发送指令。
            MyCom.Write(strSendCommand);
            mReceiveByteCount = 0;
            //strUpData = "";
            strDownData = "";
            strDownData = strSendCommand;
            comBusying = true;//设置串口忙标志
            bCommWell = false;//设置本次通讯标志
        }
        #endregion

    }
}
