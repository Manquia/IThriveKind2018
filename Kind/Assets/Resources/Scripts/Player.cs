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
        UpdateMovement();
	}


    public float slowCoeficient = 0.3f;
    public float movementSpeed = 6.0f;

    void UpdateMovement()
    {
        float dt = Time.deltaTime;
        var movementVec = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) movementVec += Vector3.up;
        if (Input.GetKey(KeyCode.S)) movementVec -= Vector3.up;
        if (Input.GetKey(KeyCode.D)) movementVec += Vector3.right;
        if (Input.GetKey(KeyCode.A)) movementVec -= Vector3.right;

        // normalize movement
        if (movementVec != Vector3.zero)
            movementVec = movementVec.normalized;


        if(movementVec == Vector3.zero)
        {
            rigid.velocity = Vector2.Lerp(rigid.velocity, Vector2.zero, slowCoeficient);
        }
        else // want to move
        {
            rigid.AddForce(movementVec * movementSpeed * dt, ForceMode2D.Impulse);
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
