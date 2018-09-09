using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : MonoBehaviour
{

    public PlayerMessage QueryPM;
    public PlayerMessage[] UsingPM;
    public PlayerMessage UseCompletePM;

    public float timeToComplete = 0;
    public float timeRemaining = 0;

    // Use this for initialization
    void Start()
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
        timeRemaining -= e.dt;
        timeRemaining = Mathf.Max(timeRemaining, 0.0f);

        float mu = 1.0f - (timeRemaining / timeToComplete);
        float muStep = 1.0f / UsingPM.Length;
        //Debug.Log("mu:  " + mu);

        int sampleIndex = (int)((mu + (0.5f * muStep)) * ((float)UsingPM.Length - 1.0f));
        sampleIndex = Mathf.Min(sampleIndex, UsingPM.Length - 1);

        var pmToUse = UsingPM[sampleIndex];

        e.pm = pmToUse;
        e.timeRemaining = timeRemaining;
        e.timeToCompelte = timeToComplete;
        e.valid = true;

        if (e.timeRemaining <= 0.0f)
        {
            UseCompleted uc = new UseCompleted();
            uc.objectUsed = gameObject;
            uc.pm = UseCompletePM;
            FFMessageBoard<UseCompleted>.Send(uc, e.user);

            e.valid = false;
            Complete();
        }
        return 0;
    }

    void Complete()
    {
        FFMessage<TriggerFade>.SendToLocal(new TriggerFade());
    }

    private int OnUseBegin(UseBegin e)
    {
        timeRemaining = timeToComplete;
        return 0;
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
