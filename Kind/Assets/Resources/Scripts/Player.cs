using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerMessage
{
    public Font font;
    public string message;
}

// doesn't have a message becuase it would be immediatly replaced by using's message
class UseBegin
{
    public GameObject user;
}
class Using
{
    public GameObject user;
    public float timeToCompelte;
    public float timeRemaining;
    public float dt;
    public PlayerMessage pm;
}
class UseCompleted
{
    public GameObject objectUsed;
    public PlayerMessage pm;
}

class QueryUsable
{
    public bool usableObject = false;
    public GameObject gameObject;
    public PlayerMessage pm;
}

public class Player : MonoBehaviour {


    public GameObject selectedObject;
    Rigidbody2D rigid;

	// Use this for initialization
	void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();

        FFMessageBoard<UseCompleted>.Connect(OnUseCompleted, gameObject);
    }
    private void OnDestroy()
    {
        FFMessageBoard<UseCompleted>.Disconnect(OnUseCompleted, gameObject);
    }
    

    // Update is called once per frame
    void Update ()
    {
        float dt = Time.deltaTime;

        UpdateMovement(dt);
        UpdateRotation(dt);
        UpdateUse(dt);
	}

    public Font PlayerUseFont;

    private void UpdateUse(float dt)
    {
        if(selectedObject != null)
        {
            //Debug.Log("Have Selected Object");
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("UseBegin");

                UseBegin u = new UseBegin();
                u.user = gameObject;
                FFMessageBoard<UseBegin>.Send(u, selectedObject);
            }
            else if(Input.GetKey(KeyCode.E))
            {
                Debug.Log("Using");

                Using u = new Using();
                u.user = gameObject;
                u.dt = dt;
                FFMessageBoard<Using>.Send(u, selectedObject);

                UpdatePlayerMessage(u.pm, 1.0f - (u.timeRemaining / u.timeToCompelte));
            }
            
        }
        else
        {

            //Debug.Log("Don't Have selected Object");
        }
    }

    void UpdatePlayerMessage(PlayerMessage pm, float progress = -1.0f)
    {
        UI_Text ut;
        ut.font = pm.font;
        ut.text = pm.message;
        ut.progress = progress;

        FFMessage<UI_Text>.SendToLocal(ut);
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
        Debug.Log("OnTrigger Entered 2d");
        QueryUsable qu = new QueryUsable();
        FFMessageBoard<QueryUsable>.Send(qu, collision.gameObject);


        if(qu.usableObject)
        {
            SelectUsableObject(qu);
            DisplayUsableObjectMessage(qu);
        }
        else
        {
            DeselectUsableObject(qu.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DeselectUsableObject(collision.gameObject);
    }


    private int OnUseCompleted(UseCompleted e)
    {
        Debug.Log("OnUseCompleted");
        DeselectUsableObject(e.objectUsed);

        UpdatePlayerMessage(e.pm);
        return 0;
    }

    void SelectUsableObject(QueryUsable qu)
    {
        selectedObject = qu.gameObject;
    }

    void DisplayUsableObjectMessage(QueryUsable qu)
    {
        UpdatePlayerMessage(qu.pm);
    }

    void DeselectUsableObject(GameObject go)
    {
        if(go.gameObject == selectedObject)
        {
            selectedObject = null;
        }
    }

    void UseSelectedObject()
    {
        if(selectedObject != null)
        {
            UseBegin u = new UseBegin();
            u.user = gameObject;
            FFMessageBoard<UseBegin>.Send(u, selectedObject);
        }
    }

}
