using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.IO;
using System.Xml;

namespace DAL
{
    /// <summary>
    /// 对Scada所用的XML文件进行访问的类
    /// </summary>
    public class XMLService
    {
        // 存放变量的XML文件路径
        private static string pathVar = System.Windows.Forms.Application.StartupPath + "\\ConfigFile\\" + "Variable_Modbus.xml";

        // 存放报警变量的xml文件路径
        private static string pathVarAlarm = System.Environment.CurrentDirectory + @"\ConfigFile\" + "VariableAlarm_Modbus.xml";

        // 存放存储区域的xml文件路径
        private static string pathStoreArea = System.Windows.Forms.Application.StartupPath + "\\ConfigFile\\" + "StoreArea.xml";

        // 存放串口属性设置信息的ini文件路径
        private static string pathModbusPort = System.Windows.Forms.Application.StartupPath + "\\ConfigFile\\" + "ModbusPort.ini";

        /// <summary>
        /// 获取所有的变量对象
        /// </summary>
        /// <param name="xmlpath">存放变量的xml文件路径</param>
        /// <returns></returns>
        public static List<Variable_Modbus> GetLIstVar()
        {

            if (!File.Exists(pathVar)) return null;

            List<Variable_Modbus> listVar = new List<Variable_Modbus>();

            //从指定路径加载xml文档
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(pathVar);

            //获取“Root”节点
            XmlNode noodroot = xdoc.SelectSingleNode("//Root");


            //读取Root节点下所有的变量节点，封装成变量对象，存入集合
            foreach (XmlNode noodtool in noodroot.ChildNodes)
            {
                if (noodtool.Name == "Variable")
                {
                    //封装变量对象
                    Variable_Modbus objVar = new Variable_Modbus();
                    objVar.VarName = GetValueByAttribute(noodtool, "VarName");
                    objVar.Address = GetValueByAttribute(noodtool, "Address");
                    objVar.DataType = GetValueByAttribute(noodtool, "DataType");
                    objVar.StoreType = GetValueByAttribute(noodtool, "StoreArea");
                    objVar.Note = GetValueByAttribute(noodtool, "Note");
                    objVar.IsFiling = GetValueByAttribute(noodtool, "IsFiling");
                    objVar.IsAlarm = GetValueByAttribute(noodtool, "IsAlarm");
                    objVar.IsReport = GetValueByAttribute(noodtool, "IsReport");
                    objVar.AbsoluteAddress = GetValueByAttribute(noodtool, "AbsoluteAddress");

                    listVar.Add(objVar);
                }
            }

            return listVar;
        }


        /// <summary>
        /// 获取所有的报警变量
        /// </summary>
        /// <returns></returns>
        public static List<VarAlarm_Modbus> GetListVarAlarm()
        {

            if (!File.Exists(pathVarAlarm)) return null;

            List<VarAlarm_Modbus> listVarAlarm = new List<VarAlarm_Modbus>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(pathVarAlarm);
            XmlNode noodroot = xdoc.SelectSingleNode("//Root");

            foreach (XmlNode nood in noodroot.ChildNodes)
            {
                if (nood.Name == "VariableAlarm")
                {
                    //封装报警变量对象
                    VarAlarm_Modbus objVar = new VarAlarm_Modbus();
                    objVar.VarName = GetValueByAttribute(nood, "VarName");
                    objVar.Priority = Convert.ToInt32(GetValueByAttribute(nood, "Priority"));
                    objVar.AlarmType = GetValueByAttribute(nood, "AlarmType");
                    objVar.AlarmValue = float.Parse(GetValueByAttribute(nood, "AlarmValue"));
                    objVar.Note = GetValueByAttribute(nood, "Note");
                    //将对象存入集合
                    listVarAlarm.Add(objVar);
                }
            }

            return listVarAlarm;
        }

        /// <summary>
        /// 获取所有的存储区域对象
        /// </summary>
        /// <returns></returns>
        public static List<StoreArea> GetLIstStoreArea()
        {

            if (!File.Exists(pathStoreArea)) return null;

            List<StoreArea> listStoreArea = new List<StoreArea>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(pathStoreArea);
            XmlNode noodroot = xdoc.SelectSingleNode("//Root");

            foreach (XmlNode noodtool in noodroot.ChildNodes)
            {
                if (noodtool.Name == "StoreArea")
                {
                    StoreArea objVar = new StoreArea();
                    objVar.StoreType = GetValueByAttribute(noodtool, "StoreType");
                    objVar.StartReg = int.Parse(GetValueByAttribute(noodtool, "StartReg"));
                    objVar.Length = int.Parse(GetValueByAttribute(noodtool, "Length"));
                    listStoreArea.Add(objVar);
                }
            }
            return listStoreArea;
        }

