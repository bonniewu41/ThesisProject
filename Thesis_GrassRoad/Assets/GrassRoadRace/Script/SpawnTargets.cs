using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{

    /* =============== Public variables =============== */
    public GameObject targetPrefab;
    public GameObject character;
    /* ================================================ */


    /* =============== Private variables =============== */
    private GameObject _targetClone;
    private float characterPosZ;
    private float characterPosX;
    private float xPos;
    private float yPos;
    private float zPos;

    private int targetSpreadLength = 13; // 13 to 18
    private int targetSpreadHeight = 5; // 5 to 7
    private int maxTarget = 7;
    private float pathLength = 15.0f;
    private float spawnZ = 0.0f;
    private float spawnX = 0.0f;
    public static int spawnZCount = 0;
    public static int spawnXCount = 0;

    private List<GameObject> activeTargets;
    /* ================================================ */


    void Start()
    {
        activeTargets = new List<GameObject>();

        //FirstSpawnTarget();
        //DeleteTarget();
    }

    void Update()
    {
        characterPosZ = character.transform.position.z;
        characterPosX = character.transform.position.x;

        if (EnterArea.trigger_count == 1 || EnterArea.trigger_count == 3 || EnterArea.trigger_count == 5) // case1 : moving with -x
        {
            if (characterPosX < spawnX - 15)
            {
                //if (spawnXCount < 11)
                //{
                    SpawnTarget_x();
                    DeleteTarget();
                //}
            }
        }
        else if (EnterArea.trigger_count == 4) // case2: moving with -z
        {
            if (characterPosZ < spawnZ - 25)
            {
                //if (spawnZCount < 11)
                //{
                    SpawnTarget_z();
                    DeleteTarget();
                //}
                
            }
        }
        else if (EnterArea.trigger_count == 6) // case3: last path
        {
            if (characterPosZ > spawnZ - 5)
            {
                //if (spawnZCount < 11)
                //{
                SpawnTarget_z();
                DeleteTarget();
                //}

            }
        }
        else // case3: moving with z
        {
            if (characterPosZ > spawnZ) // case4: moving normal
            {
                //if (spawnZCount < 11)
                //{
                    SpawnTarget_z();
                    DeleteTarget();
                //}
            }
        }
    }


    /* spawns 10 Targets at a time based on z position */
    void SpawnTarget_z()
    {
        List<int>  TargetGroup = SelectTarget();
         
        for (int i = 0; i < maxTarget; i++)
        {
            xPos = TargetGroup[i] - (183.9f * (EnterArea.trigger_count / 2));
            yPos = Random.Range(targetSpreadHeight, targetSpreadHeight + 2);

            if (EnterArea.trigger_count == 4)
            {
                zPos = characterPosZ - Random.Range(targetSpreadLength, targetSpreadLength + 5);
            }
            else
            {
                zPos = characterPosZ + Random.Range(targetSpreadLength, targetSpreadLength + 5);
            }

            _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));

            activeTargets.Add(_targetClone);
        }

        if (EnterArea.trigger_count == 4)
        {
            spawnZ -= pathLength;
        }
        else
        {
            spawnZ += pathLength;
        }

        //if (spawnZCount != 11)
        //{
        //    spawnZCount++;
        //}
        //else
        //{
        //    spawnZCount = 0;
        //}
    }


    /* spawns 10 Targets at a time based on x position */
    void SpawnTarget_x()
    {
        List<int> TargetGroup = SelectTarget();

        for (int i = 0; i < maxTarget; i++)
        {
            xPos = characterPosX - Random.Range(targetSpreadLength, targetSpreadLength + 5);
            yPos = Random.Range(targetSpreadHeight, targetSpreadHeight + 2);

            if (EnterArea.trigger_count == 3)
            {
                zPos = 362.3f + TargetGroup[i];
            }
            else
            {
                zPos = 178.3f * (EnterArea.trigger_count % 2) + TargetGroup[i]; // 178*1 = where they are before
            }

            _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 90, 0));

            activeTargets.Add(_targetClone);
        }
        spawnX -= pathLength;

        //if (spawnXCount != 11)
        //{
        //    spawnXCount++;
        //}
        //else
        //{
        //    spawnXCount = 0;
        //}
    }


    //void FirstSpawnTarget()
    //{
    //    List<int> firstTargetGroup = SelectTarget();

    //    for (int i = 0; i < maxTarget; i++)
    //    {
    //        xPos = firstTargetGroup[i];
    //        yPos = Random.Range(5, 7);
    //        zPos = (int)(characterPosZ + Random.Range(9, 14));
    //        _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));

    //        activeTargets.Add(_targetClone);
    //    }
    //    spawnZ += pathLength;
    //}


    void DeleteTarget()
    {
        for (int i = maxTarget - 1; i >= 0; --i)
        {
            Destroy(activeTargets[i], 7f);
            activeTargets.RemoveAt(i);
        }
    }


    /* creates random selection for the patterns of targets appearing
       415, 514, 325, 424, 523 */
    private List<int> SelectTarget()
    {
        int cur_left;
        int cur_middle;
        int cur_right;

        // targets = 10
        //int left_num = Random.Range(3, 6);
        //int mid_num = Random.Range(1, 3);
        //if (left_num == 3)
        //{
        //    mid_num = 2;
        //}

        // targets = 7
        int left_num = Random.Range(2, 5);
        int mid_num = Random.Range(1, 3);
        if (left_num == 4)
        {
            mid_num = 1;
        }

        List<int> firstTargetGroup = new List<int>();

        // chooses left section numbers
        for (int i = 0; i < left_num; i++)
        {
            // -10, -3
            cur_left = Random.Range(-7, -2);
            while (firstTargetGroup.Contains(cur_left))
            {
                cur_left = Random.Range(-7, -2);
            }
            firstTargetGroup.Add(cur_left);
        }

        // chooses middle section numbers
        for (int i = left_num; i < left_num + mid_num; i++)
        {
            // -3, 4
            cur_middle = Random.Range(-2, 3);

            while (firstTargetGroup.Contains(cur_middle))
            {
                cur_middle = Random.Range(-2, 3);
            }
            firstTargetGroup.Add(cur_middle);
        }

        // chooses right section numbers
        for (int i = left_num + mid_num; i < maxTarget; i++)
        {
            // 4, 11
            cur_right = Random.Range(3, 8);
            while (firstTargetGroup.Contains(cur_right))
            {
                cur_right = Random.Range(3, 8);
            }
            firstTargetGroup.Add(cur_right);
        }

        return firstTargetGroup;
    }
}


