/*
   Tuner Data - Used to read the static data  in game development.
   e-mail : dongliang17@126.com  
*/
using UnityEngine;
using TD;

public class TDMonoTunner : MonoBehaviour
{
    Line m_TDLine = null;

    public Line Line
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
    public void SetDataSource(string tableName, Line line)
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
        Root.Instance.Save(m_TableName);
    }
}
