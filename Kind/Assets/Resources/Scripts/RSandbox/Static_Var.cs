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
            money = value;
            MoneyStruct msg;
            msg.updatedString = money.ToString();
            FFMessage<MoneyStruct>.SendToLocal(msg);
        }
    }

    public static void RefreshUI()
    {
        MoneyStruct msg;
        msg.updatedString = money.ToString();
        FFMessage<MoneyStruct>.SendToLocal(msg);
    }

    private static int money = 100;
}
