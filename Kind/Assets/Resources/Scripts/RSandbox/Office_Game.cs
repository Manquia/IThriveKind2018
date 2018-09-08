using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Office_Game : MonoBehaviour {
    public int game_end_string_length = 50;
    public Button end_game_button;

    public void OnValueChange(string value)
    {
        if(value.Length > game_end_string_length)
        {
            end_game_button.interactable = true;
        }
    }
}
