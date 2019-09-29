using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : Weapon
{
    public float missileRotationSpeed = 100;
    public override void Shoot()
    {
        base.Shoot();


        Missile missile = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Missile>();
        missile.team = team;
        missile.moveSpeed = projectileSpeed;
        missile.damage = projectileDamage;
        missile.rotationSpeed = missileRotationSpeed;
    }
}
