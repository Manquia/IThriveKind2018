using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Office_Game : MonoBehaviour {
    public int game_end_string_length = 50;
    public Button end_game_button;

    public GameObject screen_transition_object;
    public AudioSource audio_player;
    [Space(5)]
    public Font office_Font;
    public string ui_instruction;
    private IEnumerator enumerator;

    private WaitForSeconds wait = new WaitForSeconds(0.5f);


    public void OnValueChange(string value)
    {
        //Handle player sound
        if(enumerator != null)
        {
            StopAllCoroutines();
        }
        StartCoroutine(PlayerKeyDown());

        //Check for button interactable condition
        if(value.Length > game_end_string_length)
        {
            end_game_button.interactable = true;
        }
    }

    public void OnButtonPressed()
    {
        FFMessage<TriggerFade>.SendToLocal(new TriggerFade());
    }

    public void PlaySound()
    {
        if (!audio_player.isPlaying)
        {
            audio_player.Play();
        }
    }

    public void StopSound()
    {
        audio_player.Pause();
    }

    IEnumerator PlayerKeyDown()
    {
        enumerator = PlayerKeyDown();
        PlaySound();
        yield return wait;
        StopSound();
        enumerator = null;
    }

    private void Start()
    {
        UI_Text tx;
        tx.text = ui_instruction;
        tx.font = office_Font;
        FFMessage<UI_Text>.SendToLocal(tx);
    }
}
