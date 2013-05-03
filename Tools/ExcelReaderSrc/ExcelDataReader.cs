/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using NPOI.HSSF.UserModel;

namespace TD
{
    public class ExcelDataReader : IDataReader
    {
        string m_Path;
        string m_FileName;
        //string m_suffix;
        int StartRow = 3;
        HSSFWorkbook m_Hssfworkbook = null;
        List<Row> mRowList = new List<Row>();
        public string path
        {
            get { return m_Path; }
            set { m_Path = value; }
        }

        Schema m_Schema = null;

        public ExcelDataReader(String a_path)
        {
            Init(a_path);
        }
        public void Init(String a_path)
        {
            Debug.Log("read excel path : " + a_path);
            path = a_path;
            string[] sArr1 = path.Split(new char[] { '/' });
            string str1 = sArr1[sArr1.Length - 1];
            string[] sArr2 = str1.Split(new char[] { '.' });
            m_FileName = sArr2[0];
            //m_suffix = sArr2[1];
            InitHssf();
            LoadSchema();
            LoadData();
        }

        void InitHssf()
        {
            if (m_Hssfworkbook == null)
            {
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    m_Hssfworkbook = new HSSFWorkbook(file);
                }
            }
        }

        //load Schema To Reader.
        void LoadSchema()
        {
            m_Schema = new Schema();
            m_Schema.ClassName = m_FileName;
            HSSFSheet sheet = (HSSFSheet)m_Hssfworkbook.GetSheetAt(0);
            HSSFRow row0 = (HSSFRow)sheet.GetRow(0);
           // HSSFRow row1 = (HSSFRow)sheet.GetRow(1);
            HSSFRow row2 = (HSSFRow)sheet.GetRow(2);


            for (int i = 0; i < row0.LastCellNum; ++i)
            {
                FieldDefine tempDefine = new FieldDefine();

                tempDefine.FieldName = row0.GetCell(i).ToString();
                tempDefine.Index = i;
                string tempTypeStr = row2.GetCell(i).ToString();
                if (tempTypeStr == "INT" || tempTypeStr == "int" || tempTypeStr == "Int")
                {
                    tempDefine.FieldType = FIELD_TYPE.T_INT;
                }
                else if (tempTypeStr == "FLOAT" || tempTypeStr == "float" || tempTypeStr == "Float")
                {
                    tempDefine.FieldType = FIELD_TYPE.T_FLOAT;
                }
                else if (tempTypeStr == "STRING" || tempTypeStr == "string" || tempTypeStr == "String")
                {
                    tempDefine.FieldType = FIELD_TYPE.T_STRING;
                }
                else
                {
                    tempDefine.FieldType = FIELD_TYPE.T_INVALID;
                }

                m_Schema.AddDefine(tempDefine);
            }
        }
        void LoadData()
        {
            if (m_Schema == null)
            {
                return;
            }
            HSSFSheet sheet = (HSSFSheet)m_Hssfworkbook.GetSheetAt(0);

            int CurrentRowIndex = 0;

            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            while (rows.MoveNext())
            {
                HSSFRow row = (HSSFRow)rows.Current;
                if (CurrentRowIndex < StartRow)
                {
                    CurrentRowIndex++;
                    continue;
                }
                Row tempRow = new Row(m_Schema);

                for (int i = 0; i < row.LastCellNum; i++)
                {
                    HSSFCell cell = (HSSFCell)row.GetCell(i);
                    FieldDefine tempDefine = m_Schema.GetDefine(i);
                    Field tempField = null;
                    if (cell != null)
                    {
                        switch (tempDefine.FieldType)
                        {
                            case FIELD_TYPE.T_INT:
                                int tempInt = (int)cell.NumericCellValue;
                                tempField = new Field(tempInt);
                                break;
                            case FIELD_TYPE.T_FLOAT:
                                float tempFloat = (float)cell.NumericCellValue;
                                tempField = new Field(tempFloat);
                                break;
                            case FIELD_TYPE.T_STRING:
                                string tempString = cell.StringCellValue;
                                tempField = new Field(tempString);
                                break;
                        }
                    }
                    else
                    {
                        switch (tempDefine.FieldType)
                        {
                            case FIELD_TYPE.T_INT:
                                int tempInt = 0;
                                tempField = new Field(tempInt);
                                break;
                            case FIELD_TYPE.T_FLOAT:
                                float tempFloat = 0;
                                tempField = new Field(tempFloat);
                                break;
                            case FIELD_TYPE.T_STRING:
                                string tempString = "";
                                tempField = new Field(tempString);
                                break;
                        }
                    }
                    tempRow.m_Fields.Add(tempField);
                }
                mRowList.Add(tempRow);
                CurrentRowIndex++;
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
