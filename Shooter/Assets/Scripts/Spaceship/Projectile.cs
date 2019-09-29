using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Spaceship.Team team;
    public float moveSpeed;

    public int damage;

    Vector3 spawnPosition;



    protected virtual void Start()
    {

        spawnPosition = transform.position;
    }

    protected virtual void Update()
    {
        Move();



    }

    protected virtual void Move()
    {

    }

    protected void OnTriggerEnter(Collider other)
    {

    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ViewCollider"))
            Destroy(gameObject);
    }
}
