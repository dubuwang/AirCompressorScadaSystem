using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scada
{
    public partial class FrmView : Form
    {
        public FrmView()
        {
            InitializeComponent();

        }
        public FrmView(PLCService p)
            : this()
        {

            this.objPLCService = p;
        }

        private PLCService objPLCService;


        private void TapDoubleClick(object sender, EventArgs e)
        {
            
            #region 冷却泵
            if (sender is PictureBox)
            {
                if (((PictureBox)sender).Tag != null)
                {
                    string VarName = ((PictureBox)sender).Tag.ToString();
                    string Res = string.Empty;
                    bool State;
                    switch (VarName)
                    {
                        case "LQB1_Run_State":
                            if (PLCService.CurrentValue.ContainsKey(VarName))
                            {
                                Res = PLCService.CurrentValue[VarName];
                            }
                            State = Res == "1" ? true : false;
                            if (State)
                            {
                                Frm_Control objFrm = new Frm_Control("停止1#冷却泵", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            else
                            {
                                Frm_Control objFrm = new Frm_Control("启动1#冷却泵", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            break;
                        case "LQB2_Run_State":
                            if (PLCService.CurrentValue.ContainsKey(VarName))
                            {
                                Res = PLCService.CurrentValue[VarName];
                            }
                            State = Res == "1" ? true : false;
                            if (State)
                            {
                                Frm_Control objFrm = new Frm_Control("停止2#冷却泵", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            else
                            {
                                Frm_Control objFrm = new Frm_Control("启动2#冷却泵", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            break;
                        case "KYJ1_In_State":
                            if (PLCService.CurrentValue.ContainsKey(VarName))
                            {
                                Res = PLCService.CurrentValue[VarName];
                            }
                            State = Res == "1" ? true : false;
                            if (State)
                            {
                                Frm_Control objFrm = new Frm_Control("关闭1#空压机进液阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            else
                            {
                                Frm_Control objFrm = new Frm_Control("打开1#空压机进液阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            break;
                        case "KYJ2_In_State":
                            if (PLCService.CurrentValue.ContainsKey(VarName))
                            {
                                Res = PLCService.CurrentValue[VarName];
                            }
                            State = Res == "1" ? true : false;
                            if (State)
                            {
                                Frm_Control objFrm = new Frm_Control("关闭2#空压机进液阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            else
                            {
                                Frm_Control objFrm = new Frm_Control("打开2#空压机进液阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            break;
                        case "KYJ3_In_State":
                            if (PLCService.CurrentValue.ContainsKey(VarName))
                            {
                                Res = PLCService.CurrentValue[VarName];
                            }
                            State = Res == "1" ? true : false;
                            if (State)
                            {
                                Frm_Control objFrm = new Frm_Control("关闭3#空压机进液阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            else
                            {
                                Frm_Control objFrm = new Frm_Control("打开3#空压机进液阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            break;
                        case "CQG1_Out_State":
                            if (PLCService.CurrentValue.ContainsKey(VarName))
                            {
                                Res = PLCService.CurrentValue[VarName];
                            }
                            State = Res == "1" ? true : false;
                            if (State)
                            {
                                Frm_Control objFrm = new Frm_Control("关闭1#储气罐出气阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            else
                            {
                                Frm_Control objFrm = new Frm_Control("打开1#储气罐出气阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            break;
                        case "CQG2_Out_State":
                            if (PLCService.CurrentValue.ContainsKey(VarName))
                            {
                                Res = PLCService.CurrentValue[VarName];
                            }
                            State = Res == "1" ? true : false;
                            if (State)
                            {
                                Frm_Control objFrm = new Frm_Control("关闭2#储气罐出气阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            else
                            {
                                Frm_Control objFrm = new Frm_Control("打开2#储气罐出气阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            break;
                        case "CQG3_Out_State":
                            if (PLCService.CurrentValue.ContainsKey(VarName))
                            {
                                Res = PLCService.CurrentValue[VarName];
                            }
                            State = Res == "1" ? true : false;
                            if (State)
                            {
                                Frm_Control objFrm = new Frm_Control("关闭3#储气罐出气阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            else
                            {
                                Frm_Control objFrm = new Frm_Control("打开3#储气罐出气阀", !State, VarName, objPLCService);
                                objFrm.ShowDialog();
                            }
                            break;

                        default:
                            break;
                    }
                }

            }
            #endregion

        }


    }
}
