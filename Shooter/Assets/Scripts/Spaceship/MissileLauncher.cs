using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : Weapon
{





    public override void Shoot()
    {
        base.Shoot();


        Missile missile = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Missile>();
        missile.team = team;
        missile.moveSpeed = projectileSpeed;
        missile.damage = damage;
    }
}
