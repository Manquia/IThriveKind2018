using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicahSandbox : FFComponent {

    public AnimationCurve curve;

    FFAction.ActionSequence seq;
	// Use this for initialization
	void Start ()
    {
        seq = action.Sequence();


        seq.Property(ffposition, Vector3.zero, FFEase.E_Continuous, 1.0f);
        seq.Property(ffposition, Vector3.zero, FFEase.E_Continuous, 1.0f);
        seq.Delay(2.0f);
        seq.Sync();
        seq.Property(ffrotation, Quaternion.identity, curve, 1.0f);
        seq.Delay(0.5f);
        seq.Sync();
        seq.Property(ffscale, Vector3.one * 2.0f, FFEase.E_Continuous, 1.0f);
        seq.Sync();
        seq.Call(DestroyMe);


    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
