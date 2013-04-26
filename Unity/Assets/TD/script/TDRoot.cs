/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TD
{
    public class TDRoot : Singleton<TDRoot>
    {

        Dictionary<string, Table> m_TableMap = new Dictionary<string, Table>();

        string m_DataTunnerContainerName = "TTDB_DataTuners";
        /// <summary>
        /// Open a Data File or a Folder.
        /// The Path is DataPath and SchemaPath.
        /// </summary>
        /// <param name="path">path</param>
        /// <returns></returns>
        public bool Open(string a_path)
        {
            //生成读取器
            IDataReader reader = TDFactory.Instance.GetDataReader(a_path);
            Schema tempSchema = reader.ReadSchema();
            if (tempSchema == null)
            {
                return false;
            }
            Table tempTable = new Table(reader);
            m_TableMap.Add(tempSchema.ClassName, tempTable);
            return true;
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
    }
}
