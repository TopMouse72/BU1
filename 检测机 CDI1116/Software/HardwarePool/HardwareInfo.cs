using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Colibri.CommonModule.XML;

namespace HardwarePool
{
    public class HardwareInfo
    {
        public string ver;
        public string Description = "";
        public string UpdateDate;
        private HardwareInfo() { }
        public HardwareInfo(string version, string descrip, DateTime date)
        {
            ver = version;
            Description = descrip;
            UpdateDate = date.ToShortDateString();
        }
        public static bool operator ==(HardwareInfo hard1, HardwareInfo hard2)
        {
            return hard1.UpdateDate == hard2.UpdateDate && hard1.Description == hard2.Description;
        }
        public static bool operator !=(HardwareInfo hard1, HardwareInfo hard2)
        {
            return !(hard1.UpdateDate == hard2.UpdateDate && hard1.Description == hard2.Description);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public class HardwareInfoList
    {
        [XmlArrayItem(ElementName = "InPortIndex")]
        public List<int> DamageInList = new List<int>();
        [XmlArrayItem(ElementName = "OutPortIndex")]
        public List<int> DamageOutList = new List<int>();
        private static string xmlFile = "HardwareConfig.xml";
        private static SerialXML xml = new SerialXML();
        private static HardwareInfo currentInfo;
        public List<HardwareInfo> Hardwares = new List<HardwareInfo>();
        public HardwareInfoList()
        {
            currentInfo = new HardwareInfo(getModuleNameVersionList()["HardWareEnum"].ToString(),
          HardwareChangeReport.strDescription, HardwareChangeReport.CurrentUpdateDate);
        }
        public static SortedList getModuleNameVersionList()
        {

            SortedList NameVersionCollection = new SortedList();
            char[] delimiter = { ',' };
            char[] verDelimiter = { '=' };

            Assembly[] assmblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assmblies)
            {

                string name = assembly.FullName;

                string[] nameTokens = name.Split(delimiter);

                string assemblyName = nameTokens[0];



                if (assemblyName.IndexOf("System") == -1 && assemblyName != "mscorlib")
                {
                    string[] versionTokens = nameTokens[1].Split(verDelimiter);
                    string version = versionTokens[1];

                    if (!NameVersionCollection.Contains(assemblyName))
                    {

                        ////Treat the executable differently
                        //if (assembly.CodeBase.ToLower().EndsWith(".exe"))
                        //{
                        //    string AssemblyTitle = ((AssemblyTitleAttribute)(assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0])).Title;
                        //    version = "??.??.??";
                        //    if (AssemblyTitle.Length >= 8)
                        //    {
                        //        version = AssemblyTitle.Remove(0, AssemblyTitle.Length - 8);
                        //    }

                        //}

                        NameVersionCollection.Add(assemblyName, version);
                    }
                }
            }

            return NameVersionCollection;
        }
        public static HardwareInfoList ReadSetting()
        {
            HardwareInfoList temp = null;
            temp = (HardwareInfoList)xml.ReadSetting(xmlFile, "HardwareInfoList", typeof(HardwareInfoList));
            if (temp == null) temp = new HardwareInfoList();
            for (int i = 0; i < HardwareChangeReport.damagedInPort.Length; i++) temp.DamageInList.Add(HardwareChangeReport.damagedInPort[i]);
            for (int i = 0; i < HardwareChangeReport.damagedOutPort.Length; i++) temp.DamageOutList.Add(HardwareChangeReport.damagedOutPort[i]);
            foreach (HardwareInfo hard in temp.Hardwares)
                if (hard == currentInfo) return temp;
            temp.Hardwares.Add(currentInfo);
            return temp;
        }
        public static void SaveSetting(HardwareInfoList hardwares)
        {
            xml.SaveSetting(xmlFile, "HardwareInfoList", hardwares, System.IO.FileMode.Create);
        }
    }
}