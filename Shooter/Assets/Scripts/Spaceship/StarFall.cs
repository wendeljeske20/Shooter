using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFall : Enemy
{
    protected new void Update()
    {
        base.Update();
        
        if (weapon.canShoot)
            weapon.Shoot((target.transform.position - transform.position).normalized);
    }
}
