using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct HelpedYou
{
    public GameObject obj;
}

public class GlobalEndGameHolder : MonoBehaviour {


    public List<GameObject> helpedPeople = new List<GameObject>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        FFMessage<HelpedYou>.Connect(HelpedMeF);
    }


    private int HelpedMeF(HelpedYou obj)
    {
        helpedPeople.Add(obj.obj);
        obj.obj.transform.parent = transform;
        return 0;
    }
}
