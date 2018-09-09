using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedPlayer : MonoBehaviour {


    private void Start()
    {
        FFMessage<PathFollowerCompletedLoopEvent>.Connect(DoThing);
    }

    private void OnDestroy()
    {
        FFMessage<PathFollowerCompletedLoopEvent>.Disconnect(DoThing);
    }

    int DoThing(PathFollowerCompletedLoopEvent p)
    {
        if (p.obj.name.Contains("Player"))
        {
            string level = "Office";
            FFMessage<TriggerFade>.SendToLocal(new TriggerFade(level));
        }
        return 0;
    }
}
