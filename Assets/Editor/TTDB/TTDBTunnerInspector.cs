/*
   Tuner Data - Used to read the static data  in game development.
   e-mail : dongliang17@126.com  
*/
using UnityEditor;
using UnityEngine;
using TD;

[CustomEditor(typeof(TDMonoTunner))]
public class TTDBTunnerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        TDMonoTunner tempTunner = target as TDMonoTunner;
        int lineIndex = (int)tempTunner.Line.m_Fields[0].m_Value;

        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUILayout.Label("------------- Data Tunner -------------");
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        //数据表名
        GUILayout.BeginHorizontal();
        {

            GUILayout.Label("Table：");
            GUILayout.Label(tempTunner.TableName);
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        //索引ID
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("Index：");
            GUILayout.Label(lineIndex.ToString());
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();

        //数据字段

        for (int i = 1; i < tempTunner.Line.m_Fields.Count; i++)
        {
            Field item_field = tempTunner.Line.m_Fields[i];
            FIELD_TYPE item_fieldType = Root.Instance.GetFieldType(tempTunner.TableName, i);
            string item_filedName = Root.Instance.GetFiledName(tempTunner.TableName, i);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(item_filedName + "：");

                switch (item_fieldType)
                {
                    case FIELD_TYPE.T_INT:
                        item_field.m_Value = EditorGUILayout.IntField((int)item_field.m_Value);
                        break;
                    case FIELD_TYPE.T_FLOAT:
                        item_field.m_Value = EditorGUILayout.FloatField((float)item_field.m_Value);

                        break;
                    case FIELD_TYPE.T_STRING:
                        item_field.m_Value = EditorGUILayout.TextField((string)item_field.m_Value);
                        break;
                }

                //编辑器
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Label(" ");

        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save File"))
            {
                tempTunner.SaveFile();
            }
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();

        GUILayout.Label(" ");
        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUILayout.Label("------------------ End ------------------");
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();


    }
}

