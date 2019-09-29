using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    AudioManager audioManager;

    //public Enemy[] enemyPrefabs;
    Wave[] waves;

    public static List<Enemy> enemyList = new List<Enemy>();

    public static ParticleSystem smallExplosion, bigExplosion;


    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        smallExplosion = Resources.Load<ParticleSystem>("ExplosionSmall");
        bigExplosion = Resources.Load<ParticleSystem>("ExplosionBig");
        waves = transform.GetComponentsInChildren<Wave>();
    }
    void Update()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            Wave wave = waves[i];
            if (wave.canSpawn)
            {
                //for (int j = 0; j < enemyPrefabs.Length; j++)
                {
                    // if (enemyPrefabs[j].GetComponent<AudioSyncer>().subClipIndex == audioManager.currentClipIndex)
                    {
                        //  Spawn(wave, enemyPrefabs[j]);
                        Spawn(wave, wave.enemyPrefab);
                        // break;
                    }
                }

                wave.canSpawn = false;
            }
        }
    }

    void Spawn(Wave wave, Enemy enemyPrefab)
    {

        Vector3 spawnPosition = wave.path.StartPosition;
        if (wave.spawnPosition == Wave.SpawnPosition.End)
            spawnPosition = wave.path.EndPosition;

        Enemy enemy = Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
        enemy.spawnPosition = spawnPosition;
        enemy.path = wave.path;
        enemy.moveSpeed = wave.enemyMoveSpeed;

        for (int i = 0; i < enemy.weapons.Length; i++)
        {
            enemy.weapons[i].GetComponent<AudioSyncer>().subClipIndex = audioManager.currentClipIndex;
        }

        enemyList.Add(enemy);


    }
}
