using MS_DAL;
using MS_Enitity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MS_UI
{
  public static  class CommMethods
    {
        //变量信息集合
        public static List<Variable_Modbus> VarModbusList = new List<Variable_Modbus>();
        //报警变量信息集合
        public static List<VarAlarm_Modbus> VarAlarmModbusList = new List<VarAlarm_Modbus>();
        //存储区信息集合
        public static List<StoreArea> StoreModbusList = new List<StoreArea>();
        //变量名和数值的字典集合
        public static Dictionary<string, string> CurrentValue = new Dictionary<string, string>();
        //变量名和绝对地址的字典集合
        public static Dictionary<string, string> CurrentVarAdd = new Dictionary<string, string>();
        //PLC信息
        public static List<string> CommInfo = new List<string>();
        //归档变量集合
        public static List<Variable_Modbus> FileVarModbusList = new List<Variable_Modbus>();
        //报表变量集合
        public static List<Variable_Modbus> ReportVarModbusList = new List<Variable_Modbus>();
        //Moubus实例
        public static Modbus objMod = new Modbus();
        //Modbus从站地址
        public static int Address;
        //读写标志位
        public static bool IsWriting = false;
        //定义登录用户
        public static SysAdmins objAdmins = new SysAdmins();
        //定义通讯状态位
        public static bool CommOK = false;
        //定义MODBUS通讯参数
        public static ModbusEnitity objModbusEnitity = new ModbusEnitity();


      #region 向XML文件中添加节点属性信息
      /// <summary>
      /// 添加节点属性信息
      /// </summary>
      /// <param name="name"></param>
      /// <param name="value"></param>
      /// <param name="head"></param>
      public static void XMLAttributeAppend(XmlDocument rootxml, string name, string value, XmlElement head)
      {
          XmlAttribute att = rootxml.CreateAttribute(name);
          att.Value = value;
          head.Attributes.Append(att);
      }
      #endregion

      #region 根据节点及节点名称获取相应的Value
      /// <summary>
      /// 节点属性信息
      /// </summary>
      /// <param name="name"></param>
      /// <param name="value"></param>
      /// <param name="head"></param>
      public static string XMLAttributeGetValue(XmlNode rootxml, string name)
      {
          string resvalue = "";
          if (rootxml != null && rootxml.Attributes != null && rootxml.Attributes[name] != null)
          {
              resvalue = rootxml.Attributes[name].Value;
          }
          return resvalue;
      }
      #endregion

      #region 装载XML文件：根据路径返回Variable_Modbus  LIST集合
      /// <summary>
      /// 装载XML文件：根据路径返回LIST集合
      /// </summary>
      /// <param name="xmlpath"></param>
      /// <returns></returns>
      public static List<Variable_Modbus> LoadXML(string xmlpath)
      {
          VarModbusList.Clear();
          if (!File.Exists(xmlpath))
          {
              MessageBox.Show("IO配置变量的XML文件不存在！");
          }
          else
          {
              XmlDocument xdoc = new XmlDocument();
              xdoc.Load(xmlpath);
              foreach (XmlNode noodroot in xdoc.ChildNodes)
              {
                  if (noodroot.Name == "Root")
                  {
                      foreach (XmlNode noodtool in noodroot.ChildNodes)
                          if (noodtool.Name == "Variable")
                          {
                              Variable_Modbus objVar = new Variable_Modbus();
                              objVar.VarName = XMLAttributeGetValue(noodtool, "VarName");
                              objVar.Address = XMLAttributeGetValue(noodtool, "Address");
                              objVar.DataType = XMLAttributeGetValue(noodtool, "DataType");
                              objVar.StoreArea = XMLAttributeGetValue(noodtool, "StoreArea");
                              objVar.Note = XMLAttributeGetValue(noodtool, "Note");
                              objVar.IsFiling = XMLAttributeGetValue(noodtool, "IsFiling");
                              objVar.IsAlarm = XMLAttributeGetValue(noodtool, "IsAlarm");
                              objVar.IsReport = XMLAttributeGetValue(noodtool, "IsReport");
                              objVar.AbsoluteAddress = XMLAttributeGetValue(noodtool, "AbsoluteAddress");
                              VarModbusList.Add(objVar);
                          }
                  }
              }

          }
          return VarModbusList;
      }
      #endregion

      #region 装载XML文件：根据路径返回VariableAlarm_Modbus  LIST集合
      /// <summary>
      /// 装载XML文件：根据路径返回LIST集合
      /// </summary>
      /// <param name="xmlpath"></param>
      /// <returns></returns>
      public static List<VarAlarm_Modbus> LoadAlarmXML(string xmlpath)
      {
          VarAlarmModbusList.Clear();
          if (!File.Exists(xmlpath))
          {
              MessageBox.Show("IO配置变量的XML文件不存在！");
          }
          else
          {
              XmlDocument xdoc = new XmlDocument();
              xdoc.Load(xmlpath);
              foreach (XmlNode noodroot in xdoc.ChildNodes)
              {
                  if (noodroot.Name == "Root")
                  {
                      foreach (XmlNode noodtool in noodroot.ChildNodes)
                          if (noodtool.Name == "VariableAlarm")
                          {
                              VarAlarm_Modbus objVar = new VarAlarm_Modbus();
                              objVar.VarName = XMLAttributeGetValue(noodtool, "VarName");
                              objVar.Priority = Convert.ToInt32(XMLAttributeGetValue(noodtool, "Priority"));
                              objVar.AlarmType = XMLAttributeGetValue(noodtool, "AlarmType");
                              objVar.AlarmValue = float.Parse(XMLAttributeGetValue(noodtool, "AlarmValue"));
                              objVar.Note = XMLAttributeGetValue(noodtool, "Note");
                              VarAlarmModbusList.Add(objVar);
                          }
                  }
              }
          }
          return VarAlarmModbusList;
      }
      #endregion

      #region 装载XML文件：根据路径返回StoreModbusList  LIST集合
      /// <summary>
      /// 装载XML文件：根据路径返回LIST集合
      /// </summary>
      /// <param name="xmlpath"></param>
      /// <returns></returns>
      public static List<StoreArea> LoadStoreXML(string xmlpath)
      {
          StoreModbusList.Clear();
          if (!File.Exists(xmlpath))
          {
              MessageBox.Show("存储区的XML文件不存在！");
          }
          else
          {
              XmlDocument xdoc = new XmlDocument();
              xdoc.Load(xmlpath);
              foreach (XmlNode noodroot in xdoc.ChildNodes)
              {
                  if (noodroot.Name == "Root")
                  {
                      foreach (XmlNode noodtool in noodroot.ChildNodes)
                          if (noodtool.Name == "StoreArea")
                          {
                              StoreArea objVar = new StoreArea();
                              objVar.StoreType = XMLAttributeGetValue(noodtool, "StoreType");
                              objVar.StartReg = int.Parse(XMLAttributeGetValue(noodtool, "StartReg"));
                              objVar.Length = int.Parse(XMLAttributeGetValue(noodtool, "Length"));
                              StoreModbusList.Add(objVar);
                          }
                  }
              }

          }
          return StoreModbusList;
      }
      #endregion

      #region 读取因子、PLC信息文件
      /// <summary>
      /// 读取因子、PLC信息文件
      /// </summary>
      /// <param name="filename"></param>
      /// <returns></returns>
      public static List<string> GetFactor(string filename)
      {
          FileStream fs = new FileStream(filename, FileMode.Open);
          StreamReader sr = new StreamReader(fs, Encoding.Default);
          List<string> list = new List<string>();
          while (true)
          {
              string str = sr.ReadLine();
              if (str == null) break;
              list.Add(str.Trim());
          }
          sr.Close();
          fs.Close();
          return list;
      }
      #endregion

      #region DGV双缓冲
      /// <summary>
      /// dataGirdView刷新
      /// </summary>
      /// <param name="dgv"></param>
      /// <param name="setting"></param>
      public static void DoubleBuffered(this DataGridView dgv, bool setting)
      {
          Type dgvType = dgv.GetType();
          PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
          pi.SetValue(dgv, setting, null);
      }
      #endregion 
  }
}
