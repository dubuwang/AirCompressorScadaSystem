using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SysAdminService
    {
        /// <summary>
        /// 管理员登陆，根据登陆账号和密码从数据库比对
        /// </summary>
        /// <param name="objAdmin">包含密码和账号的管理员对象</param>
        /// <returns></returns>
        public SysAdmin AdminLogin(SysAdmin objAdmin)
        {
            //1.定义sql语句
            string sql = "select LoginName,Role,LongId from SysAdmins where ";
            sql += "LongId=@LongId and LoginPwd=@LoginPwd";
            //2.封装参数
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@LongId",objAdmin.LoginId),
                new SqlParameter("@LoginPwd",objAdmin.LoginPwd)
            };


            //3.执行查询
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql, param);
                if (objReader.Read())
                {
                    objAdmin.LoginName = objReader["LoginName"].ToString();
                    objAdmin.Role = Convert.ToInt32(objReader["Role"].ToString());
                    objAdmin.LoginId = Convert.ToInt32(objReader["LongId"].ToString());
                    objReader.Close();
                }
                else
                {
                    objAdmin = null;
                }
            }
            catch (SqlException ex)
            {
                objAdmin = null;
                throw new Exception("数据库访问出错：" + ex.Message);
            }
            catch (Exception ex)
            {
                objAdmin = null;
                throw ex;
            }


            return objAdmin;
        }
    }
}
