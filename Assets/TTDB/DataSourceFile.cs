/*
   Tuner Data - Used to read the static data  in game development.
   e-mail : dongliang17@126.com  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
namespace TD
{
    public class DataSourceFile
    {
        public string m_Path = null;
        public Table m_Table = null;

        public DataSourceFile(string fullPath)
        {
            Open(fullPath);
        }

        public void Open(string fullPath)
        {
            try
            {                
                string text = File.ReadAllText(fullPath);
                m_Path = fullPath;
                m_Table = new Table(text);
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        public void Refresh()
        {
            if (m_Path != null)
            {
                Open(m_Path);
            }
        }

        public void Save()
        {
            SaveAs(m_Path);
        }
        public void SaveAs(string fullPath)
        {
            if (m_Table == null)
            {
                return;
            }
            string outStr = m_Table.Serialize();
            File.WriteAllText(fullPath, outStr, Encoding.UTF8);
        }

        public string GetFiledName(int field)
        {
            if (m_Table == null)
            {
                return null;
            }

            return m_Table.GetFiledName(field);
        }

        public int GetRecordNum()
        {
            if (m_Table == null)
            {
                return 0;
            }
            return m_Table.GetRecordNum();
        }


        public int GetFieldNum()
        {
            if (m_Table == null)
            {
                return 0;
            }
            return m_Table.GetFieldNum();
        }

        public FIELD_TYPE GetFieldType(int field)
        {
            if (m_Table == null)
            {
                return FIELD_TYPE.T_INVALID;
            }
            return m_Table.GetFieldType(field);
        }

        public Field GetData(int index, int field)
        {
            if (m_Table == null)
            {
                return null;
            }
            return m_Table.GetData(index, field);
        }

        public Line GetData(int index)
        {
            if (m_Table == null)
            {
                return null;
            }
            return m_Table.GetData(index);

        }
        //start 0
        public Line GetDataByRowNum(int row)
        {
            if (m_Table == null)
            {
                return null;
            }
            return m_Table.GetDataByRowNum(row);

        }

        public object GetDataByNum(int row, int field)
        {
            if (m_Table == null)
            {
                return null;
            }
            return m_Table.GetDataByNum(row, field);
        }

        public Line Search_First_Column_Equ(int field, object value)
        {
            if (m_Table == null)
            {
                return null;
            }
            return m_Table.Search_First_Column_Equ(field, value);
        }

        public void SetData(int index, int field, object value)
        {
            if (m_Table == null)
            {
                return;
            }
            m_Table.SetData(index, field, value);
        }

        public string Serialize()
        {
            if (m_Table == null)
            {
                return null;
            }
            return m_Table.Serialize();
        }
    }
}