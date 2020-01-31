using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 报表数据表访问类
    /// </summary>
    public class ReportDataService
    {
        public DataTable GetReportDataByCondition(List<string> listVarName, DateTime DTstart, string reportType)
        {
            string strName = string.Empty;
            if (listVarName.Count == 0)
            {
                strName = "";
            }
            else
            {
                foreach (string item in listVarName)
                {
                    strName += "," + item;
                }
            }
            
            string condition = string.Empty;
            if (reportType == "班报表")
            {
                condition = "DateDiff(hh,'" + DTstart + "',InsertTime)>=0 and DateDiff(hh,'" + DTstart + "',InsertTime)<=7";
            }
            else if (reportType == "日报表")
            {
                condition = "DateDiff(hh,'" + DTstart + "',InsertTime)>=0 and DateDiff(hh,'" + DTstart + "',InsertTime)<=23";
            }
            else if (reportType == "周报表")
            {

            }

            string sql = " Select InsertTime" + strName + " from ReportData where  " + condition + "";

            return SQLHelper.GetDataSet(sql).Tables[0];
        }
    }
}
