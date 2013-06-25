/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using System;
namespace TunerData
{
    public interface IDataWriter
    {
        string path { get; set; }
        void Init(String a_Path);
        bool Write(Schema schema,Row[] rows);
    }
}
