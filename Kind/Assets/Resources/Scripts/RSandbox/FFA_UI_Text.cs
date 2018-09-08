using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct UI_Text
{
    public string text;
}
public class FFA_UI_Text : MonoBehaviour {
    public Text UI_text;

    private void Start()
    {
        FFMessage<UI_Text>.Connect(ChangeText);
    }

    private void OnDestroy()
    {
        FFMessage<UI_Text>.Disconnect(ChangeText);    
    }

    private int ChangeText(UI_Text e)
    {
        UI_text.text = e.text;
        return 0;
    }
}
