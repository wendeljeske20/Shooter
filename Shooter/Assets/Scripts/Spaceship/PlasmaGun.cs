using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : Weapon
{



    public override void Shoot()
    {
        base.Shoot();

        Plasma plasma = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Plasma>();
        plasma.team = team;
        plasma.moveSpeed = projectileSpeed;
        plasma.damage = damage;
    }

   
}
