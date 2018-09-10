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

    public float Ox;
    public float Oy;
    public float length;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < waitTime.Length; i++)
        {
            wait[i] = new WaitForSeconds(waitTime[i]);
        }
        obj = GameObject.Find("GlobalEndGameHolder");
        helpedpeople = GetComponent<GlobalEndGameHolder>();
        StartCoroutine(endingScene());
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

        yield return DisplayAndWait("What have you done in your life,", 2);
        yield return DisplayAndWait("determines who you are.", 4);

        yield return DisplayAndWait("Hopefully it is a life well lived...",3);

        for(int i = 0; i < helpedpeople.helpedPeople.Count; i++)
        {
            yield return ComeOutAndTalkAndFade(helpedpeople.helpedPeople[i]);
        }
    }

    IEnumerator ComeOutAndTalkAndFade(GameObject obj)
    {
        for(int i = (int)Ox; i < length; i++)
        {
            for(int t = (int)Oy; t < length; t++)
            {
                obj.SetActive(true);
                obj.transform.position = new Vector3(i, t, 0);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
