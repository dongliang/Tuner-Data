/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using UnityEngine;
using System.Collections;
using Tuner.Data;
public class TestTTBS : MonoBehaviour {

	// Use this for initialization 
    void Start()
    {       
        TDRoot.Instance.Open(Application.dataPath + "/TD/data/test.byte");
        TDRoot.Instance.AddDataTunner("test", 210250);

        TDStruct.test testObj = TDRoot.Instance.getTable("test").GetStruct<TDStruct.test>(210250);
        Debug.Log(testObj.JieShouRenId);

        TDRoot.Instance.GenerateStruct("test", Application.dataPath + "/");
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
