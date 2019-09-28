using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    AudioSyncer audioSyncer;

       public Spaceship.Team team;
    public Projectile projectilePrefab;
    public float projectileSpeed = 10f;
    public float attackRange = 20f;
    // public float attackInterval = 0.3f;
    // float nextAttackInterval;
    //public float fireRate;
    public int damage = 10;

    public bool canShoot;

    private void Start()
    {
        audioSyncer = GetComponent<AudioSyncer>();
    }
    private void Update()
    {

        //nextAttackInterval += Time.deltaTime;

        if (audioSyncer && audioSyncer.isBeat)
        {
            canShoot = true;
            audioSyncer.isBeat = false;
        }

    }

    // public virtual void Shoot(GameObject target)
    // {

    //     canShoot = false;
    //     //nextAttackInterval = 0;
    // }
    public virtual void Shoot()
    {

        canShoot = false;
        //nextAttackInterval = 0;
    }
}
