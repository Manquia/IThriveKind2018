using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOnActivate : FFComponent {

    public Animation animator;
    public Animation[] animations;


    FFAction.ActionSequence animSeq;
	// Use this for initialization
	void Start () {
        animSeq = action.Sequence();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
