using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    Spaceship[] potencialTargets;
    Spaceship target;
    public float rotationSpeed;


    protected override void Move()
    {
        Vector3 followDirection = Vector3.forward;// transform.position -target.transform.position;
        followDirection.Normalize();
        transform.Translate(followDirection * moveSpeed * Time.deltaTime);

        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);


        potencialTargets = Spawner.enemyList.ToArray();

        // target = potencialTargets[0];

        float closestDistance = 99999;
        //if (!target)
        {
            for (int i = 0; i < potencialTargets.Length; i++)
            {
                float d = Vector3.Distance(transform.position, potencialTargets[i].transform.position);
                if (d < closestDistance)
                {
                    closestDistance = d;
                    target = potencialTargets[i];
                    //closestDistance = 99999;
                }

            }
        }


        if (target)
            LookAtTarget();
        // transform.LookAt(targetTransform.position, Vector3.forward);
    }

    void LookAtTarget()
    {
        //transform.LookAt(target.transform.position);
        //angle = Vector3.SignedAngle(targetTransform.position - transform.position, Vector3.right, Vector3.forward);
        // eulers = transform.eulerAngles;
        //eulers.x = -angle;

        // transform.rotation = Quaternion.LookRotation(angle, Vector3.right);
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


    }

}
