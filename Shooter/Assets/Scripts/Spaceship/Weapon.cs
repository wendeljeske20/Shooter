using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Projectile projectilePrefab;
    public float projectileSpeed = 10f;
    public float attackRange = 20f;
    public float attackInterval = 0.3f;
    float nextAttackInterval;
    //public float fireRate;
    public int damage = 10;

    public bool canShoot;

    private void Update()
    {
        nextAttackInterval += Time.deltaTime;

        if (nextAttackInterval >= attackInterval)
        {
            canShoot = true;
        }

    }

    public virtual void Shoot(GameObject target)
    {

        canShoot = false;
        nextAttackInterval = 0;
    }
    public virtual void Shoot(Vector3 direction)
    {

        canShoot = false;
        nextAttackInterval = 0;
    }
}
