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
    private int maxTarget = 2;
    private float pathLength = 18.0f;
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
            if (characterPosX > spawnX + 5)
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
        spawnZCount++;
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
        spawnXCount++;
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
        int cur_num;
        List<int> TargetSpread = new List<int>();

        for (int i = 0; i < maxTarget; i++)
        {
            if (EnterArea.trigger_count == 0 && spawnZCount % 2 == 0 || EnterArea.trigger_count == 1 && spawnXCount % 2 == 0 )
            {
                cur_num = Random.Range(3, 6); // (inclusive, exclusive)
                while (TargetSpread.Contains(cur_num))
                {
                    cur_num = Random.Range(3, 6);
                }
                
            } else 
            {
                cur_num = Random.Range(-5, -2);  // (inclusive, exclusive)
                while (TargetSpread.Contains(cur_num))
                {
                    cur_num = Random.Range(-5, -2);
                }
            } 

            TargetSpread.Add(cur_num);
        }

        return TargetSpread;
    }
}
