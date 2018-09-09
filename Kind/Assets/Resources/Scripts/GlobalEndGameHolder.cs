using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct HelpedYou
{
    public GameObject obj;
}

public class GlobalEndGameHolder : MonoBehaviour {

    

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
       // FFMessage<HelpedYou>.Connect(HelpedMe);
    }


    public void HelpedMe(GameObject obj)
    {
        obj.transform.parent = transform;
    }
}
