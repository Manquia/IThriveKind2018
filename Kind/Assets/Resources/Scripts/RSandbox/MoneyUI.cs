using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct MoneyStruct
{
    public string updatedString;
}

public class MoneyUI : MonoBehaviour {
    public Text UI;
	// Use this for initialization
	void Start () {
        FFMessage<MoneyStruct>.Connect(ChangeText);
    }
	
	// Update is called once per frame
	void Update () {
        FFMessage<MoneyStruct>.Connect(ChangeText);
    }

    private int ChangeText(MoneyStruct e)
    {
        UI.text = e.updatedString;
        return 0;
    }
}

