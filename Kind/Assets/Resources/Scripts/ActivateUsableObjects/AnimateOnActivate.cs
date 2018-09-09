using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOnActivate : FFComponent {

    public Animation animator;
    public AnimationClip[][] AnimationSets;


    FFAction.ActionSequence animSeq;
    int animationIndex = 0;
    int SetIndex = 0;

	// Use this for initialization
	void Start ()
    {
        animSeq = action.Sequence();
        FFMessageBoard<ActivateUsableObject>.Connect(OnActivate, gameObject);
    }
    private void OnDestroy()
    {
        FFMessageBoard<ActivateUsableObject>.Disconnect(OnActivate, gameObject);
    }

    private int OnActivate(ActivateUsableObject e)
    {
        RunSet();
        return 0;
    }

    void RunSet()
    {
        animationIndex = 0;

        if (SetIndex < AnimationSets.Length)
            RunAnimation();
    }
    void RunAnimation()
    {
        var set = AnimationSets[SetIndex];
        var anim = set[animationIndex];
        
        animSeq.Call(PlayAnim);
        animSeq.Delay(anim.length + Time.deltaTime);
        animSeq.Sync();

        ++animationIndex;
        if(animationIndex < set.Length)
        {
            animSeq.Call(RunAnimation);
        }
        else
        {
            animationIndex = 0;
            ++SetIndex;
        }
    }


    void PlayAnim()
    {
        animator.Play();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
