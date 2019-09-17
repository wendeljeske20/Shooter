using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : Weapon
{





    public override void Shoot(GameObject target)
    {
        base.Shoot(target);


        Missile missile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Missile>();
        missile.target = target;
        missile.moveSpeed = projectileSpeed;
        missile.attackRange = attackRange;
        missile.damage = damage;
    }
}
