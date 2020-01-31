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
using System.Xml;
using System.IO;
using Common;

namespace Scada
{
    public partial class FrmIOVarManage : Form
    {
        public FrmIOVarManage()
        {
            InitializeComponent();

            //从xml文件读取出变量对象集合
            listVar = XMLService.GetLIstVar();
            //从xml文件读取出报警变量对象集合
            listVarAlarm = XMLService.GetListVarAlarm();

            DataGridViewStyle.DoubleBuffered(this.dgvVar,true);
            this.dgvVar.AutoGenerateColumns = false;
            //更新dgv
            this.UpdatedgvVar();
        }

        /// <summary>
        /// 变量对象集合
        /// </summary>
        List<Variable_Modbus> listVar = new List<Variable_Modbus>();
        /// <summary>
        /// 报警变量对象集合
        /// </summary>
        List<VarAlarm_Modbus> listVarAlarm = new List<VarAlarm_Modbus>();


        public static FrmNewVar objFrmNew = null;


        /// <summary>
        /// 新建变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (objFrmNew == null)
            {
                objFrmNew = new FrmNewVar();
                objFrmNew.actionTrans -= ReceiveVarFromNew;
                objFrmNew.actionTrans += ReceiveVarFromNew;
                objFrmNew.Show();
            }
            else
            {
                objFrmNew.Activate();
                objFrmNew.WindowState = FormWindowState.Normal;
            }

        }

        /// <summary>
        /// 修改变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (this.dgvVar.SelectedRows.Count != 1 || this.dgvVar.CurrentRow == null) return;

            #region 封装要修改的变量和其对应的报警对象集合

            //将dgv中选中列对应的变量对象从集合中读取出来
            int index = this.dgvVar.CurrentRow.Index;
            Variable_Modbus objVar = this.listVar[index];

            //变量对象对应的报警对象集合
            List<VarAlarm_Modbus> listAlarm = null;
            if (objVar.IsAlarm == "1")
            {
                listAlarm = new List<VarAlarm_Modbus>();
                //如果变量是设置为报警的，则从报警变量对象集合中取出其对应的报警变量，封装到集合中
                foreach (VarAlarm_Modbus item in this.listVarAlarm)
                {
                    if (item.VarName == objVar.VarName)
                    {
                        listAlarm.Add(item);
                    }
                }
            }
            #endregion

            //创建修改变量窗体，将要修改的变量和其对应的变量对象集合传递进去
            FrmModifyVar objFrm = new FrmModifyVar(objVar, listAlarm);
            DialogResult dr = objFrm.ShowDialog();

