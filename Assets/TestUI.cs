using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour {

    public LitEngine.ScriptInterface.UIInterface uiinter;
	// Use this for initialization
	void Start () {
        uiinter.SetActive(true);
        Time.timeScale = 0.1f;
    }

    public void HideTest()
    {
        uiinter.SetActive(false);
    }

    public void showTest()
    {
        uiinter.SetActive(true);
    }

    public void Customtest(string _state)
    {
        uiinter.PlayAnimation(_state);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
