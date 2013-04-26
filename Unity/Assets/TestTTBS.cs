/*
   Tuner Data -  Read Static Data in Game Development.
   e-mail : dongliang17@126.com
*/
using UnityEngine;
using System.Collections;
using TD;
public class TestTTBS : MonoBehaviour {

	// Use this for initialization 
    void Start()
    {
        TDRoot.Instance.Open(Application.dataPath + "/TD/data/test.xls");
        TDRoot.Instance.AddDataTunner("test", 210250);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
