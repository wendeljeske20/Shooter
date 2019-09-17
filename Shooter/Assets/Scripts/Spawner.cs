using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Wave> waveList = new List<Wave>();
    public Player player;
    void Update()
    {
        for (int i = 0; i < waveList.Count; i++)
        {
            Wave wave = waveList[i];
            if (wave.canSpawn)
            {
                Spawn(wave);
                wave.canSpawn = false;
            }
        }
    }

    void Spawn(Wave wave)
    {

        Vector3 spawnPosition = wave.path.StartPosition;
        if (wave.spawnPosition == Wave.SpawnPosition.End)
            spawnPosition = wave.path.EndPosition;

        Enemy enemy = Instantiate(wave.enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.spawnPosition = spawnPosition;
        enemy.path = wave.path;
        enemy.moveSpeed = wave.enemyMoveSpeed;
        enemy.target = player.gameObject;
    }
}
