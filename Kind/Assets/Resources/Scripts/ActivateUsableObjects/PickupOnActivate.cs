using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupOnActivate : FFComponent {
 
    public float pickupTime = 1.0f;

    FFAction.ActionSequence pickupSeq;
    // Use this for initialization
    void Start()
    {
        pickupSeq = action.Sequence();
        FFMessageBoard<ActivateUsableObject>.Connect(OnActivate, gameObject);
    }
    private void OnDestroy()
    {
        FFMessageBoard<ActivateUsableObject>.Disconnect(OnActivate, gameObject);
    }

    private int OnActivate(ActivateUsableObject e)
    {
        Pickup(e.user.transform.position);
        return 0;
    }

    void Pickup(Vector3 posToFlyTo)
    {
        var sprite = GetComponent<SpriteRenderer>();

        pickupSeq.Property(ffposition, posToFlyTo, FFEase.E_Continuous, pickupTime);
        pickupSeq.Property(ffscale, Vector3.zero, FFEase.E_Continuous, pickupTime);
        pickupSeq.Sync();
        pickupSeq.Call(DestroyMe);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

