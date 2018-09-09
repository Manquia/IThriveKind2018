using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_Level_Game_System : MonoBehaviour {
    public Font font;
    public int Daily_Income = 100;
	// Use this for initialization
	void Start () {
        Static_Var.currentLevel = (Static_Var.currentLevel + 1) % 3;
        StartCoroutine(minusMoney());
    }

    IEnumerator minusMoney()
    {
        yield return new WaitForEndOfFrame();
        Static_Var.RefreshUI();
        yield return new WaitForSeconds(3f);
        Static_Var.Money -= Daily_Income;
        UI_Text tx = new UI_Text { text = "-100 FROM INCOME", font = font, progress = 0f };
        FFMessage<UI_Text>.SendToLocal(tx);
    }        
}
