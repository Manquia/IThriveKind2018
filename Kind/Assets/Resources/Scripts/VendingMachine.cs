﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour {
    
    public PlayerMessage QueryPM;
    public PlayerMessage UsingPM;
    public PlayerMessage UseCompletePM;

    public int moneyCost;
    public float timeToComplete = 0;
    public float timeRemaining = 0;

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
        timeRemaining -= e.dt;

        e.pm = UsingPM;
        e.timeRemaining = timeRemaining;
        e.timeToCompelte = timeToComplete;

        if(e.timeRemaining < 0)
        {
            UseCompleted uc = new UseCompleted();
            uc.objectUsed = gameObject;
            uc.pm = UseCompletePM;
            FFMessageBoard<UseCompleted>.Send(uc, e.user);

            Complete();
        }
        return 0;
    }

    void Complete()
    {
        Static_Var.Money -= moneyCost;
        Debug.Log("Money: " + Static_Var.Money);
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

    // Update is called once per frame
    void Update () {
		
	}
}