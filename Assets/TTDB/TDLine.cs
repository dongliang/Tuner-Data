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
    public class TDLine
    {
        public List<TDField> m_Fields = new List<TDField>();
        public TDField GetData(int field)
        {
            if (field >= 0 && field < m_Fields.Count)
            {
                return m_Fields[field];
            }
            return null;
        }
    }
}