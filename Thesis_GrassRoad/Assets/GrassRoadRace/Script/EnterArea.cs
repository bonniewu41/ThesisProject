using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : MonoBehaviour
{

    public static bool canTurn;
    public static int trigger_count = 0;


    public bool OnTriggerEnter(Collider other)
    {
        canTurn = true;
        trigger_count++;
        Debug.Log(trigger_count);

        return canTurn;
    }
}
