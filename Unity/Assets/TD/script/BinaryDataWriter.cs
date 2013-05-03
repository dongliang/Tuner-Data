/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

namespace TD
{
    /// <summary>
    ///  int ----- Identity
    ///  int ----- RecordsNumber
    ///  int ----- columnsNumber
    ///  string * columnsNumber ----- FieldName
    ///  int * columnsNumber ------ FieldType
    /// </summary>
    public class BinaryDataWriter : IDataWriter
    {
        string m_Path;
        //string m_FileName;
        //string m_suffix;
        int m_Identity = 82305;
        public string path
        {
            get { return m_Path; }
            set { m_Path = value; }
        }

        public BinaryDataWriter(String a_path)
        {
            Init(a_path);
        }
        public void Init(String a_path)
        {
           // Debug.Log("write binary path : "+a_path);
            path = a_path;
            //string[] sArr1 = path.Split(new char[] { '/' });
            //string str1 = sArr1[sArr1.Length - 1];
            //string[] sArr2 = str1.Split(new char[] { '.' });
            //m_FileName = sArr2[0];
            //m_suffix = sArr2[1];
        }
        BinaryWriter m_Writer = null;
        private static FileStream m_stream = null;
        public bool InitWriter()
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            if (m_stream == null && m_Writer == null)
            {
                if (File.Exists(path))
                    File.Delete(path);
                m_stream = new FileStream(path, FileMode.Create);
                m_Writer = new BinaryWriter(m_stream);
            }           
            return true;
        }

        bool WriteSchema(Schema a_schema, int a_recordsNumber)
        {
            if (!InitWriter())
            {
                return false;
            }

            //Write int ----- Identity
            m_Writer.Write(m_Identity);

            //Write int ----- RecordsNumber
            m_Writer.Write(a_recordsNumber);

            //Write int ----- columnsNumber
            m_Writer.Write(a_schema.Count);

            //Write string * columnsNumber ----- FieldName
            for (int i = 0; i < a_schema.Count; ++i)
            {
                m_Writer.Write(a_schema.GetDefine(i).FieldName);
            }

            //Write int * columnsNumber ------ FieldType
            for (int i = 0; i < a_schema.Count; ++i)
            {
                m_Writer.Write((int)a_schema.GetDefine(i).FieldType);
            }

            return true;
        }


        bool WriteData(Row[] a_rows, Schema a_schema)
        {
            if (!InitWriter())
            {
                return false;
            }

            foreach (Row row in a_rows)
            {
                for (int i = 0; i < a_schema.Count; ++i)
                {
                    FieldDefine define = a_schema.GetDefine(i);
                    switch (define.FieldType)
                    {
                        case FIELD_TYPE.T_INT:
                            m_Writer.Write((int)row.GetField(i).m_Value);
                            break;
                        case FIELD_TYPE.T_FLOAT:
                            m_Writer.Write((float)row.GetField(i).m_Value);
                            break;
                        case FIELD_TYPE.T_STRING:
                            m_Writer.Write(row.GetField(i).m_Value.ToString());
                            break;
                        case FIELD_TYPE.T_INVALID:
                            //Debug.LogError("Wrong Field Type.");
                            break;
                        default:
                            break;
                    }
                }
            }
            return true;
        }

        public bool Write(Schema schema, Row[] rows)
        {
            bool res = false;
            res = WriteSchema(schema, rows.Length);
            if (!res)
            {
                //Debug.Log("Write Error");
                return false;
            }
            res = WriteData(rows, schema);

            if (res)
            {
                m_stream.Close();
                m_stream = null;
                m_Writer.Close();
                m_Writer = null;
            }
            return res;
        }
    }
}
