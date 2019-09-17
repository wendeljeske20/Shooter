using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spaceship
{
    public Weapon weapon;
    public Path path;

    public GameObject target;

    [HideInInspector] public Vector3 spawnPosition;
    public int currentPathIndex = 0;

    int moveDirection = 1;

    public Vector3 exitDirection;

    private void Start()
    {

        exitDirection = (path.positionList[path.positionList.Count - 2] - path.positionList[path.positionList.Count - 1]).normalized;

        if (spawnPosition == path.EndPosition)
        {
            moveDirection = -1;
            currentPathIndex = path.positionList.Count - 1;
            exitDirection = (path.positionList[1] - path.positionList[0]).normalized;
        }




    }

    protected void Update()
    {

        FollowPath();

       


    }



    void FollowPath()
    {
        Vector3 targetPosition = transform.position - exitDirection;

        if (currentPathIndex >= 0 && currentPathIndex < path.positionList.Count)
            targetPosition = path.positionList[currentPathIndex];


        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);



        if ((transform.position - targetPosition).sqrMagnitude < 0.05)
        {
            currentPathIndex += moveDirection;
        }




    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ViewCollider"))
        {
            Destroy(gameObject);
        }

    }
}
