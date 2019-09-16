using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public GameObject[] pathPrefabs;
    public Camera mainCamera;

    private float safeZone = 18.0f;
    private float spawnZ = 0.0f;
    private float pathLength = 15.0f;
    private int amtPathOnScreen = 7;
    private int lastPrefabIndex = 0;

    private List<GameObject> activePaths;

    // Start is called before the first frame update
    void Start()
    {
        activePaths = new List<GameObject>();
        for (int i = 0; i < amtPathOnScreen; i++)
        {
            if (i < 2)
            {
                SpawnPath(0);
            } else
            {
                SpawnPath();
            }
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

        if (prefabIndex == -1)
        {
            path = Instantiate(pathPrefabs[RandomPrefabIndex()]) as GameObject;
        } else
        {
            path = Instantiate(pathPrefabs[prefabIndex]) as GameObject;
        }
        
        path.transform.SetParent(transform);
        path.transform.position = Vector3.forward * spawnZ;
        spawnZ += pathLength;
        activePaths.Add(path);
    }


    private void DeletePath()
    {
        Destroy(activePaths[0]);
        activePaths.RemoveAt(0);
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
}
