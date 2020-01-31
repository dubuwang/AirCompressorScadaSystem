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

namespace Scada
{
    public partial class Frm_ReportSet : Form
    {
        public Frm_ReportSet()
        {
            InitializeComponent();

            foreach (Variable_Modbus item in PLCService.listVarIsReport)
            {
                listUnSet.Add(item.Note);//初始化未选择的变量
                DicNoteVarName.Add(item.Note, item.VarName);
            }

            UpdateLst();
        }

        #region 字段
        //变量名称集合
        public List<string> listVarName = new List<string>();
        /// <summary>
        /// 未选中的变量的注释集合
        /// </summary>
        List<string> listUnSet = new List<string>();
        /// <summary>
        /// 已选中的变量的注释集合
        /// </summary>
        List<string> listSeted = new List<string>();
        /// <summary>
        /// 变量注释和变量名称的Dic集合
        /// </summary>
        Dictionary<string, string> DicNoteVarName = new Dictionary<string, string>();
        #endregion

        /// <summary>
        /// 更新ListBox
        /// </summary>
        private void UpdateLst()
        {
            //ListBox可以绑定一个string集合

            this.lstUnSelect.DataSource = null;
            this.lstSelected.DataSource = null;
            this.lstUnSelect.DataSource = this.listUnSet;
            this.lstSelected.DataSource = this.listSeted;
        }


        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (this.lstUnSelect.Items.Count == 0 || this.lstUnSelect.SelectedItem == null) return;
                
            string str = this.lstUnSelect.SelectedValue.ToString();
            this.listUnSet.Remove(str);
            this.listSeted.Add(str);
            
            UpdateLst();
        }

        private void btn_AddAll_Click(object sender, EventArgs e)
        {
            foreach (string item in this.listUnSet)
            {
                this.listSeted.Add(item);
            }
            this.listUnSet.Clear();
            UpdateLst();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (this.lstSelected.Items.Count == 0 || this.lstSelected.SelectedItem == null) return;
                
            string str = this.lstSelected.SelectedValue.ToString();
            this.listSeted.Remove(str);
            this.listUnSet.Add(str);
            UpdateLst();
        }

        private void btn_DelAll_Click(object sender, EventArgs e)
        {
            foreach (string item in this.listSeted)
            {
                this.listUnSet.Add(item);
            }
            this.listSeted.Clear();
            UpdateLst();
        }

        private void btn_Sure_Click(object sender, EventArgs e)
        {
            if (this.listSeted.Count == 0)
            {
                MessageBox.Show("请至少选择一个参数！", "选择提示");
                return;
            }
            foreach (string item in this.listSeted)
            {
                if (DicNoteVarName.ContainsKey(item))
                {
                    listVarName.Add(DicNoteVarName[item]);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
