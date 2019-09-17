using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float moveSpeed;
    public float attackRange = 10;
    public int damage;

    Vector3 spawnPosition;

    public void Start()
    {
        spawnPosition = transform.position;
    }

    void Update()
    {
        Move();

        float distance = (transform.position - spawnPosition).sqrMagnitude;

        if (distance >= attackRange * attackRange)
        {
            Destroy(gameObject);
        }

    }

    protected virtual void Move()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
