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
    private float spawnZ = 0.0f;

    private List<GameObject> activeTargets;
    //private GameObject _t;


    void Start()
    {
        //StartCoroutine(TargetSpawn());
        //Invoke("spawnTarget", spawnTime);
        //InvokeRepeating("spawnTarget", spawnTime, repeatTime);

        activeTargets = new List<GameObject>();

        int[] firstTargetGroup = FirstTargetSelection();

        spawnTarget();
        deleteTarget();

        //for (int i = 0; i < 10; i++)
        //{
        //    xPos = firstTargetGroup[i];
        //    yPos = Random.Range(2, 5);
        //    zPos = (int)(characterPos + Random.Range(9, 14));

        //    _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));
        //    //targetCount++;
        //}
    }

    void Update()
    {
        characterPos = character.transform.position.z;

        
        if (characterPos > (spawnZ - 3))
        {
            
            spawnTarget();
            //deleteTarget();
            //for(int i = 0; i < maxTarget; i++)
            //{
            //    //xPos = (int)GetRandom()[i];
            //    xPos = firstTargetGroup[i];
            //    yPos = Random.Range(2, 5);
            //    zPos = (int)(characterPos + Random.Range(9, 14));
            //    spawnTarget();
            //}
            //spawnZ += pathLength;



            //deleteTarget();
        }
        if (characterPos > spawnZ)
        {
            //deleteTarget();
        }

        //spawnTarget();
        //Debug.Log(characterPos);
    }



    // spawns 10 Targets at a time
    void spawnTarget()
    {
        //int xPos = x;
        //int yPos = y;
        //int zPos = z;
        int[] firstTargetGroup = FirstTargetSelection();
        for (int i = 0; i < maxTarget; i++)
        {
            //xPos = (int)GetRandom()[i];
            xPos = firstTargetGroup[i];
            yPos = Random.Range(2, 5);
            zPos = (int)(characterPos + Random.Range(9, 14));
            _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));

            activeTargets.Add(_targetClone);
            Debug.Log(activeTargets.Count);
        }

        //_targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));
        //targetCount++;


        spawnZ += pathLength;

    }


    void deleteTarget()
    {
        for (int i = 9; i >= 0; --i)
        {
            activeTargets.RemoveAt(i);
            Destroy(activeTargets[i], 3f);
            
            //targetCount--;
        }

        //if (targetPrefab.transform.position.z < characterPos + 5)
        //{
        //    Destroy(_targetClone, 5f);
        //    targetCount--;
        //}
    }


    private int GetRandom()
    {
        int[] validChoices = { -8, -8, -7, -7, -6, -6, -5, -5, -4, -4, -3, -2, -1, 1, 2, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        return validChoices[Random.Range(0, validChoices.Length)];
    }


    private Hashtable computeXPos()
    {

        int cur_left = Random.Range(-10, -3); // left options
        int cur_middle = Random.Range(-3, 4); // middle options
        int cur_right = Random.Range(4, 11); //right options

        int[] allCombinations = { 4, 1, 5, 3, 1, 6, 2, 1, 7, 5, 1, 4, 6, 1, 3, 7, 1, 2, 3, 2, 5, 4, 2, 4, 5, 2, 3 };

        var validChoices = new Hashtable();

        //float[,] myFloats = new float[10, 10];


        return validChoices;
    }


    // creates random selection for the first target group with (L=4, M=2, R=4)
    private int[] FirstTargetSelection()
    {
        int cur_left;
        int cur_middle;
        int cur_right;

        int[] firstTargetGroup = new int[10];

        // 0, 1, 2, 3
        for (int i = 0; i< 4; i++)
        {
            cur_left = Random.Range(-10, -3);
            firstTargetGroup[i] = cur_left;
        }

        // 4, 5
        for (int i = 4; i < 6; i++)
        {
            cur_middle = Random.Range(-3, 4);
            firstTargetGroup[i] = cur_middle;
        }

        // 6, 7, 8, 9
        for (int i = 6; i < 10; i++)
        {
            cur_right = Random.Range(4, 11);
            firstTargetGroup[i] = cur_right;
        }

        return firstTargetGroup;
    }
}




//spawnTarget();

//xPos = (int)GetRandom()[i];


// *trying to compute random selection in the Start method that doesn't repeat 

//random_num = Random.Range(0, firstTargetGroup.Length);

//int random_num;
//int prev_random = -1;

//if (random_num == prev_random)
//{
//    //prev_random = random_num;
//    random_num = Random.Range(0, firstTargetGroup.Length);
//}

//prev_random = random_num;
//int cur_xPos = firstTargetGroup[random_num];
//firstTargetGroup[random_num] = 0;

//if (cur_xPos == xPos)
//{

//    cur_xPos = firstTargetGroup[random_num];
//}
//else
//{
//    xPos = cur_xPos;
//}


//xPos = firstTargetGroup[random_num];









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