using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{

    /* =============== Public variables =============== */
    public GameObject targetPrefab;
    public GameObject character;

    public int xPos;
    public int yPos;
    public int zPos;

   
    public int addedDistance = 4; // This adjusts how far targets should be located from the current character position

    public static int spawnZCount = 0;
    public static int spawnXCount = 0;
    /* ================================================ */


    /* =============== Private variables =============== */
    private int maxTarget = 7;
    private GameObject _targetClone;
    private float characterPosZ;
    private float characterPosX;
    private float pathLength = 15.0f;
    private float spawnZ = 0.0f;
    private float spawnX = 0.0f;

    private List<GameObject> activeTargets;
    /* ================================================ */


    void Start()
    {
        activeTargets = new List<GameObject>();

        FirstSpawnTarget();
        DeleteTarget();
    }

    void Update()
    {
        characterPosZ = character.transform.position.z;
        characterPosX = character.transform.position.x;

        if (EnterArea.trigger_count == 1) // case1 : moving with -x
        {
            if (characterPosX < spawnX - 10)
            {
                if (spawnXCount < 11)
                {
                    SpawnTarget_x();
                    DeleteTarget();
                }
                
            }
        }
        else if (EnterArea.trigger_count == 3)
        {
            if (characterPosX < spawnX - 25)
            {
                if (spawnXCount < 11)
                {
                    SpawnTarget_x();
                    DeleteTarget();
                }

            }
        }
        else if (EnterArea.trigger_count == 5)
        {
            if (characterPosX < spawnX - 40)
            {
                if (spawnXCount < 11)
                {
                    SpawnTarget_x();
                    DeleteTarget();
                }

            }
        }
        else if (EnterArea.trigger_count == 4) // case2: moving with -z
        {

            if (characterPosZ < spawnZ + 15)
            {
                if (spawnZCount < 11)
                {
                    SpawnTarget_z();
                    DeleteTarget();
                }
            }
        }
        else
        {
            if (characterPosZ > (spawnZ - addedDistance))
            {
                if (spawnZCount < 11)
                {
                    SpawnTarget_z();
                    DeleteTarget();
                }

            }
        }
    }


    /* spawns 10 Targets at a time based on z position */
    void SpawnTarget_z()
    {
        List<int> firstTargetGroup = SelectTarget();

        for (int i = 0; i < maxTarget; i++)
        {
            xPos = firstTargetGroup[i] - (184 * (EnterArea.trigger_count / 2));
            yPos = Random.Range(5, 7);

            if (EnterArea.trigger_count == 4)
            {
                zPos = (int)(characterPosZ - Random.Range(9 + addedDistance, 14 + addedDistance));
            }
            else
            {
                zPos = (int)(characterPosZ + Random.Range(9 + addedDistance, 14 + addedDistance));
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

        spawnZCount++;
        //Debug.Log("Z: " + spawnZCount);
    }


    /* spawns 10 Targets at a time based on x position */
    void SpawnTarget_x()
    {
        List<int> TargetGroup = SelectTarget();

        for (int i = 0; i < maxTarget; i++)
        {
            xPos = (int)(characterPosX - Random.Range(13, 18));
            yPos = Random.Range(5, 7);

            if (EnterArea.trigger_count == 3)
            {
                zPos = (int)(178.4 * 2 + 5) + TargetGroup[i];
            }
            else
            {
                zPos = (int)(178.4 * (EnterArea.trigger_count % 2)) + TargetGroup[i]; // 178*1 = where they are before
            }

            _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));

            activeTargets.Add(_targetClone);
        }
        spawnX -= pathLength;

        spawnXCount++;
        //Debug.Log("X: " + spawnXCount);
    }


    void FirstSpawnTarget()
    {
        List<int> firstTargetGroup = SelectTarget();

        for (int i = 0; i < maxTarget; i++)
        {
            xPos = firstTargetGroup[i];
            yPos = Random.Range(5, 7);
            zPos = (int)(characterPosZ + Random.Range(9, 14));
            _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));

            activeTargets.Add(_targetClone);
        }
        spawnZ += pathLength;
    }


    void DeleteTarget()
    {
        for (int i = maxTarget - 1; i >= 0; --i)
        {
            Destroy(activeTargets[i], 6.5f);
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
        for (int i = left_num + mid_num; i < 7; i++)
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


