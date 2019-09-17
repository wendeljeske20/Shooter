using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    public GameObject target;

    protected override void Move()
    {
        Vector3 followDirection = transform.position -target.transform.position;
        followDirection.Normalize();
        transform.Translate(followDirection * moveSpeed * Time.deltaTime);
    }
}
