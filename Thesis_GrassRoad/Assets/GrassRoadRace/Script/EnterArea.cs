using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : MonoBehaviour
{

    public static bool canTurn;
    public static int trigger_count;


    public void OnTriggerEnter(Collider other)
    {

        SpawnTargets.spawnXCount = 0;
        SpawnTargets.spawnZCount = 0;

        trigger_count++;
    }
}
