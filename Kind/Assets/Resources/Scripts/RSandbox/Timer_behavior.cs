using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer_behavior : MonoBehaviour {
    public float waitTime = 2.0f;
    public float maxTime = 15.0f;

    private float displacementInterval;

    WaitForSeconds twoSeconds;
    public Image img;

    static string level = "level";

    private void Start()
    {
        displacementInterval = waitTime / maxTime;
        twoSeconds = new WaitForSeconds(waitTime);
        StartCoroutine(updateTimer());

        if(!SceneManager.GetActiveScene().name.Contains(level))
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator updateTimer()
    {
        while (img.fillAmount >= 0)
        {
            yield return twoSeconds;
            img.fillAmount -= displacementInterval;
        }
    }
}
