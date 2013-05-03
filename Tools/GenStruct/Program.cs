using System;
using System.Collections.Generic;
using System.Text;
using TD;

namespace GenStruct
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

            Console.WriteLine("-----Tuner Data generate model struct(.cs file) : -----");
            Console.WriteLine("");
            Console.WriteLine("1.Path:");
            Console.WriteLine("source path : " + SourcePath);
            Console.WriteLine("target path : " + TargetPath);
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
            Console.WriteLine("2.generate files:");
            foreach (string item in tables)
            {
                TDRoot.Instance.GenerateStruct(item, TargetPath);
                Console.WriteLine("file : " + TargetPath +"TD_"+ item + ".cs");
            }
            Console.Read();
        }
    }
}
