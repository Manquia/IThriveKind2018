using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnActivate : FFComponent {


    public float fadeTime = 1.7f;

    FFAction.ActionSequence fadeSeq;
	// Use this for initialization
	void Start ()
    {
        fadeSeq = action.Sequence();
        FFMessageBoard<ActivateUsableObject>.Connect(OnActivate, gameObject);
	}
    private void OnDestroy()
    {
        FFMessageBoard<ActivateUsableObject>.Disconnect(OnActivate, gameObject);
    }

    private int OnActivate(ActivateUsableObject e)
    {
        Fade();
        return 0;
    }

    void Fade()
    {
        var sprite = GetComponent<SpriteRenderer>();
        var spriteColorRef = new FFRef<Color>(() => sprite.color, (v) => { sprite.color = v; });
        fadeSeq.Property(spriteColorRef, spriteColorRef.Val.MakeClear(), FFEase.E_SmoothStart, fadeTime);
        fadeSeq.Sync();
        fadeSeq.Call(DestroyMe);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
