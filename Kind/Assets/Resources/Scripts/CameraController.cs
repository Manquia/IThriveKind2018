using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public Player player;
    public float followCoeficient = 0.5f;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player == null)
            return;

        float dt = Time.deltaTime;

        transform.position = Vector3.Lerp(new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z), new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            transform.position.z),
            dt * followCoeficient);
		
	}
}
