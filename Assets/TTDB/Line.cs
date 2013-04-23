/*
   Tuner Data - Used to read the static data  in game development.
   e-mail : dongliang17@126.com  
*/
using System;
using System.Collections.Generic;
using UnityEngine;
namespace TD
{
    public class Line
    {
        public List<Field> m_Fields = new List<Field>();
        public Field GetData(int field)
        {
            if (field >= 0 && field < m_Fields.Count)
            {
                return m_Fields[field];
            }
            return null;
        }
    }
}