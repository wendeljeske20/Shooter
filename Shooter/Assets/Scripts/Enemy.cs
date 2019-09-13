using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spaceship
{
    
    public Path path;

    public int currentPathIndex;
    private void Start()
    {
        currentPathIndex = path.pointsAmount - 1;

        transform.position = path.positionList[currentPathIndex];
    }

    private void Update()
    {
        if (currentPathIndex >= 0)
            FollowPath();
    }

    void FollowPath()
    {
        Vector3 targetPosition = path.positionList[currentPathIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);


        if ((transform.position - targetPosition).sqrMagnitude < 0.05)
        {
            Debug.Log("teste");
            currentPathIndex--;
        }




    }
   
}
