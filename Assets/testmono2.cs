using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
public class testmono2 : Testmono1
{

	// Use this for initialization
	void Start () {
        string hostname = "pregate1.369qipai.net";
        //pregate1.369qipai.net
        IPAddress[] tips = Dns.GetHostAddresses(hostname);
        DLog.Log( "HostName: " + hostname + " Length:" + tips.Length);
        for (int i = 0; i < tips.Length; i++)
        {
            Debug.Log(tips[i].ToString());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    override protected void OnDestroy()
    {
        Debug.Log("testmono2");
        base.OnDestroy();
    }
}
