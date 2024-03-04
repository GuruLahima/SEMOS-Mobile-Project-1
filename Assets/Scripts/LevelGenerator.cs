using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Tooltip("The time between each spawning of an obstacle")]
    public float spawnInterval;
    public float startDelay;
    public float offsetMax;
    public float obstacleSpeed;
    [Tooltip("the x position after which the obstacles are destroyed")]
    public float deathThreshold;
    public GameObject obstaclePrefab;
    public Transform spawnPosition;
    public Transform spawnParent;
    public BirdController bird;

    private float timer;
    private List<GameObject> obstacles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("SpawnObstacle", startDelay, spawnInterval);

        bird.Points = -1;
    }

    void SpawnObstacle()
    {
        Vector3 newSpawnPos = spawnPosition.position + new Vector3(0, Random.Range(-offsetMax, offsetMax), 0);
        GameObject newObstacle = Instantiate(obstaclePrefab, newSpawnPos, Quaternion.identity, spawnParent);
        obstacles.Add(newObstacle);
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
        CleanObstacles();
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

    void CleanObstacles()
    {

        // 
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].transform.position.x < deathThreshold)
            {
                Destroy(obstacles[i]);
                obstacles.RemoveAt(i);
            }
        }
    }
}
