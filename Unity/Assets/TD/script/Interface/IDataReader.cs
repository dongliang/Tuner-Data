/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using System;
namespace TD
{
    public interface IDataReader
    {
        string path { get; set; }
        void Init(String a_Path);
        Row[] ReadData();
        Schema ReadSchema();
    }
}
