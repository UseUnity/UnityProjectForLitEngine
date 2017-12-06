using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testmono1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual protected void OnDestroy()
    {
        Debug.Log("testmono1");
    }
}
