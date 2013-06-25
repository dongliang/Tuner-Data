/*  
    Tuner Text Data Base 
    e-mail : dongliang17@126.com
 */
using UnityEditor;
using UnityEngine;
using TunerData;

[CustomEditor(typeof(TTDBMonoTunner))]
public class TTDBTunnerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        TTDBMonoTunner tempTunner = target as TTDBMonoTunner;
        int lineIndex = (int)tempTunner.TDLine.m_Fields[0].m_Value;

        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            GUILayout.Label("------------- Data Tunner -------------");
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        //Table Name
        GUILayout.BeginHorizontal();
        {

            GUILayout.Label("Table：");
            GUILayout.Label(tempTunner.TableName);
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
        //Index ID
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("Index：");
            GUILayout.Label(lineIndex.ToString());
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();

        //Data Field

        for (int i = 1; i < tempTunner.TDLine.m_Fields.Count; i++)
        {
            Table tempTable = TDRoot.Instance.getTable(tempTunner.TableName);
            Field item_field = tempTunner.TDLine.m_Fields[i];
            FIELD_TYPE item_fieldType = tempTable.GetFieldType( i);



            string item_filedName = tempTable.GetFieldName(i);
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

                //Editor
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
             //   tempTunner.SaveFile();
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

