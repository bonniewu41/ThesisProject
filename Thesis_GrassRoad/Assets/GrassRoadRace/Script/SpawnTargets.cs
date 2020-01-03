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

    public int maxTarget = 10;
    public int addedDistance = 4; // This adjusts how far targets should be located from the current character position
    /* ================================================ */


    /* =============== Private variables =============== */
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

        if ((EnterArea.trigger_count % 2) == 1) // case1 : moving with x
        {
            if (characterPosX < (spawnX + addedDistance))
            {
                SpawnTarget_x();
                DeleteTarget();
            }
        }
        else if (EnterArea.trigger_count == 4) 
        {
            if (characterPosZ < (spawnZ - addedDistance))
            {
                SpawnTarget_z();
                DeleteTarget();
            }
        }
        else
        {
            if (characterPosZ > (spawnZ - addedDistance))
            {
                SpawnTarget_z();
                DeleteTarget();
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
            yPos = Random.Range(2, 5);

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
    }


    /* spawns 10 Targets at a time based on x position */
    void SpawnTarget_x()
    {
        List<int> TargetGroup = SelectTarget();

        for (int i = 0; i < maxTarget; i++)
        {
            xPos = (int)(characterPosX - Random.Range(9 + addedDistance, 14 + addedDistance));
            yPos = Random.Range(2, 5);

            if (EnterArea.trigger_count == 3)
            {
                zPos = (178 * 2) + TargetGroup[i];
            }
            else
            {
                zPos = (178 * (EnterArea.trigger_count % 2)) + TargetGroup[i];
            }

            _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));

            activeTargets.Add(_targetClone);
        }
        spawnX -= pathLength;
    }


    void FirstSpawnTarget()
    {
        List<int> firstTargetGroup = SelectTarget();

        for (int i = 0; i < maxTarget; i++)
        {
            xPos = firstTargetGroup[i];
            yPos = Random.Range(2, 5);
            zPos = (int)(characterPosZ + Random.Range(9, 14));
            _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));

            activeTargets.Add(_targetClone);
        }
        spawnZ += pathLength;
    }


    void DeleteTarget()
    {
        for (int i = 9; i >= 0; --i)
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

        int left_num = Random.Range(3, 6);
        int mid_num = Random.Range(1, 3);
        if (left_num == 3)
        {
            mid_num = 2;
        }

        List<int> firstTargetGroup = new List<int>();

        // chooses left section numbers
        for (int i = 0; i < left_num; i++)
        {
            cur_left = Random.Range(-10, -3);
            while (firstTargetGroup.Contains(cur_left))
            {
                cur_left = Random.Range(-10, -3);
            }
            firstTargetGroup.Add(cur_left);
        }

        // chooses middle section numbers
        for (int i = left_num; i < left_num + mid_num; i++)
        {
            cur_middle = Random.Range(-3, 4);

            while (firstTargetGroup.Contains(cur_middle))
            {
                cur_middle = Random.Range(-3, 4);
            }
            firstTargetGroup.Add(cur_middle);
        }

        // chooses right section numbers
        for (int i = left_num + mid_num; i < 10; i++)
        {
            cur_right = Random.Range(4, 11);
            while (firstTargetGroup.Contains(cur_right))
            {
                cur_right = Random.Range(4, 11);
            }
            firstTargetGroup.Add(cur_right);
        }

        return firstTargetGroup;
    }
}


