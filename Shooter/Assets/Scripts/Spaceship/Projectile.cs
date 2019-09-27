using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    public float moveSpeed;

    public int damage;

    Vector3 spawnPosition;

    public Vector3 targetDirection;

    public void Start()
    {

        spawnPosition = transform.position;
    }

    void Update()
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
