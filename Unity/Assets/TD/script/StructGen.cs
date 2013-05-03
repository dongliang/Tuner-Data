using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TD
{
    public class StructGen : Singleton<StructGen>
    {
        /// <summary>
        /// Generate the .cs file. 
        /// </summary>
        /// <param name="schema">schema</param>
        /// <param name="path">folder path</param>
        public void Generate(Schema schema, string path)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("using TD;\r\n");
            builder.Append("namespace TDStruct\r\n");
            builder.Append("{\r\n");
            builder.Append("public class " + schema.ClassName + " : ITDStruct\r\n");
            builder.Append("\t{\r\n");

            for (int i = 0; i < schema.Count; i++)
            {
                FieldDefine fd = schema.GetDefine(i);
                string fd_type_str = "int";
                switch (fd.FieldType)
                {
                    case FIELD_TYPE.T_FLOAT:
                        fd_type_str = "float";
                        break;
                    case FIELD_TYPE.T_STRING:
                        fd_type_str = "string";
                        break;
                }
                builder.Append("\t\tpublic " + fd_type_str + " " + fd.FieldName + ";\r\n");
            }

            builder.Append("\t\tpublic void Init(Row row)\r\n");
            builder.Append("\t\t{\r\n");

            for (int i = 0; i < schema.Count; i++)
            {
                FieldDefine fd = schema.GetDefine(i);
                string fd_get_value_str = ".GetInt()";
                switch (fd.FieldType)
                {
                    case FIELD_TYPE.T_FLOAT:
                        fd_get_value_str = ".GetFloat()";
                        break;
                    case FIELD_TYPE.T_STRING:
                        fd_get_value_str = ".GetString()";
                        break;
                }
                builder.Append("\t\t\t" + fd.FieldName + " = row.GetField(" + fd.Index.ToString() + ")" + fd_get_value_str + ";\r\n");
            }

            builder.Append("\t\t}\r\n");
            builder.Append("\t}\r\n");
            builder.Append("}\r\n");

            //output file
            File.WriteAllText(path + "TD_" + schema.ClassName + ".cs", builder.ToString(), Encoding.UTF8);
        }
    }
}
