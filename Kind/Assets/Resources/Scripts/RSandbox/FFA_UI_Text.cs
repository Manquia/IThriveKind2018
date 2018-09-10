using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct UI_Text
{
    public string text;
    public Font font;

    //0 is nothing, 1 is full, negative don't display 
    public float progress;

    public UI_Text(string t, Font f, float p)
    {
        text = t;
        font = f;
        progress = p;
    }
}

public class FFA_UI_Text : MonoBehaviour {
    public Text diaglouge;
    public Slider slider;

    private void Awake()
    {
        FFMessage<UI_Text>.Connect(ChangeText);
    }

    private void OnDestroy()
    {
        FFMessage<UI_Text>.Disconnect(ChangeText);    
    }

    private int ChangeText(UI_Text e)
    {
        diaglouge.text = e.text;
        diaglouge.font = e.font;
        slider.value = e.progress;
        return 0;
    }
}
