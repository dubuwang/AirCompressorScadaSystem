using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using DAL;
using Common;

namespace Scada
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();

            this.btnLogin.Parent = this.picbLogin;
            this.btnQuit.Parent = this.picbLogin;
            this.label1.Parent = this.picbLogin;
            this.label2.Parent = this.picbLogin;
            this.checkBox1.Parent = this.picbLogin;
           
        }

        private SysAdminService objAdminService = new SysAdminService();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //选中匿名登录，直接匿名身份登录，否则，判断用户名和密码是否正确
            if (this.checkBox1.Checked == false)
            {
                #region 数据验证
                if (this.txtLoginId.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请填写用户名!", "登录提示");
                    this.txtLoginId.Focus();
                    return;
                }
                if (!DataValidate.IsInteger(this.txtLoginId.Text.Trim()))
                {
                    MessageBox.Show("用户名必须为正整数!", "登录提示");
                    this.txtLoginId.Focus();
                    return;

                }
                if (this.txtLoginPwd.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请填写密码!", "登录提示");
                    this.txtLoginPwd.Focus();
                    return;
                }
                #endregion

                //封装对象
                SysAdmin objAdmin = new SysAdmin()
                {
                    LoginId = Convert.ToInt32(this.txtLoginId.Text.Trim()),
                    LoginPwd = this.txtLoginPwd.Text.Trim()
                };

                try
                {
                    //调用登陆数据库方法，进行账号验证
                    objAdmin = objAdminService.AdminLogin(objAdmin);

                    if (objAdmin == null)
                    {
                        MessageBox.Show("用户名或密码不正确!", "登录提示");
                        return;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("登陆异常，原因："+ex.Message, "登录提示");
                    return;
                }
                
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
