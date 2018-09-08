using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaretPosFix : MonoBehaviour {

    private void Start()
    {
        StartCoroutine(waitForFix());
    }

    IEnumerator waitForFix()
    {
        yield return new WaitForSeconds(0.5f);
        Fix();
    }

    // Use this for initialization
    public void Fix () {
        this.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.59f);
	}

    public void OnValueChange(string value)
    {
        this.GetComponent<RectTransform>().pivot = new Vector2(0.5f, .5f);
    }

}
