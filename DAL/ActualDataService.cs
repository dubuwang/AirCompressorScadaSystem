using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// ActualData数据表访问类
    /// </summary>
    public class ActualDataService
    {
        public DataTable GetActualDataBetweenTimePeriod(string varName,DateTime dtStart,DateTime dtFinish)
        {
            //通过变量名和时间区域，查找Value和InsertTime
            string sql = "select Value,CONVERT(varchar(100), InsertTime, 108) as Time from ActualData where VarName='{0}' and InsertTime between '{1}' and '{2}' ";

            sql = string.Format(sql, varName,dtStart, dtFinish);
            try
            {
                return SQLHelper.GetDataSet(sql).Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
            
        }
    }
}
