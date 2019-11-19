using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    public GameObject targetPrefab;
    public GameObject character;
    public int xPos;
    public int yPos;
    public int zPos;

    public int targetCount = 0;
    public int maxTarget = 10;
    public float spawnTime = 0.1f;
    public float repeatTime = 0.6f;

    private float characterPos;
    //private GameObject _t;


    void Start()
    {
        //StartCoroutine(TargetSpawn());
        InvokeRepeating("spawnTarget", spawnTime, repeatTime);
        //for (int i = 0; i < targetCount; i++)
        //{
        //    spawnTarget();
        //}
    }

    void Update()
    {
        characterPos = character.transform.position.z;
        //spawnTarget();
        //Debug.Log(characterPos);
    }

    //IEnumerator TargetSpawn()
    //{
    //    while (targetCount < 5)
    //    {
    //        xPos = GetRandom();
    //        yPos = Random.Range(2, 5);
    //        zPos = (int)(characterPos + Random.Range(10, 18));

    //        // problem right now is that when instantiating a clone, you can delete it but then it continues to spawn a lot of them
    //        _t = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));
    //        //GameObject t = Instantiate(targetPrefab) as GameObject;
    //        //t.transform.position = new Vector3(xPos, yPos, zPos);
    //        //t.transform.rotation = Quaternion.Euler(90, 0, 0);

    //        targetCount += 1;

    //        yield return new WaitForSeconds(0.2f);

    //        if (targetPrefab.transform.position.z < characterPos + 5)
    //        {
    //            Destroy(_t, 4f);
    //            targetCount -= 1;
    //        }
    //    }
    //}

    void spawnTarget()
    {
        xPos = GetRandom();
        yPos = Random.Range(2, 5);
        zPos = (int)(characterPos + Random.Range(10, 18));

        GameObject _targetClone;

        if (targetCount < maxTarget)
        {
            _targetClone = Instantiate(targetPrefab) as GameObject;
            _targetClone.transform.position = new Vector3(xPos, yPos, zPos);
            _targetClone.transform.rotation = Quaternion.Euler(90, 0, 0);
            targetCount++;

            if (targetPrefab.transform.position.z < characterPos + 5)
            {
                Destroy(_targetClone, 5f);
                targetCount --;
            }
        }


        //Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));

        
    }

    private int GetRandom()
    {
        int[] validChoices = { -8, -8, -7, -7, -6, -6, -5, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        return validChoices[Random.Range(0, validChoices.Length)];
    }
}
