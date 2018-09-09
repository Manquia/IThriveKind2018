using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Static_Var : MonoBehaviour {

    public static int Money
    {
        get
        {
            return money;
        }
        set
        {
            
            MoneyStruct msg;

            if(value > money)
            {
                msg.color = Color.green;
            }
            else
            {
                msg.color = Color.red;
            }

            money = value;
            msg.updatedString = money.ToString();

            FFMessage<MoneyStruct>.SendToLocal(msg);


        }
    }

    public static int currentLevel = 1;

    public static void RefreshUI()
    {
        MoneyStruct msg;
        msg.updatedString = money.ToString();
        msg.color = Color.black;
        FFMessage<MoneyStruct>.SendToLocal(msg);
    }

    private static int money = 200;
}
