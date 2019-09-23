using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public GameObject[] pathPrefabs;
    public GameObject[] hurdlePrefabs;
    public Camera mainCamera;

    private float safeZone = 18.0f;
    private float spawnZ = 0.0f;
    private float pathLength = 15.0f;
    private int amtPathOnScreen = 7;
    private int lastPrefabIndex = 0;
    private int lastHurdleIndex = 0;

    private List<GameObject> activePaths;
    private List<GameObject> activeHurdles;

    // Start is called before the first frame update
    void Start()
    {
        activePaths = new List<GameObject>();
        activeHurdles = new List<GameObject>();
        for (int i = 0; i < amtPathOnScreen; i++)
        {
            SpawnPath();
            //if (i < 2)
            //{
            //    SpawnPath(0);
            //} else
            //{
            //    SpawnPath();
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.transform.position.z - safeZone > (spawnZ - amtPathOnScreen * pathLength))
        {
            SpawnPath();
            DeletePath();
        }
    }


    private void SpawnPath(int prefabIndex = -1)
    {
        GameObject path;
        GameObject hurdle;

        if (prefabIndex == -1)
        {
            path = Instantiate(pathPrefabs[RandomPrefabIndex()]) as GameObject;
            hurdle = Instantiate(hurdlePrefabs[RandomHurdle()]) as GameObject;
        } else
        {
            path = Instantiate(pathPrefabs[prefabIndex]) as GameObject;
            hurdle = Instantiate(hurdlePrefabs[prefabIndex]) as GameObject;
        }
        
        path.transform.SetParent(transform);
        path.transform.position = Vector3.forward * spawnZ;

        hurdle.transform.SetParent(transform);
        hurdle.transform.position = Vector3.forward * spawnZ;

        spawnZ += pathLength;

        activePaths.Add(path);
        activeHurdles.Add(hurdle);
    }


    private void DeletePath()
    {
        Destroy(activePaths[0]);
        Destroy(activeHurdles[0]);

        activePaths.RemoveAt(0);
        activeHurdles.RemoveAt(0);
    }


    private int RandomPrefabIndex()
    {
        if (pathPrefabs.Length <= 1)
        {
            return 0;
        }

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, pathPrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    private int RandomHurdle()
    {
        int randomHurdleIndex = lastHurdleIndex;
        while (randomHurdleIndex == lastHurdleIndex)
        {
            randomHurdleIndex = Random.Range(0, hurdlePrefabs.Length);
        }

        lastHurdleIndex = randomHurdleIndex;
        return randomHurdleIndex;
    }
}