        /// <summary>
        /// 读取ini文件中串口的属性信息
        /// </summary>
        /// <returns></returns>
        public static List<string> GetPortSet()
        {
            List<string> list = new List<string>();
            using (FileStream fs = new FileStream(pathModbusPort, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    while (true)
                    {
                        string str = sr.ReadLine();
                        if (str == null) break;
                        list.Add(str.Trim());
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 将变量集合存至XML文件 
        /// </summary>
        /// <param name="listVar"></param>
        public static void SaveListVar(List<Variable_Modbus> listVar)
        {
            XmlDocument xmldoc = new XmlDocument();
            //创建第一行描述信息
            XmlDeclaration dec = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmldoc.AppendChild(dec);
            //创建根节点
            XmlElement rootnode = xmldoc.CreateElement("Root");
            //给根节点添加子节点(变量)
            foreach (Variable_Modbus item in listVar)
            {
                //子节点名称都为Variable
                XmlElement xmle = xmldoc.CreateElement("Variable");
                //给该节点添加属性
                AppendAttribute(xmldoc, "VarName", item.VarName, xmle);
                AppendAttribute(xmldoc, "Address", item.Address, xmle);
                AppendAttribute(xmldoc, "DataType", item.DataType, xmle);
                AppendAttribute(xmldoc, "StoreArea", item.StoreType, xmle);
                AppendAttribute(xmldoc, "Note", item.Note, xmle);
                AppendAttribute(xmldoc, "IsFiling", item.IsFiling, xmle);
                AppendAttribute(xmldoc, "IsAlarm", item.IsAlarm, xmle);
                AppendAttribute(xmldoc, "IsReport", item.IsReport, xmle);
                AppendAttribute(xmldoc, "AbsoluteAddress", item.AbsoluteAddress, xmle);

                rootnode.AppendChild(xmle);

            }
            xmldoc.AppendChild(rootnode);


            if (File.Exists(pathVar)) File.Delete(pathVar);

            xmldoc.Save(pathVar);
        }

        /// <summary>
        /// 将存储区域对象集合存至XML文件
        /// </summary>
        /// <param name="listStore"></param>
        public static void SavaListStoreArea(List<StoreArea> listStore)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration dec = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmldoc.AppendChild(dec);

            XmlElement rootnode = xmldoc.CreateElement("Root");
            foreach (StoreArea item in listStore)
            {
                XmlElement xmle2 = xmldoc.CreateElement("StoreArea");
                AppendAttribute(xmldoc, "StoreType", item.StoreType, xmle2);
                AppendAttribute(xmldoc, "StartReg", item.StartReg.ToString(), xmle2);
                AppendAttribute(xmldoc, "Length", item.Length.ToString(), xmle2);
                rootnode.AppendChild(xmle2);
            }
            xmldoc.AppendChild(rootnode);
            if (File.Exists(pathStoreArea)) File.Delete(pathStoreArea);

            xmldoc.Save(pathStoreArea);
        }

        /// <summary>
        /// 将报警对象集合存到XML文件
        /// </summary>
        /// <param name="listVarAlarm"></param>
        public static void SaveListVarAlarm(List<VarAlarm_Modbus> listVarAlarm)
        {

            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration dec = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmldoc.AppendChild(dec);

            XmlElement rootnode = xmldoc.CreateElement("Root");
            foreach (VarAlarm_Modbus item in listVarAlarm)
            {
                XmlElement xmle3 = xmldoc.CreateElement("VariableAlarm");
                AppendAttribute(xmldoc, "VarName", item.VarName, xmle3);
                AppendAttribute(xmldoc, "Priority", item.Priority.ToString(), xmle3);
                AppendAttribute(xmldoc, "AlarmType", item.AlarmType, xmle3);
                AppendAttribute(xmldoc, "AlarmValue", item.AlarmValue.ToString(), xmle3);
                AppendAttribute(xmldoc, "Note", item.Note, xmle3);
                rootnode.AppendChild(xmle3);
            }
            xmldoc.AppendChild(rootnode);
            if (File.Exists(pathVarAlarm))
            {
                File.Delete(pathVarAlarm);
            }
            xmldoc.Save(pathVarAlarm);
        }

        /// <summary>
        /// 给节点添加属性信息
        /// </summary>
        /// <param name="xmlDoc">xml文档对象</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        /// <param name="element"></param>
        public static void AppendAttribute(XmlDocument xmlDoc, string name, string value, XmlElement element)
        {
            XmlAttribute att = xmlDoc.CreateAttribute(name);
            att.Value = value;
            element.Attributes.Append(att);
        }

        /// <summary>
        /// 节点属性信息,根据节点及节点名称获取相应的Value
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <param name="head"></param>
        public static string GetValueByAttribute(XmlNode nood, string attributeName)
        {

            if (nood != null && nood.Attributes[attributeName] != null)
            {
                return nood.Attributes[attributeName].Value;
            }
            else
            {
                return null;
            }
        }

    }
}
