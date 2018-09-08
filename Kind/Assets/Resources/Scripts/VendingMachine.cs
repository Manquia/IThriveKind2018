using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        FFMessageBoard<UseBegin>.Connect(OnUse, gameObject);
        FFMessageBoard<QueryUsable>.Connect(OnQueryUsable, gameObject);

    }
    

    private void OnDestroy()
    {
        FFMessageBoard<UseBegin>.Disconnect(OnUse, gameObject);
        FFMessageBoard<QueryUsable>.Disconnect(OnQueryUsable, gameObject);

    }

    private int OnUse(UseBegin e)
    {

        return 0;
    }

    private int OnQueryUsable(QueryUsable e)
    {

        return 0;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
