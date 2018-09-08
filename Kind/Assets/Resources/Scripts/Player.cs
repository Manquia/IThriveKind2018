using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Use
{
    public GameObject user;
}

class QueryUsable
{
    public bool usableObject = false;
    public string message;
    public GameObject gameObject;
}

public class Player : MonoBehaviour {


    Rigidbody2D rigid;

	// Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update ()
    {
        float dt = Time.deltaTime;

        UpdateMovement(dt);
        UpdateRotation(dt);
	}

    void UpdateRotation(float dt)
    {
        transform.up = Vector3.Lerp(transform.up, lookDir, rotationCoeficient * dt); 
    }

    public float rotationCoeficient = 1.3f;
    public float slowCoeficient = 3.3f;
    public float acceleration = 20.0f;
    public float maxSpeed = 3.5f;

    public Vector3 lookDir = Vector3.up;

    void UpdateMovement(float dt)
    {
        var movementVec = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) movementVec += Vector3.up;
        if (Input.GetKey(KeyCode.S)) movementVec -= Vector3.up;
        if (Input.GetKey(KeyCode.D)) movementVec += Vector3.right;
        if (Input.GetKey(KeyCode.A)) movementVec -= Vector3.right;

        // normalize movement
        if (movementVec != Vector3.zero)
        {
            movementVec = movementVec.normalized;
            lookDir = movementVec;
        }


        if(movementVec == Vector3.zero)
        {
            rigid.velocity = Vector2.Lerp(rigid.velocity, Vector2.zero, slowCoeficient * dt);
        }
        else // want to move
        {
            rigid.velocity += new Vector2(movementVec.x, movementVec.y) * acceleration * dt;
        }

        // limit speed
        if(rigid.velocity.magnitude > maxSpeed)
        {
            rigid.velocity = rigid.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        QueryUsable qu = new QueryUsable();
        FFMessageBoard<QueryUsable>.Send(qu, collision.gameObject);


        if(qu.usableObject)
        {
            SelectUsableObject(qu);
            DisplayUsableObjectMessage(qu);
        }
        else
        {
            DeselectUsableObject(qu);
        }
    }

    public GameObject selectedObject;

    void SelectUsableObject(QueryUsable qu)
    {
        selectedObject = qu.gameObject;
    }

    void DisplayUsableObjectMessage(QueryUsable qu)
    {
        // ADD send to changeTest event
    }

    void DeselectUsableObject(QueryUsable qu)
    {
        if(qu.gameObject == selectedObject)
        {
            selectedObject = null;
        }
    }

    void UseSelectedObject()
    {
        if(selectedObject != null)
        {
            Use u;
            u.user = gameObject;
            FFMessageBoard<Use>.Send(u, selectedObject);
        }
        
    }

}
