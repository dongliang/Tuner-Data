/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TD
{
    class TDFactory : Singleton<TDFactory>
    {


        public IDataReader GetDataReader(string a_path)
        {
            string path;
            string fileName;
            string suffix;
            path = a_path;
            string[] sArr1 = path.Split(new char[] { '\\' });
            string str1 = sArr1[sArr1.Length - 1];
            string[] sArr2 = str1.Split(new char[] { '.' });
            fileName = sArr2[0];
            suffix = sArr2[1];

            IDataReader tempReader = null;

            switch (suffix)
            {
                case "xls":
                    tempReader = new ExcelDataReader(path);
                    break;
                default:
                    return null;
            }
            return tempReader;
        }
    }
}
