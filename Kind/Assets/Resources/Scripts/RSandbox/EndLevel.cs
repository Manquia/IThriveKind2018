using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {


    string World = "World";

    private void OnEnable()
    {
        Timer_behavior.OnTimerEnd += Timer_behavior_OnTimerEnd;
    }

    private void OnDisable()
    {
        Timer_behavior.OnTimerEnd -= Timer_behavior_OnTimerEnd;
    }


    private void Timer_behavior_OnTimerEnd(string timerName)
    {
        if(timerName.Contains(World))
        {
            StartCoroutine(endLevel());
        }
    }

    private void Start()
    {
        
    }

    IEnumerator endLevel()
    {
        yield return null;
        string level = "Level" + Static_Var.currentLevel.ToString();
        FFMessage<TriggerFade>.SendToLocal(new TriggerFade(level));
    }

}
