using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 报警变量实体类
    /// </summary>
    public class VarAlarm_Modbus
    {
        /// <summary>
        /// 变量名称,和变量名称是对应的
        /// </summary>
        public string VarName { get; set; }
        /// <summary>
        /// 报警类型(High HiHi Low LoLo)
        /// </summary>
        public string AlarmType { get; set; }
        /// <summary>
        /// 报警数值
        /// </summary>
        public float AlarmValue { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Note { get; set; }
    }
}
