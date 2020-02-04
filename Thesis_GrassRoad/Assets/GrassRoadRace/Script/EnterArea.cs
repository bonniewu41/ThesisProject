using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : MonoBehaviour
{

    public static bool canTurn;
    public static int trigger_count = 0;


    public void OnTriggerEnter(Collider other)
    {
        //canTurn = true;

        SpawnTargets.spawnXCount = 0;
        SpawnTargets.spawnZCount = 0;

        trigger_count++;
    }
}
