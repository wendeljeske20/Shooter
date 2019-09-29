using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    //Spaceship[] potencialTargets;
    Spaceship target;
    [HideInInspector] public float rotationSpeed;

    //public float spinSpeed;

    protected override void Start()
    {
        base.Start();

        if (team == Spaceship.Team.Enemy)
            target = GameObject.FindObjectOfType<Player>();
    }

    protected override void Update()
    {

        Move();
        rotationSpeed -= 3 * Time.deltaTime;

        if (team == Spaceship.Team.Player)
            FindClosestEnemy();


        if (target)
            LookAtTarget();
    }
    protected override void Move()
    {
        Vector3 followDirection = Vector3.forward;
        followDirection.Normalize();
        transform.Translate(followDirection * moveSpeed * Time.deltaTime);

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
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


    }

}
