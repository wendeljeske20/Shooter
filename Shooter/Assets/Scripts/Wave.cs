using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public enum SpawnPosition
    {
        Start,
        End
    }
    public SpawnPosition spawnPosition;
    //public List<Enemy> enemyList = new List<Enemy>();
    public Path path;
    public float startTime = 0, endTime = 5;
    public float spawnInterval = 2;
    public float enemyMoveSpeed = 5;
    
    float nextSpawnInterval;
    [HideInInspector] public bool canSpawn = true;

    //public Vector3 exitDirection;

    void Start()
    {
        nextSpawnInterval = spawnInterval;
    }

    void Update()
    {
        nextSpawnInterval += Time.deltaTime;

        if (Time.time > startTime && Time.time < endTime)
        {
            if (nextSpawnInterval >= spawnInterval)
            {
                canSpawn = true;
                nextSpawnInterval = 0;
            }
        }

    }


}
