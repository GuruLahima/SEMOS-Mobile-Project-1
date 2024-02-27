using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float spawnInterval;
    public float startDelay;
    public float offsetMax;
    public float obstacleSpeed;
    public GameObject obstaclePrefab;
    public Transform spawnPosition;
    public Transform spawnParent;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnObstacle", startDelay, spawnInterval);
    }

    void SpawnObstacle()
    {
        Vector3 newSpawnPos = spawnPosition.position + new Vector3(0, Random.Range(-offsetMax, offsetMax), 0);
        Instantiate(obstaclePrefab, newSpawnPos, Quaternion.identity, spawnParent);
    }

    void SpawnObstacles()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = spawnInterval;
            SpawnObstacle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnObstacles();
        MoveObstacle();
    }

    void MoveObstacle()
    {
        foreach (Transform child in spawnParent)
        {
            if (child == spawnParent)
            {
                continue;
            }

            // move the child to the left every frame
            child.Translate(Vector3.left * obstacleSpeed * Time.deltaTime);
        }
    }
}
