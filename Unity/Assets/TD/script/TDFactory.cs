/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace TD
{
    public enum E_DataFile_Type
    {
        xls,
        binary
    }
    class TDFactory : Singleton<TDFactory>
    {
        public IDataReader GetDataReader(string a_path)
        {
            string path;
            //string fileName;
            string suffix;
            path = a_path;
            string[] sArr1 = path.Split(new char[] { '\\' });
            string str1 = sArr1[sArr1.Length - 1];
            string[] sArr2 = str1.Split(new char[] { '.' });
            //fileName = sArr2[0];
            suffix = sArr2[1];
            IDataReader tempReader = null;
            switch (suffix)
            {

#if USE_EXCEL
                case "xls":
                    tempReader = new ExcelDataReader(path);
                    break;          
#endif
                case "byte":
                    tempReader = new BinaryDataReader(path);
                    break;
                default:
                    return null;
            }
            return tempReader;
        }

        public string GetFileSuffix(E_DataFile_Type type)
        {
            switch (type)
            {
                case E_DataFile_Type.xls:
                    return "xls";
                case E_DataFile_Type.binary:
                    return "byte";
                default:
                    return "byte";
            }
        }

        public IDataWriter GetDataWriter(string a_path)
        {
            string path;
            //string fileName;
            string suffix;
            path = a_path;
            string[] sArr1 = path.Split(new char[] { '\\' });
            string str1 = sArr1[sArr1.Length - 1];
            string[] sArr2 = str1.Split(new char[] { '.' });
            //fileName = sArr2[0];
            suffix = sArr2[1];
            IDataWriter tempWriter = null;
            switch (suffix)
            {
                case "byte":
                    tempWriter = new BinaryDataWriter(path);
                    break;
                default:
                    return null;
            }
            return tempWriter;
        }
    }
}
