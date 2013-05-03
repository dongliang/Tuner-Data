/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TD
{
    public class BinaryDataReader : IDataReader
    {
        string m_Path;
        string m_FileName;
        //string m_suffix;
        int m_recordsNumber = 0;
        int m_Identity = 82305;

        BinaryReader m_Reader = null;
        private static FileStream m_stream = null;
        List<Row> mRowList = new List<Row>();
        public string path
        {
            get { return m_Path; }
            set { m_Path = value; }
        }

        Schema m_Schema = null;

        public BinaryDataReader(String a_path)
        {
            Init(a_path);
        }
        public void Init(String a_path)
        {
            //Debug.Log("read binary path : "+a_path);
            path = a_path;
            string[] sArr1 = path.Split(new char[] { '/' });
            string str1 = sArr1[sArr1.Length - 1];
            string[] sArr2 = str1.Split(new char[] { '.' });
            m_FileName = sArr2[0];
            //m_suffix = sArr2[1];
            bool readerInited = InitReader();
            if (!readerInited)
            {
                return;
            }
            LoadSchema();
            LoadData();
            closeFile();
        }

        void closeFile()
        {
            if (m_stream != null)
            {
                m_stream.Close();
                
            }

            if (m_Reader != null)
            {
                m_Reader.Close();
            }
        }

        public bool InitReader()
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            if (m_stream == null && m_Reader == null)
            {
                if (!File.Exists(path))
                    return false;
                m_stream = new FileStream(path, FileMode.Open);
                m_Reader = new BinaryReader(m_stream);
            }
            return true;
        }

        void LoadSchema()
        {
            if (m_Schema == null)
            {
                m_Schema = new Schema();
                m_Schema.ClassName = m_FileName;


                int identity = m_Reader.ReadInt32();
                if (m_Identity != identity)
                {
                    return;
                }
                //read record number
                m_recordsNumber = m_Reader.ReadInt32();

                //read column number
                int columnNumber = m_Reader.ReadInt32();

                //create field define and fill field name.
                for (int i = 0; i < columnNumber; ++i)
                {
                    FieldDefine define = new FieldDefine();
                    define.FieldName = m_Reader.ReadString();
                    define.Index = i;
                    m_Schema.AddDefine(define);
                }

                //read field type.
                for (int i = 0; i < columnNumber; ++i)
                {
                    FieldDefine define = m_Schema.GetDefine(i);
                    define.FieldType = (FIELD_TYPE)m_Reader.ReadInt32();
                }
            }
        }

        //load Data To Reader.
        void LoadData()
        {
            if (m_Schema == null)
            {
                return;
            }
            for (int i = 0; i < m_recordsNumber; ++i)
            {
                Row tempRow = new Row(m_Schema);
                for (int j = 0; j < m_Schema.Count; ++j)
                {
                    FieldDefine tempDefine = m_Schema.GetDefine(j);
                    Field tempField = null;
                    switch (tempDefine.FieldType)
                    {
                        case FIELD_TYPE.T_INT:
                            int tempInt = m_Reader.ReadInt32();
                            tempField = new Field(tempInt);
                            break;
                        case FIELD_TYPE.T_FLOAT:
                            float tempFloat = m_Reader.ReadSingle();
                            tempField = new Field(tempFloat);
                            break;
                        case FIELD_TYPE.T_STRING:
                            string tempString = m_Reader.ReadString();
                            tempField = new Field(tempString);
                            break;
                        default:
                            tempField = new Field(0);
                            //Debug.Log("Undefined Field Type");
                            break;
                    }
                    tempRow.m_Fields.Add(tempField);
                }
                mRowList.Add(tempRow);
            }
        }

        public Schema ReadSchema()
        {
            return m_Schema;
        }

        public Row[] ReadData()
        {

            if (mRowList.Count > 0)
            {
                return mRowList.ToArray();
            }
            else
            {
                return null;
            }
        }
    }
}
