/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TD
{
    //tableName = ClassName = FileName
    public class TDRoot : Singleton<TDRoot>
    {

        Dictionary<string, Table> m_TableMap = new Dictionary<string, Table>();
        Dictionary<string, string> m_FileMap = new Dictionary<string, string>();

        string m_DataTunnerContainerName = "TTDB_DataTuners";

        //file name is table name.
        public bool Open(string a_path)
        {
            if (!string.IsNullOrEmpty(a_path))
            {
                a_path = a_path.Replace(@"\", @"/");
            }
            IDataReader reader = TDFactory.Instance.GetDataReader(a_path);
            Schema tempSchema = reader.ReadSchema();
            if (tempSchema == null)
            {
                return false;
            }
            Table tempTable = new Table(reader);
            if (m_TableMap.ContainsKey(tempSchema.ClassName))
            {
                m_TableMap[tempSchema.ClassName] = tempTable;
            }
            else
            {
                m_TableMap.Add(tempSchema.ClassName, tempTable);
            }

            if (m_FileMap.ContainsKey(tempSchema.ClassName))
            {
                m_FileMap[tempSchema.ClassName] = a_path;
            }
            else
            {
                m_FileMap.Add(tempSchema.ClassName, a_path);
            }


            return true;
        }
        //file name is table name.
        public bool Save(string name, E_DataFile_Type type)
        {
            //get path and change the suffix.
            string path = null;
            m_FileMap.TryGetValue(name, out path);
            if (path != null)
            {
                string[] sArr2 = path.Split(new char[] { '.' });
                path = sArr2[0] + "." + TDFactory.Instance.GetFileSuffix(type);
                return SaveAs(name, path);
            }
            else
            {
                return false;
            }
        }

        public bool Save(string name, string folder, E_DataFile_Type type)
        {
            if (!string.IsNullOrEmpty(folder))
            {
                folder = folder.Replace(@"\", @"/");
            }
            if (folder != null)
            {
                return SaveAs(name, folder + name + "." + TDFactory.Instance.GetFileSuffix(type));
            }
            else
            {
                return false;
            }
        }



        //file name is table name.
        public bool SaveAs(string a_name, string a_path)
        {
            if (!string.IsNullOrEmpty(a_path))
            {
                a_path = a_path.Replace(@"\", @"/");
            }
            IDataWriter writer = TDFactory.Instance.GetDataWriter(a_path);
            Table table = null;
            m_TableMap.TryGetValue(a_name, out table);
            if (table != null)
            {
                return table.Write(writer);
            }
            else
            {
                return false;
            }
        }

        public Table getTable(string name)
        {
            Table temp = null;
            m_TableMap.TryGetValue(name, out temp);
            return temp;
        }

        public void AddDataTunner(string tableName, int index)
        {
            Row tempRow = getTable(tableName).GetRow(index);

            GameObject container_go = GameObject.Find(m_DataTunnerContainerName);
            if (container_go == null)
            {
                container_go = new GameObject(m_DataTunnerContainerName);
            }
            GameObject tempTunner_go = new GameObject("TTDB_Tuner_" + tableName + "_" + index);
            tempTunner_go.transform.parent = container_go.transform;
            TTDBMonoTunner tunner = tempTunner_go.AddComponent<TTDBMonoTunner>();
            tunner.SetDataSource(tableName, tempRow);
        }

        public void GenerateStruct(string tableName, string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                path = path.Replace(@"\", @"/");
            }
            Table table = getTable(tableName);
            if (table == null)
            {
                return;
            }
            Schema schema = table.m_Schema;
            StructGen.Instance.Generate(schema, path);
        }
    }
}
