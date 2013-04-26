/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using UnityEngine;
using TD;

public class TTDBMonoTunner : MonoBehaviour
{
    Row m_TDLine = null;

    public Row TDLine
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
    public void SetDataSource(string tableName, Row line)
    {
        m_TableName = tableName;
        m_TDLine = line;
    }
}
