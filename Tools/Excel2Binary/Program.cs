using System;
using System.Collections.Generic;

using System.Text;
using TD;

namespace Excel2Binary
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                return;
            }
            string SourcePath = args[0];
            string TargetPath = args[1];

            List<string> tables = new List<string>();
            string[] pathArr = Util.GetAllFilePath(SourcePath, "xls", true);
            foreach (string path in pathArr)
            {
                string fileName = Util.GetFileName(path);
                string suffix = Util.GetFileSuffix(path);
                tables.Add(fileName);
                TDRoot.Instance.Open(path);
            }

            foreach (string item in tables)
            {
                TDRoot.Instance.Save(item, TargetPath, E_DataFile_Type.binary);
            }

        }
    }
}
