using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOnActivate : MonoBehaviour
{
    public float distance = 0.0f;
    [Range(0.1f, 20.0f)]
    public float speed = 1.0f;

    private int loopCounter = 0;

    public FFPath PathToFollow;
    public bool moving = false;
    
    // Use this for initialization
    void Start()
    {
        FFMessageBoard<ActivateUsableObject>.Connect(OnActivate, gameObject);
    }
    private void OnDestroy()
    {
        FFMessageBoard<ActivateUsableObject>.Disconnect(OnActivate, gameObject);
    }

    private int OnActivate(ActivateUsableObject e)
    {
        moving = true;
        return 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            distance += Time.fixedDeltaTime * speed;

            var path = PathToFollow.GetComponent<FFPath>();
            if (path)
            {
                var position = path.PointAlongPath(distance);
                transform.position = position;
                if ((int)(distance % path.PathLength) > loopCounter)
                {
                    loopCounter = (int)(distance % path.PathLength);
                    PathFollowerCompletedLoopEvent e;
                    e.distTraveled = path.PathLength;
                    FFMessage<PathFollowerCompletedLoopEvent>.SendToLocal(e);
                }
            }
        }
    }
}
