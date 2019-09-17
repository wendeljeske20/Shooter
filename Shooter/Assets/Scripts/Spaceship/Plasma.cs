using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : Projectile
{
    public Vector3 targetDirection;

    protected override void Move()
    {
        transform.Translate(targetDirection * moveSpeed * Time.deltaTime);
    }
}
