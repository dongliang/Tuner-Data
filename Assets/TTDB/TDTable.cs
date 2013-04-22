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
    public class TDTable
    {
        public List<TDLine> m_DataLines;
        public Dictionary<string, int> m_FieldNameMap;
        public List<TD_FIELD_TYPE> m_FieldsType;
        public List<string> m_Notes;
        private Dictionary<int, int> m_Index;
        public TDTable(string text)
        {
            OpenFrom_Text(text);
        }

        public int _GetLineFromText(string text, int start, int deadEnd, out string lineBuf)
        {

            int idx = start;
            lineBuf = "";
            while (idx < deadEnd && text[idx] != '\r' && text[idx] != '\n')
            {
                lineBuf += text[idx];
                idx++;
            }
            while (idx < deadEnd && (text[idx] == '\r' || text[idx] == '\n')) idx++;
            return idx;
        }

        public string[] _SplitStringToArray(string source, char separator)
        {
            Char[] field_separator = new Char[] { separator };
            string[] tempStrArray = source.Split(field_separator);
            return tempStrArray;


        }
        public void OpenFrom_Text(string text)
        {

            //create first line 获取第一行 
            int DeadEnd = text.Length;
            string tempLineStr = "";

            int markIdx = 0;
            markIdx = _GetLineFromText(text, markIdx, DeadEnd, out tempLineStr);
            //create field name index 建立字段名索引
            m_FieldNameMap = new Dictionary<string, int>();
            string[] tempStrArray = _SplitStringToArray(tempLineStr, '\t');
            for (int i = 0; i < tempStrArray.Length; i++)
            {
                m_FieldNameMap.Add(tempStrArray[i], i);
            }
            //create notes 生成注解
            if (m_Notes == null)
            {
                m_Notes = new List<string>();
            }
            markIdx = _GetLineFromText(text, markIdx, DeadEnd, out tempLineStr);
            // Debug.Log(tempLineStr);
            tempStrArray = _SplitStringToArray(tempLineStr, '\t');
            for (int i = 0; i < tempStrArray.Length; i++)
            {
                m_Notes.Add(tempStrArray[i]);
                //Debug.Log(tempStrArray[i]);
            }

            //create field type list. 建立类型列表
            m_FieldsType = new List<TD_FIELD_TYPE>();
            markIdx = _GetLineFromText(text, markIdx, DeadEnd, out tempLineStr);
            tempStrArray = _SplitStringToArray(tempLineStr, '\t');
            for (int i = 0; i < tempStrArray.Length; i++)
            {
                if (tempStrArray[i] == "INT" || tempStrArray[i] == "int" || tempStrArray[i] == "Int")
                {
                    m_FieldsType.Add(TD_FIELD_TYPE.T_INT);
                }
                else if (tempStrArray[i] == "FLOAT" || tempStrArray[i] == "float" || tempStrArray[i] == "Float")
                {
                    m_FieldsType.Add(TD_FIELD_TYPE.T_FLOAT);
                }
                else if (tempStrArray[i] == "STRING" || tempStrArray[i] == "string" || tempStrArray[i] == "String")
                {
                    m_FieldsType.Add(TD_FIELD_TYPE.T_STRING);
                }
                else
                {
                    Debug.LogError("text data base field type error _ unknow type:" + tempStrArray[i]);
                }
            }


            //build data. 建立数据

            int RecordsNum = 0;
            int fieldsNum = m_FieldsType.Count;

            if (m_DataLines == null)
            {
                m_DataLines = new List<TDLine>();
            }
            if (m_Index == null)
            {
                m_Index = new Dictionary<int, int>();
            }

            do
            {
                markIdx = _GetLineFromText(text, markIdx, DeadEnd, out tempLineStr);
                if (markIdx >= DeadEnd)
                {
                    break;
                }

                tempStrArray = _SplitStringToArray(tempLineStr, '\t');

                //wrong field number 列数不对
                if (tempStrArray.Length == 0)
                {
                    continue;
                }
                //fill space. 补上空格
                if (tempStrArray.Length < fieldsNum)
                {
                    int subNum = fieldsNum - tempStrArray.Length;
                    for (int i = 0; i < subNum; i++)
                    {
                        tempStrArray[tempStrArray.Length] = "";
                    }
                }


                //create line.创建行
                TDLine tempTDLine = new TDLine();
                for (int i = 0; i < fieldsNum; i++)
                {
                    TDField newField = new TDField(0);
                    switch (m_FieldsType[i])
                    {
                        case TD_FIELD_TYPE.T_INT:
                            if (tempStrArray[i] != "")
                            {
                                newField = new TDField(System.Convert.ToInt32(tempStrArray[i]));
                            }
                            break;
                        case TD_FIELD_TYPE.T_FLOAT:
                            if (tempStrArray[i] != "")
                            {
                                newField = new TDField(System.Convert.ToSingle(tempStrArray[i]));
                            }
                            break;
                        case TD_FIELD_TYPE.T_STRING:
                            newField = new TDField(tempStrArray[i]);
                            break;
                        default:
                            Debug.Log("only 3 type, if see this code is wrong.");
                            break;
                    }
                    tempTDLine.m_Fields.Add(newField);
                }
                m_DataLines.Add(tempTDLine);
                m_Index.Add((int)tempTDLine.m_Fields[0].m_Value, RecordsNum);
                RecordsNum++;
            } while (true);
            /*
            //log full table. 用于Log 整张表的 测试代码             
            foreach (TD_FIELD_TYPE item in m_FieldsType)
            {
                Debug.Log(item);
            }
            foreach (KeyValuePair<string, int> item in m_FieldNameMap)
            {
                Debug.Log(item);
            }             
            foreach (TDLine tempLine in m_DataLines)
            {

                string tempStr = "";
          

                foreach (TDField item in tempLine.m_Fields)
                {
                    tempStr += item.m_Value.ToString();
                    tempStr += " ";
                }
                tempStr += "\n";
                Debug.Log(tempStr);
            }
            */
        }

        public string GetFiledName(int field)
        {
            foreach (KeyValuePair<string, int> pair in m_FieldNameMap)
            {
                if (pair.Value == field)
                {
                    return pair.Key;
                }
            }
            return null;
        }

        public int GetRecordNum()
        {
            return m_DataLines.Count;
        }

        public int GetFieldNum()
        {
            return m_FieldsType.Count;
        }

        public TD_FIELD_TYPE GetFieldType(int field)
        {
            return m_FieldsType[field];
        }

        public TDField GetData(int index, int field)
        {
            TDLine line = GetData(index);
            if (line != null)
            {
                return line.GetData(field);
            }
            return null;
        }

        public TDLine GetData(int index)
        {
            if (m_Index != null)
            {
                int row = 0;
                if (m_Index.TryGetValue(index, out row))
                {
                    return m_DataLines[row];
                }
            }
            return null;
        }
        //start 0. 从0开始
        public TDLine GetDataByRowNum(int row)
        {
            if (row >= 0 && row < GetRecordNum())
            {
                return m_DataLines[row];
            }
            return null;
        }

        public object GetDataByNum(int row, int field)
        {
            TDLine line = GetDataByRowNum(row);
            if (line != null)
            {
                return line.GetData(field);
            }
            return null;
        }

        public TDLine Search_First_Column_Equ(int field, object value)
        {
            foreach (TDLine item_line in m_DataLines)
            {
                switch (m_FieldsType[field])
                {
                    case TD_FIELD_TYPE.T_INT:
                        if ((int)value == (int)item_line.m_Fields[field].m_Value)
                        {
                            return item_line;
                        }
                        break;
                    case TD_FIELD_TYPE.T_FLOAT:
                        if ((float)value == (float)item_line.m_Fields[field].m_Value)
                        {
                            return item_line;
                        }
                        break;
                    case TD_FIELD_TYPE.T_STRING:
                        if ((string)value == (string)item_line.m_Fields[field].m_Value)
                        {
                            return item_line;
                        }
                        break;
                }
            }
            return null;
        }

        public void SetData(int index, int field, object value)
        {
            TDField tempField = GetData(index, field);
            tempField.m_Value = value;
        }

        public string Serialize()
        {
            string tempStr = "";
            //output first line.输出第一行：字段名
            for (int i = 0; i < m_FieldsType.Count; i++)
            {
                tempStr += GetFiledName(i);
                if (i != m_FieldsType.Count - 1)
                {
                    tempStr += '\t';
                }
                else
                {
                    tempStr += "\r\n";
                }
            }
            //output second line: note. 输出第二行:注释
            for (int i = 0; i < m_Notes.Count; i++)
            {
                tempStr += m_Notes[i];
                //Debug.Log(m_Notes[i]);
                if (i != m_Notes.Count - 1)
                {
                    tempStr += '\t';
                }
                else
                {
                    tempStr += "\r\n";
                }
            }
            //output third line: field type. 输出第三行:类型
            for (int i = 0; i < m_FieldsType.Count; i++)
            {
                TD_FIELD_TYPE tempType = GetFieldType(i);
                switch (tempType)
                {
                    case TD_FIELD_TYPE.T_INT:
                        tempStr += "int";
                        break;
                    case TD_FIELD_TYPE.T_FLOAT:
                        tempStr += "float";
                        break;
                    case TD_FIELD_TYPE.T_STRING:
                        tempStr += "string";
                        break;
                }
                if (i != m_FieldsType.Count - 1)
                {
                    tempStr += '\t';
                }
                else
                {
                    tempStr += "\r\n";
                }
            }
            //output data. 输出数据
            foreach (TDLine item_line in m_DataLines)
            {
                for (int i = 0; i < m_FieldsType.Count; i++)
                {
                    // 
                    switch (m_FieldsType[i])
                    {
                        case TD_FIELD_TYPE.T_INT:
                            tempStr += ((int)item_line.m_Fields[i].m_Value).ToString();
                            break;
                        case TD_FIELD_TYPE.T_FLOAT:
                            tempStr += ((float)item_line.m_Fields[i].m_Value).ToString();
                            break;
                        case TD_FIELD_TYPE.T_STRING:
                            tempStr += ((string)item_line.m_Fields[i].m_Value).ToString();
                            break;
                    }
                    if (i != m_FieldsType.Count - 1)
                    {
                        tempStr += '\t';
                    }
                    else
                    {
                        tempStr += "\r\n";
                    }
                }
            }

            return tempStr;
        }
    }
}
