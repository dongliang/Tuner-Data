Tuner Data
==========
Read Static Data in unity3d Game Development.

Features：

  1. Excel Import : Support import xls( Excel 2003 ) data .
  2. Binary Format: Support Export Binary(byte). And read it at Runtime.
  3. Generate Model Class: Support Generate Model Class. and get the data object easily.

Example:
  
      1.open a data file.
      
       TDRoot.Instance.Open(Application.dataPath + "/TD/data/test.byte");
       
      2.get the model object.
      
       TDStruct.test testObj = TDRoot.Instance.getTable("test").GetStruct<TDStruct.test>(210250);
      
      3.Generate Model Class ( .cs file)
      
       TDRoot.Instance.GenerateStruct("test", Application.dataPath + "/");
