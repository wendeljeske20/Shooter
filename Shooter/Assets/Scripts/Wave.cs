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
    public float startTime, endTime;
    public float spawnInterval;
    public float enemyMoveSpeed;
    public Enemy enemyPrefab;
    float nextSpawnInterval;
    [HideInInspector] public bool canSpawn;

    public Vector3 exitDirection;


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
