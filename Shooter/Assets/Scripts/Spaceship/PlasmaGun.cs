using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : Weapon
{



    public override void Shoot(Vector3 direction)
    {
        base.Shoot(direction);





        Plasma plasma = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Plasma>();
        plasma.targetDirection = direction;
        plasma.moveSpeed = projectileSpeed;
        plasma.attackRange = attackRange;
        plasma.damage = damage;
    }

    // Quaternion RotationToTarget(Vector3 direction)
    // {
    //     //Vector3 dir =  direction - transform.position;
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     return Quaternion.AngleAxis(angle, Vector3.forward);
    // }
}
