using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialSpawnTargets : MonoBehaviour
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
    private int maxTarget = 5;
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
    }

    void Update()
    {
        characterPosZ = character.transform.position.z;
        characterPosX = character.transform.position.x;

        if (EnterArea.trigger_count == 1) // case1 : moving with x
        {
            if (characterPosX > spawnX + 10)
            {
                SpawnTarget_x();
                DeleteTarget();
            }
        }
        else // case2: moving with z
        {
            if (characterPosZ > spawnZ)
            {
                SpawnTarget_z();
                DeleteTarget();
            }
        }
    }


    /* spawns 7 Targets at a time based on z position */
    void SpawnTarget_z()
    {
        List<int> TargetGroup = SelectTarget();

        for (int i = 0; i < maxTarget; i++)
        {
            xPos = TargetGroup[i] - (183.9f * (EnterArea.trigger_count / 2));
            yPos = Random.Range(targetSpreadHeight, targetSpreadHeight + 2);
            zPos = characterPosZ + Random.Range(targetSpreadLength, targetSpreadLength + 5);

            _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 0, 0));
            activeTargets.Add(_targetClone);
        }

        spawnZ += pathLength;
    }


    /* spawns 7 Targets at a time based on x position */
    void SpawnTarget_x()
    {
        List<int> TargetGroup = SelectTarget();

        for (int i = 0; i < maxTarget; i++)
        {
            xPos = characterPosX + Random.Range(targetSpreadLength, targetSpreadLength + 5);
            yPos = Random.Range(targetSpreadHeight, targetSpreadHeight + 2);
            zPos = 178.3f + TargetGroup[i];

            _targetClone = Instantiate(targetPrefab, new Vector3(xPos, yPos, zPos), Quaternion.Euler(90, 90, 0));
            activeTargets.Add(_targetClone);
        }

        spawnX += pathLength;
    }


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

        // targets = 7
        int left_num = 2;
        int mid_num = 1;

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
