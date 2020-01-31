using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 变量实体类
    /// </summary>
    public class Variable_Modbus
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarName { get; set; }
        /// <summary>
        /// 存储区域的类型
        /// </summary>
        public string StoreType { get; set; }
        /// <summary>
        /// 报文地址，单独仅仅读取该变量的起始地址。该变量的数据值，可能为一个线圈状态，也可能为寄存器的值，但无论如何，都有一个仅仅读取数据的起始地址。
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 是否归档
        /// </summary>
        public string IsFiling { get; set; }
        /// <summary>
        /// 是否报警
        /// </summary>
        public string IsAlarm { get; set; }
        /// <summary>
        /// 是否参与报表
        /// </summary>
        public string IsReport { get; set; }
        /// <summary>
        /// 变量注释说明
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 绝对地址，PLC设备中的Modbus地址
        /// </summary>
        private string absoluteAddress;
        public string AbsoluteAddress
        {
            get
            {
                int store = 0;
                switch (StoreType)
                {
                    case "01 Coil Status(0x)":
                        store = 0;
                        break;
                    case "02 Input Status(1x)":
                        store = 1;
                        break;
                    case "03 Holding Register(4x)":
                        store = 4;
                        break;
                    case "04 Holding Status(3x)":
                        store = 3;
                        break;
                    default:
                        store = 4;
                        break;
                }
                absoluteAddress = (store * 10000 + Convert.ToInt32(Address)).ToString();
                return absoluteAddress;
            }
            set
            {
                absoluteAddress = value;
            }
        }
    }
}