            //修改变量窗体的变量修改成功后
            if (dr == DialogResult.OK)
            {
                //集合中替换新修改的变量
                this.listVar[index] = objFrm.objVar;


                //更新新修改变量对应的报警变量
                if (objFrm.objVar.IsAlarm == "1" && objFrm.listVarAlarm.Count > 0)
                {
                    //该变量是报警变量:则在报警变量对象集合中先清除原来的再添加新修改后的

                    for (int i = 0; i < listVarAlarm.Count; i++)
                    {
                        if (listVarAlarm[i].VarName == objFrm.objVar.VarName)
                        {
                            listVarAlarm.RemoveAt(i);
                            i--;
                        }
                    }

                    this.listVarAlarm.AddRange(objFrm.listVarAlarm);
                }
                else
                {
                    //该变量不是报警变量:则在报警变量集合中删除其关联的报警变量
                    for (int i = 0; i < listVarAlarm.Count; i++)
                    {
                        if (listVarAlarm[i].VarName == objFrm.objVar.VarName)
                        {
                            listVarAlarm.RemoveAt(i);
                            i--;
                        }
                    }
                }

                UpdatedgvVar();
            }

        }

        /// <summary>
        /// 删除变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvVar.SelectedRows.Count == 1 && this.dgvVar.CurrentRow != null)
            {
                if (listVar[this.dgvVar.CurrentRow.Index].IsAlarm == "1")
                {
                    for (int i = 0; i < listVarAlarm.Count; i++)
                    {
                        if (listVarAlarm[i].VarName == listVar[this.dgvVar.CurrentRow.Index].VarName)
                        {
                            listVarAlarm.RemoveAt(i);
                            i--;
                        }
                    }
                }

                this.listVar.RemoveAt(this.dgvVar.CurrentRow.Index);
            }
        }

        /// <summary>
        /// 将变量集合 存储区 报警变量集合 存入xml文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ///保存分三步进行
            #region 第一步保存变量列表至XML文件

            XMLService.SaveListVar(this.listVar);

            #endregion

            #region 第二步保存存储区域至XML文件

            List<StoreArea> listStore = GetListStoreArea();
            XMLService.SavaListStoreArea(listStore);

            #endregion

            #region 第三步保存报警变量列表至XML文件

            XMLService.SaveListVarAlarm(this.listVarAlarm);

            #endregion

            MessageBox.Show("变量信息保存成功", "保存提示");

            this.listVar = XMLService.GetLIstVar();
            this.UpdatedgvVar();
        }

        /// <summary>
        /// FrmNew窗体的委托所绑定的方法，该方法参数是从FrmNew窗体传过来
        /// </summary>
        /// <param name="var"></param>
        /// <param name="listVAlarm"></param>
        private void ReceiveVarFromNew(Variable_Modbus var, List<VarAlarm_Modbus> listVAlarm)
        {
            //将新建变量窗体要新建的变量和报警变量添加到集合中
            this.listVar.Add(var);

            if (listVAlarm != null) this.listVarAlarm.AddRange(listVAlarm);

            //更新dgv
            UpdatedgvVar();
            this.dgvVar.CurrentCell = this.dgvVar.Rows[this.dgvVar.Rows.Count - 1].Cells[0];
        }

        private void UpdatedgvVar()
        {
            this.dgvVar.DataSource = null;
            this.dgvVar.DataSource = this.listVar;
        }

        private void FrmIOVarManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmIOVarManage.objFrmNew != null)
            {
                FrmIOVarManage.objFrmNew.Close();
            }
        }

        /// <summary>
        /// 根据当前变量集合中各个变量的存储区，计算出存储区域对象的集合
        /// </summary>
        /// <returns></returns>
        private List<StoreArea> GetListStoreArea()
        {
            List<StoreArea> listStore = new List<StoreArea>();

            List<Variable_Modbus> list_0x = new List<Variable_Modbus>();
            List<Variable_Modbus> list_1x = new List<Variable_Modbus>();
            List<Variable_Modbus> list_3x = new List<Variable_Modbus>();
            List<Variable_Modbus> list_4x = new List<Variable_Modbus>();

            //遍历所有的变量，将他们按存储区域进行分类
            foreach (Variable_Modbus item in this.listVar)
            {
                if (item.StoreType == "01 Coil Status(0x)")
                {
                    list_0x.Add(item);
                }
                if (item.StoreType == "02 Input Status(1x)")
                {
                    list_1x.Add(item);
                }
                if (item.StoreType == "03 Holding Register(4x)")
                {
                    list_4x.Add(item);
                }
                if (item.StoreType == "04 Input Registers(3x)")
                {
                    list_3x.Add(item);
                }
            }
            //在存储区域集合中添加一个存储类型为0区的存储区域对象
            listStore.Add(new StoreArea()
            {
                StoreType = "01 Coil Status(0x)",
                StartReg = AnalyseVar(list_0x)[0],  //存储区的起始地址
                Length = AnalyseVar(list_0x)[1]     //存储区的长度
            });

            //在存储区域集合中添加一个存储类型为4区的存储区域对象
            listStore.Add(new StoreArea()
            {
                StoreType = "03 Holding Register(4x)",
                StartReg = AnalyseVar(list_4x)[0],
                Length = AnalyseVar(list_4x)[1]
            });

            return listStore;
        }

        /// <summary>
        /// 对变量集合进行分析，返回第一个数值为该变量类型的起始地址，第二个数值为长度
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<int> AnalyseVar(List<Variable_Modbus> list)
        {
            List<int> listResult = new List<int>(2);
            List<int> listAddress = new List<int>();

            int start = 0;
            int end = 0;
            int length = 0;
            string dataType = string.Empty;

            //遍历所有的变量
            foreach (Variable_Modbus item in list)
            {
                //将变量的地址转成int型数据，存入地址集合中
                listAddress.Add(int.Parse(item.Address));
            }

            //获取起始地址，最小值
            start = listAddress.Min();
            //获取结束地址，最大值
            end = listAddress.Max();

            //查找出地址为最小值的变量,获取其数据类型
            Variable_Modbus lin = (from n in list where n.Address == end.ToString() select n).FirstOrDefault();
            dataType = lin.DataType;

            //判断其数据类型，获取存储区的总长度length
            switch (dataType)
            {
                case "Bool":
                    length = end - start + 1;
                    break;
                case "Float":
                    length = end - start + 2;
                    break;
                case "Float Inverse":
                    length = end - start + 2;
                    break;
                case "Long":
                    length = end - start + 2;
                    break;
                case "Long Inverse":
                    length = end - start + 2;
                    break;
                case "Double":
                    length = end - start + 4;
                    break;
                case "Double Inverse":
                    length = end - start + 4;
                    break;
                default:
                    length = end - start + 2;
                    break;

            }
            listResult.Add(start);
            listResult.Add(length);
            return listResult;
        }


        private void dgvVar_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewStyle.DgvRowPostPaint(this.dgvVar, e);
        }

    }
}
