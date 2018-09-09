using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct ActivateUsableObject
{
    public GameObject user;
}
public class UsableObject : MonoBehaviour
{
    [System.Serializable]
    public struct AdvancedData
    {
        // uses the FadeTrigger event
        public string LevelToLoadOnUseComplete;
    }
    public AdvancedData advanced;

    static Dictionary<string, bool> BlackBoard = new Dictionary<string, bool>();

    public string[] conditions;
    public PlayerMessage QueryPM;
    public PlayerMessage []inProgressPMs;
    public PlayerMessage UseCompletePM;
    public string[] effects;

    public int moneyCost;
    public float timeToComplete = 0;
    public float timeRemaining = 0;
    public bool oneTimeUse = true;

    int timesUsed = 0;

    // Use this for initialization
    void Start ()
    {
        FFMessageBoard<UseBegin>.Connect(OnUseBegin, gameObject);
        FFMessageBoard<Using>.Connect(OnUsing, gameObject);
        FFMessageBoard<QueryUsable>.Connect(OnQueryUsable, gameObject);
    }

    private void OnDestroy()
    {
        FFMessageBoard<UseBegin>.Disconnect(OnUseBegin, gameObject);
        FFMessageBoard<Using>.Disconnect(OnUsing, gameObject);
        FFMessageBoard<QueryUsable>.Disconnect(OnQueryUsable, gameObject);
    }

    private int OnUsing(Using e)
    {
        if (Usable() == false)
        {
            e.valid = false;
            return 0;
        }


        timeRemaining -= e.dt;
        timeRemaining = Math.Max(timeRemaining, 0.0f);

        float mu = 1.0f - (timeRemaining / timeToComplete);
        float muStep = 1.0f / inProgressPMs.Length;
        //Debug.Log("mu:  " + mu);

        int sampleIndex = (int)((mu + (0.5f * muStep)) * ((float)inProgressPMs.Length - 1.0f));
        sampleIndex = Math.Min(sampleIndex, inProgressPMs.Length - 1);

        var pmToUse = inProgressPMs[sampleIndex];

        e.pm = pmToUse;
        e.timeRemaining = timeRemaining;
        e.timeToCompelte = timeToComplete;
        e.valid = true;
        
        if(e.timeRemaining <= 0.0f)
        {
            UseCompleted uc = new UseCompleted();
            uc.objectUsed = gameObject;
            uc.pm = UseCompletePM;
            FFMessageBoard<UseCompleted>.Send(uc, e.user);

            e.valid = false;
            Complete(e);
        }
        return 0;
    }

    void Complete(Using e)
    {
        Debug.Log("Completed: " + gameObject.name + " Usable Object");

        Static_Var.Money -= moneyCost;
        Static_Var.Money = Mathf.Max(0, Static_Var.Money);

        ActivateUsableObject auo;
        auo.user = e.user;
        FFMessageBoard<ActivateUsableObject>.Send(auo, gameObject);

        foreach(var eff in effects)
        {
            BlackBoard.Add(eff, true);
        }

        ++timesUsed;


        if(advanced.LevelToLoadOnUseComplete != "")
        {
            TriggerFade tf;
            tf.level_name = advanced.LevelToLoadOnUseComplete;
            FFMessage<TriggerFade>.SendToLocal(tf);
        }
    }

    private int OnUseBegin(UseBegin e)
    {
        timeRemaining = timeToComplete;

        return 0;
    }

    bool Usable()
    {
        foreach (var cond in conditions)
        {
            if (!BlackBoard.ContainsKey(cond) || BlackBoard[cond] == false)
                return false;
        }

        if (oneTimeUse && timesUsed > 0)
            return false;
        
        if (Static_Var.Money < moneyCost)
            return false;

        return true;
    }

    private int OnQueryUsable(QueryUsable e)
    {
        e.pm.message = QueryPM.message;
        e.pm.font = QueryPM.font;
        e.usableObject = true;
        e.gameObject = gameObject;
            
        return 0;
    }
}
