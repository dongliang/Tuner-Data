/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using System;
namespace Tuner.Data
{
    public interface IDataReader
    {
        string path { get; set; }
        void Init(String a_Path);
        Row[] ReadData();
        Schema ReadSchema();
    }
}
