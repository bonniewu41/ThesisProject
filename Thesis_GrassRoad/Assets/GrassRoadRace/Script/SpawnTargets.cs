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
    public float current_length;

    private GameObject _targetClone;
    private float characterPos;
    private float pathLength = 15.0f;
    private float spawnZ = 1.0f;

    private List<GameObject> activeTargets;
    //private GameObject _t;


    void Start()
    {
        //StartCoroutine(TargetSpawn());
        //Invoke("spawnTarget", spawnTime);
        //InvokeRepeating("spawnTarget", spawnTime, repeatTime);
        activeTargets = new List<GameObject>();
        for (int i = 0; i < maxTarget; i++)
        {
            spawnTarget();
        }
    }

    void Update()
    {
        characterPos = character.transform.position.z;

        if (characterPos > (spawnZ - 7))
        {
            for(int i = 0; i < maxTarget; i++)
            {
                spawnTarget();
            }

            deleteTarget();
        }

        //spawnTarget();
        //Debug.Log(characterPos);
    }


    //IEnumerator TargetSpawn()
    //{
    //    while (targetCount < maxTarget)
    //    {
    //        xPos = GetRandom();
    //        yPos = Random.Range(2, 5);
    //        zPos = (int)(characterPos + Random.Range(10, 18));

    //        // problem right now is that when instantiating a clone, you can delete it but then it continues to spawn a lot of them
    //        for (int i = 0; i < maxTarget; i++)
    //        {
    //            _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));
    //            targetCount++;
    //        }
    //        //GameObject t = Instantiate(targetPrefab) as GameObject;
    //        //t.transform.position = new Vector3(xPos, yPos, zPos);
    //        //t.transform.rotation = Quaternion.Euler(90, 0, 0);



    //        activeTargets.Add(_targetClone);
    //        spawnZ += pathLength - 1;

    //        yield return new WaitForSeconds(0.2f);

    //        //if (targetPrefab.transform.position.z < characterPos + 5)
    //        //{
    //        //    Destroy(_t, 4f);
    //        //    targetCount -= 1;
    //        //}

    //        if (characterPos > spawnZ - 7)
    //        {
    //            for (int i = 0; i < 10; i++)
    //            {
    //                Destroy(activeTargets[i], 5f);
    //                activeTargets.RemoveAt(i);
    //                targetCount--;
    //            }
    //        }
    //    }
    //}


    void spawnTarget()
    {
        xPos = GetRandom();
        yPos = Random.Range(2, 5);
        zPos = (int)(characterPos + Random.Range(10, 18));

        _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));
        targetCount++;

        activeTargets.Add(_targetClone);

        // *problem right now is that spawnZ is calculated even before actually spawning the targets (meaning before moving, spawnZ is already calculated
        // in the start method, therefore it never reaches the case when characterpos is greater than spawnZ)
        spawnZ += pathLength;
        Debug.Log(spawnZ);
    }


    void deleteTarget()
    {
        //if (characterPos > spawnZ - 7)
        //{
            for (int i = 0; i < 10; i++)
            {
                Destroy(activeTargets[i], 5f);
                activeTargets.RemoveAt(i);
                targetCount--;
            }
        //}

        //if (targetPrefab.transform.position.z < characterPos + 5)
        //{
        //    Destroy(_targetClone, 5f);
        //    targetCount--;
        //}
    }


    private int GetRandom()
    {
        int[] validChoices = { -8, -8, -7, -7, -6, -6, -5, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        return validChoices[Random.Range(0, validChoices.Length)];
    }
}
