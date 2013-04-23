using UnityEngine;
using System.Collections;
using TD;
public class TestTTBS : MonoBehaviour {

	// Use this for initialization 
    void Start()
    {
        Root.Instance.Init(Application.dataPath + "/Data/", new string[] { "a" });
        Root.Instance.AddDataTunner("a", 210251);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
