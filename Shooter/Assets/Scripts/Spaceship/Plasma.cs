using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : Projectile
{
    //public Vector3 targetDirection;

    protected override void Move()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        //Debug.DrawLine(transform.position,  transform.position + targetDirection,Color.red, 1);
    }
}
