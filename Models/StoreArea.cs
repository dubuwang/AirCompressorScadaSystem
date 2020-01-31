using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 存储区域类
    /// </summary>
    public class StoreArea
    {
        /// <summary>
        /// 存储区域类型
        /// </summary>
        public string StoreType { get; set; }
        /// <summary>
        /// 该存储区的起始地址
        /// </summary>
        public int StartReg { get; set; }
        /// <summary>
        /// 存储区长度
        /// </summary>
        public int Length { get; set; }
    }
}
