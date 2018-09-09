using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer_behavior : FFComponent {

    public delegate void Timer_Event(string timerName);
    public static event Timer_Event OnTimerEnd;

    private const int clockone = 0;
    private const int clocktwo = 1;
    WaitForSeconds oneSecond;

    public int maxTime;
    public int currentTime;

    public Text timertext;
    public Text childtext;
    static string level = "Level";
    public string over_string = "";

    public AudioSource src;

    private void Start()
    {
        seq = action.Sequence();
        currentTime = maxTime;
        childtext.gameObject.SetActive(false);

        //This is the office scene. Get rid of the clock
        if(!SceneManager.GetActiveScene().name.Contains(level))
        {
            this.gameObject.SetActive(false);
        }
        oneSecond = new WaitForSeconds(1f);
        StartCoroutine(updateTimer());

    }

    FFAction.ActionSequence seq;

    public void Blink()
    {
        BlinkOff();
    }
    void BlinkOff()
    {
        seq.Property(ffUIGraphicColor, new Vector4(0, 0, 0, 0), FFEase.E_Continuous, 0.1f);
        seq.Sync();
        seq.Call(BlinkOn);
    }

    void BlinkOn()
    {
        seq.Property(ffUIGraphicColor, new Vector4(1, 0, 0, 1), FFEase.E_Continuous, 0.1f);
        seq.Sync();
        seq.Call(BlinkOff);
    }

    public void StopBlink()
    {
        seq.Pause();
    }

    IEnumerator updateTimer()
    {
        while (currentTime >= 0)
        {
            yield return oneSecond;
            currentTime -= 1;
            timertext.text = floatToString(currentTime);
            src.Play();

            if(currentTime <= 10)
            {
                childtext.gameObject.SetActive(true); 
                if(seq.IsComplete())
                {
                    Blink();
                }
            }
        }
        TimeOver();
    }

    void TimeOver()
    {
        timertext.text = over_string;
        timertext.color = Color.red;
        OnTimerEnd(this.gameObject.name);
        childtext.gameObject.SetActive(false);
    }


    string floatToString(int flt)
    {
        int seconds = flt % 60;
        int minutes = flt / 60;
        return minutes + ":" + seconds;
    }
}
