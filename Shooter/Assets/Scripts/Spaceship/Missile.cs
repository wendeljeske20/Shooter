using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    //Spaceship[] potencialTargets;
    Spaceship target;
    public float rotationSpeed;

    //public float spinSpeed;

    protected override void Start()
    {
        base.Start();

        if (team == Spaceship.Team.Enemy)
            target = GameObject.Find("Player").GetComponent<Spaceship>();
    }
    protected override void Move()
    {
        Vector3 followDirection = Vector3.forward;// transform.position -target.transform.position;
        followDirection.Normalize();
        transform.Translate(followDirection * moveSpeed * Time.deltaTime);

        //  transform.Rotate(new Vector3(0, 0, spinSpeed) * Time.deltaTime);




        // target = potencialTargets[0];

        if (team == Spaceship.Team.Player)
            FindClosestEnemy();


        if (target)
            LookAtTarget();
        // transform.LookAt(targetTransform.position, Vector3.forward);
    }

    void FindClosestEnemy()
    {
        //potencialTargets = Spawner.enemyList.ToArray();
        float closestDistance = 99999;
        //if (!target)
        {
            for (int i = 0; i < Spawner.enemyList.Count; i++)
            {
                float d = Vector3.Distance(transform.position, Spawner.enemyList[i].transform.position);
                if (d < closestDistance)
                {
                    closestDistance = d;
                    target = Spawner.enemyList[i];
                    //closestDistance = 99999;
                }

            }
        }
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
