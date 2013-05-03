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
            Console.WriteLine(" ");
            Console.WriteLine("-----Tuner Data Excel to Binary : -----");
            Console.WriteLine("");
            Console.WriteLine("1.Path:");
            Console.WriteLine("source path : "+SourcePath);
            Console.WriteLine("target path : "+TargetPath);
            Console.WriteLine("");
            


            List<string> tables = new List<string>();
            string[] pathArr = Util.GetAllFilePath(SourcePath, "xls", true);
            foreach (string path in pathArr)
            {
                string fileName = Util.GetFileName(path);
                string suffix = Util.GetFileSuffix(path);
                tables.Add(fileName);
                TDRoot.Instance.Open(path);
            }
            Console.WriteLine("2.Export files:");

            foreach (string item in tables)
            {
                TDRoot.Instance.Save(item, TargetPath, E_DataFile_Type.binary);

                Console.WriteLine("file : "+TargetPath + item + ".bytes");
            }

            Console.Read();
            
        }
    }
}
