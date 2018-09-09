using UnityEngine;
using System.Collections;

public struct PathFollowerCompletedLoopEvent
{
    public float distTraveled;
    public GameObject obj;
}

public class ExPathFollower : FFComponent {

    public float distance = 0.0f;
    public float speed = 1.0f;

    private int loopCounter = 0;

    public FFPath PathToFollow;

    void Awake(){}
    void Start(){}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        distance += Time.fixedDeltaTime * speed;
        var path = PathToFollow;
        
        if (path)
        {
            var PrevPos = path.PointAlongPath(distance - 0.01f);
            var position = path.PointAlongPath(distance);
            var vecForward = Vector3.Normalize(position - PrevPos);

            transform.position = position;
            transform.up = vecForward;

            if ((int)(distance / path.PathLength) > loopCounter)
            {
                loopCounter = (int)(distance / path.PathLength);
                PathFollowerCompletedLoopEvent e;
                e.obj = this.gameObject;
                e.distTraveled = path.PathLength;
                FFMessage<PathFollowerCompletedLoopEvent>.SendToLocal(e);
            }
        }
    }

}