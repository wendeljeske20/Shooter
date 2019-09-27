using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : Weapon
{



    public override void Shoot()
    {
        base.Shoot();





        Plasma plasma = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Plasma>();
        //plasma.targetDirection = transform.right;
        plasma.moveSpeed = projectileSpeed;
        plasma.damage = damage;
    }

    // Quaternion RotationToTarget(Vector3 direction)
    // {
    //     //Vector3 dir =  direction - transform.position;
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     return Quaternion.AngleAxis(angle, Vector3.forward);
    // }
}
