using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DAL
{
    /// <summary>
    /// 往数据库插入变量数据的访问类
    /// </summary>
    public class DataInsert
    {
        public DataInsert()
        {
            timer = new Timer();
            timer.Interval =1000;
            timer.Elapsed += timer_Elapsed;
        }


        private Timer timer;

        /// <summary>
        /// Elapsed事件执行方法的线程锁，防止重入
        /// </summary>
        private static readonly Object lockInsert = new object();

        public bool StartInsert
        {
            get { return timer.Enabled; }
            set { timer.Enabled = value; }
        }


        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (lockInsert)
            {
                this.InsertActualData();
                if (DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)
                {
                    InsertDataHourReport();
                }
            }
            
        }

        #region 实时数据存储
        /// <summary>
        /// 实时的数据存储，将PLCService类中的变量对象存储到数据库中
        /// </summary>
        private void InsertActualData()
        {
            if (PLCService.CurrentValue != null && PLCService.CurrentValue.Count > 0)
            {
                List<string> sqlList = new List<string>();
                foreach (Variable_Modbus item in PLCService.listVarIsFiling)
                {
                    string varName = item.VarName;
                    string remark = PLCService.CurrentVarNote[item.VarName];
                    string sql = "Insert into ActualData(InsertTime,VarName,Value,Remark) values('{0}','{1}','{2}','{3}');";
                    double value = 0.0;
                    if (!PLCService.CurrentValue.ContainsKey(varName) || PLCService.CurrentValue[varName].Length == 0)
                    {
                        value = 0.0;
                    }
                    else
                    {
                        value = Convert.ToDouble(PLCService.CurrentValue[varName]);
                    }
                    sql = string.Format(sql, DateTime.Now, varName, value, remark);
                    sqlList.Add(sql);
                }

                SQLHelper.UpdateByTran(sqlList);
            }

        }
        #endregion

        #region 报表小时数据存储
        /// <summary>
        /// 报表小时数据存储
        /// </summary>
        private void InsertDataHourReport()
        {
            int count = PLCService.listVarIsReport.Count;
            if (count == 19)
            {
                List<string> array = new List<string>();
                foreach (Variable_Modbus item in PLCService.listVarIsReport)
                {
                    double value = 0.0;
                    if (PLCService.CurrentValue.ContainsKey(item.VarName))
                    {
                        string res = PLCService.CurrentValue[item.VarName];
                        if (res == "")
                        {
                            value = 0.0;
                        }
                        else
                        {
                            value = Convert.ToDouble(res);
                        }
                        array.Add(value.ToString("f1"));
                    }
                }

                StringBuilder sb = new StringBuilder("INSERT INTO ReportData (InsertTime,LQT_Level,LQT_InPre,LQT_InTemp,LQT_OutPre");
                sb.Append(",LQT_OutTemp,LQT_BSPre,LQB1_Current,LQB1_Fre,LQB2_Current,LQB2_Fre,KYJ1_OutTemp,KYJ2_OutTemp,KYJ3_OutTemp,CQG1_OutPre");
                sb.Append(",CQG2_OutPre,CQG3_OutPre,Env_Temp,FQG_Temp,FQG_Pre)");
                sb.Append(" values('" + DateTime.Now + "','" + array[0] + "','" + array[1]
                    + "','" + array[2] + "','" + array[3] + "','" + array[4] + "','" + array[5] + "','"
                    + array[6] + "','" + array[7] + "','" + array[8] + "','" + array[9] + "','" + array[10] + "','"
                    + array[11] + "','" + array[12] + "','" + array[13] + "','" + array[14] + "','" + array[15] + "','"
                    + array[16] + "','" + array[17] + "','" + array[18] + "')");

                SQLHelper.Update(sb.ToString());
            }
        }
        #endregion
    }
}
