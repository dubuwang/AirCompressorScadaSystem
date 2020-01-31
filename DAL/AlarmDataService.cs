using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// AlarmData数据表访问类
    /// </summary>
    public class AlarmDataService
    {
        /// <summary>
        /// 向数据插入报警数据
        /// </summary>
        /// <param name="VarName"></param>
        /// <param name="AlarmState"></param>
        /// <param name="Priority"></param>
        /// <param name="AlarmType"></param>
        /// <param name="Value"></param>
        /// <param name="AlarmValue"></param>
        /// <param name="Operator"></param>
        /// <param name="Note"></param>
        public void InsertAlarmData(string VarName, string AlarmState, int Priority, string AlarmType, float Value, float AlarmValue, string Operator, string Note)
        {
            string sql = "Insert into AlarmData(InsertTime,VarName,AlarmState,Priority,AlarmType,Value,AlarmValue,Operator,Note) values('{0}','{1}','{2}',{3},'{4}',{5},{6},'{7}','{8}')";
            sql = string.Format(sql, DateTime.Now, VarName, AlarmState, Priority, AlarmType, Value, AlarmValue, Operator, Note);
            SQLHelper.Update(sql);
        }

        /// <summary>
        /// 查询指定数量的报警数据,返回一个DataTable
        /// </summary>
        /// <param name="showNum"></param>
        public DataTable QueryAlarmDataByNum(int showNum)
        {
            string sql = "select top " + showNum + " CONVERT(varchar(100), InsertTime, 20) as InsertTime,VarName,AlarmState,Priority,AlarmType,Value,AlarmValue,Operator,Note from AlarmData  Order By Id DESC";

            return (SQLHelper.GetDataSet(sql)).Tables[0];
        }

        /// <summary>
        /// 根据时间间隔，查询一定数目的报警数据
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtFinish"></param>
        /// <param name="showNum"></param>
        /// <returns></returns>
        public DataTable QueryAlarmDataByDate(DateTime dtStart,DateTime dtFinish,int showNum)
        {
           
            string sql = "select top {0} CONVERT(varchar(100), InsertTime, 20) as InsertTime,VarName,AlarmState,Priority,AlarmType,Value,AlarmValue,Operator,Note from AlarmData where 1=1 and InsertTime between '{1}' and '{2}' Order By Id DESC ";

            sql = string.Format(sql, showNum, dtStart, dtFinish);

            return SQLHelper.GetDataSet(sql).Tables[0];
        }
    }
}
