using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct MoneyStruct
{
    public string updatedString;
    public Color color;
}

public class MoneyUI : MonoBehaviour {
    private Text UI;
    private WaitForSeconds wait = new WaitForSeconds(2.0f);
	// Use this for initialization
	void Start () {
        UI = GetComponent<Text>();
        FFMessage<MoneyStruct>.Connect(ChangeText);
    }
	
	// Update is called once per frame
	void Update () {
        FFMessage<MoneyStruct>.Connect(ChangeText);
    }

    private int ChangeText(MoneyStruct e)
    {
        UI.color = e    .color;
        UI.text = e.updatedString;
        return 0;
    }

    IEnumerator ReturnColorToNormal()
    {
        yield return wait;
        UI.color = Color.black;
    }
}

