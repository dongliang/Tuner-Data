/*
   TTDB
   Tuner Text Data Base use for game static data read.
   e-mail : dongliang17@126.com  
*/
using System;
using System.Collections.Generic;
using UnityEngine;
namespace TTDB
{
    public class TDRoot : Singleton<TDRoot>
    {
        string m_DataTunnerContainerName = "TTDB_DataTuners";
        public string m_DataPath = null;
        public List<string> m_NameList = null;
        public Dictionary<string, TDFile> m_TableMap = null;

        public void Init(string dataPath,string[] nameList)
        {
            m_DataPath = dataPath;

            TDUtil.CreateDirectory(m_DataPath);

            if (nameList.Length == 0)
            {
                return;
            }
            m_NameList = new List<string>(nameList);
            m_TableMap = new Dictionary<string, TDFile>();
            foreach (string item_name in nameList)
            {
                //TODO 如果将来支持二进制格式的文件，在nameList中填写包含后缀名的文件名，然后再将后缀提取出来。
                TDFile tempFile = new TDFile(m_DataPath + item_name + ".txt");
                m_TableMap.Add(item_name, tempFile);
            }
        }
        public TDFile getTDFile(string name)
        {
            if (m_TableMap == null)
            {
                return null;
            }
            return m_TableMap[name];
        }

        public string GetFiledName(string tableName,int field)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return null;
            }

            return tempFile.GetFiledName(field);
        }

        public int GetRecordNum(string tableName)
        { 
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return 0;
            }
            return tempFile.GetRecordNum();
        }


        public int GetFieldNum(string tableName)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return 0;
            }
            return tempFile.GetFieldNum();
        }

        public TD_FIELD_TYPE GetFieldType(string tableName,int field)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return TD_FIELD_TYPE.T_INVALID;
            }
            return tempFile.GetFieldType(field);
        }

        public TDField GetData(string tableName,int index, int field)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return null;
            }
            return tempFile.GetData(index, field);
        }

        public TDLine GetData(string tableName,int index)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return null;
            }
            return tempFile.GetData(index);

        }
        //start 0
        public TDLine GetDataByRowNum(string tableName,int row)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return null;
            }
            return tempFile.GetDataByRowNum(row);

        }

        public object GetDataByNum(string tableName,int row, int field)
        {
              TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return null;
            }
            return tempFile.GetDataByNum(row, field);
        }

        public TDLine Search_First_Column_Equ(string tableName,int field, object value)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return null;
            }
            return tempFile.Search_First_Column_Equ(field, value);
        }

        public void Save(string tableName)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return;
            }
            tempFile.Save();
        }
        public void SaveAs(string tableName,string fullPath)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return;
            }
            tempFile.SaveAs(fullPath);
        }

        public void SetData(string tableName,int index, int field, object value)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return;
            }
            tempFile.SetData(index, field, value);
        }

        public string Serialize(string tableName)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return null;
            }
            return tempFile.Serialize();
        }

        public void AddDataTunner(string tableName,int index)
        {
            TDFile tempFile = getTDFile(tableName);
            if (tempFile == null)
            {
                return;
            }
            TDLine tempLine = tempFile.GetData(index);
            if (tempLine == null)
            {
                return;
            }
            GameObject container_go = GameObject.Find(m_DataTunnerContainerName);
            if (container_go == null)
            {
                container_go = new GameObject(m_DataTunnerContainerName);
            }
            GameObject tempTunner_go = new GameObject("TTDB_Tuner_" + tableName + "_" + index);
            tempTunner_go.transform.parent = container_go.transform;
            TTDBMonoTunner tunner = tempTunner_go.AddComponent<TTDBMonoTunner>();
            tunner.SetDataSource(tableName, tempLine);
        }
    }
}