using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public Transform[] objectsToSpawn;
    public FFPath pointsToUse;
    public int duplicateCount = 2;

	// Use this for initialization
	void Start ()
    {
        if (pointsToUse == null)
            pointsToUse = GetComponent<FFPath>();


        SpawnObjects();
    }

    void SpawnObjects()
    {
        List<Vector3> points = new List<Vector3>();
        foreach(var pt in pointsToUse.points)
        {
            points.Add(pt);
        }

        for (int dIndex = 0; dIndex < duplicateCount; ++dIndex)
        {
            foreach (var o in objectsToSpawn)
            {
                if (o == null) continue;

                var trans = Instantiate(o);
                var ptIndex = Random.Range(0, points.Count - 1);
                var pos = points[ptIndex];

                trans.position = pos;
                trans.SetParent(transform, true);
                trans.localRotation = Quaternion.AngleAxis(Random.Range(0, 360.0f), Vector3.forward);

                // swap remove
                points[ptIndex] = points[points.Count - 1];
                points.RemoveAt(points.Count - 1);

                if (points.Count == 0)
                    break;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
