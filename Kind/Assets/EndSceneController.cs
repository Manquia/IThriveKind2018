using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneController : MonoBehaviour {
    public string[] text;
    public float[] waitTime;
    public Font font;

    private WaitForSeconds[] wait;

    GameObject obj;
    GlobalEndGameHolder helpedpeople;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < waitTime.Length; i++)
        {
            wait[i] = new WaitForSeconds(waitTime[i]);
        }
        obj = GameObject.Find("GlobalEndGameHolder");
        helpedpeople = GetComponent<GlobalEndGameHolder>();
    }
	
    IEnumerator DisplayAndWait(string t, float f)
    {
        FFMessage<UI_Text>.SendToLocal(new UI_Text(t, font, 0));
        yield return new WaitForSeconds(f);
    }

    IEnumerator endingScene()
    {
        yield return new WaitForSeconds(3f);

        yield return DisplayAndWait("This is the end of your life", 3);

        yield return DisplayAndWait("What have you done in your life ", 2);
        yield return DisplayAndWait("Determines who you are.", 4);

        yield return DisplayAndWait("Hopefully it is a life well lived...",3);

        for(int i = 0; i < helpedpeople.helpedPeople.Count; i++)
        {
            yield return ComeOutAndTalkAndFade(helpedpeople.helpedPeople[i]);
        }
    }

    IEnumerator ComeOutAndTalkAndFade(GameObject obj)
    {

        yield return null;
    }
}
