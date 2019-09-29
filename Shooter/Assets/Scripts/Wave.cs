using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    AudioSource audioSource;
    public enum SpawnPosition
    {
        Start,
        End
    }
    public SpawnPosition spawnPosition;
    public Enemy enemyPrefab;
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
        audioSource = GameObject.FindObjectOfType<AudioSource>();
        nextSpawnInterval = spawnInterval;
    }

    void Update()
    {
        nextSpawnInterval += Time.deltaTime;
        float time = audioSource.time;

        if (time > startTime && time < endTime)
        {

            if (nextSpawnInterval >= spawnInterval)
            {
                canSpawn = true;
                nextSpawnInterval = 0;
            }
        }

    }


}
