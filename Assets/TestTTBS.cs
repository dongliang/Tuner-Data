/*
   TTDB
   Tuner Text Data Base use for game static data read.
   e-mail : dongliang17@126.com  
*/
using UnityEngine;
using System.Collections;
using TTDB;
public class TestTTBS : MonoBehaviour {

	// Use this for initialization 
    void Start()
    {
        TDRoot.Instance.Init(Application.dataPath + "/Data/", new string[] { "a" });
        TDRoot.Instance.AddDataTunner("a", 210251);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
