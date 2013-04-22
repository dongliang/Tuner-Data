/*
   TTDB
   Tuner Text Data Base use for game static data read.
   e-mail : dongliang17@126.com  
*/
using UnityEngine;
using TTDB;

public class TTDBMonoTunner : MonoBehaviour
{
    TDLine m_TDLine = null;

    public TDLine TDLine
    {
        get { return m_TDLine; }
        set { m_TDLine = value; }
    }
    string m_TableName = null;

    public string TableName
    {
        get { return m_TableName; }
        set { m_TableName = value; }
    }
    public void SetDataSource(string tableName, TDLine line)
    {
        m_TableName = tableName;
        m_TDLine = line;
    }
    public void SaveFile()
    {
        if (m_TableName == null)
        {
            return;
        }
        TDRoot.Instance.Save(m_TableName);
    }
}
