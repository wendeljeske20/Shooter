using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    AudioSyncer audioSyncer;

    [HideInInspector] public Spaceship.Team team;
    public Projectile projectilePrefab;
    public float projectileSpeed = 10f;
    public int projectileDamage = 10;

    [HideInInspector] public bool canShoot;
    public bool shootWithMusic = true;

    float timer;

    private void Start()
    {
        audioSyncer = GetComponent<AudioSyncer>();
    }
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        //nextAttackInterval += Time.deltaTime;

        if (shootWithMusic && audioSyncer.isBeat)
        {
            canShoot = true;
            audioSyncer.isBeat = false;
        }
        else if (!shootWithMusic && timer > audioSyncer.timeStep)
        {
            timer = 0;
            canShoot = true;
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
