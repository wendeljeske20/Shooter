using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spaceship
{

    public Path path;

    public GameObject target;
    public float angle;
    [HideInInspector] public Vector3 spawnPosition;
    public int currentPathIndex = 0;

    int moveDirection = 1;

    public Vector3 exitDirection;

    bool reachedEnd;



    public float pathPosition;

    protected override void Start()
    {
        base.Start();

        if (path)
        {
            exitDirection = (path.positionList[path.positionList.Count - 2] - path.positionList[path.positionList.Count - 1]).normalized;
            pathPosition = path.less;
            if (spawnPosition == path.EndPosition)
            {
                moveDirection = -1;
                Debug.Log("-1");
                //currentPathIndex = path.positionList.Count - 1;
                exitDirection = (path.positionList[1] - path.positionList[0]).normalized;
                pathPosition = path.greater;
            }



        }

        target = GameObject.Find("Player");
    }

    protected override void Update()
    {
        base.Update();
        if (path)
        {
            //if (pathPosition >= path.less && pathPosition <= path.greater)
            float distanceToNextPoint = Vector3.Distance(path.CreateCurve(pathPosition), path.CreateCurve(pathPosition + 1));
            pathPosition += Time.deltaTime * moveSpeed * moveDirection * (1 / distanceToNextPoint);

            FollowPath();


        }

        if (target)
            LookAtPosition(target.transform.position);
        else
            LookAtPosition(Vector3.zero);

        Shoot();

    }

    void LookAtPosition(Vector3 position)
    {
        Quaternion targetRotation = Quaternion.LookRotation(position - transform.position, Vector3.forward);
        transform.rotation = targetRotation;

    }


    void FollowPath()
    {
        Vector3 targetPosition = transform.position - exitDirection;

        //if (currentPathIndex >= 0 && currentPathIndex < path.positionList.Count)
        // targetPosition = path.CreateSimpleCurve(currentTime);// path.positionList[currentPathIndex];

        if (!reachedEnd && pathPosition > path.less && pathPosition < path.greater)
            transform.position = path.CreateCurve(pathPosition);
        else
            reachedEnd = true;


        if (reachedEnd)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);






    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ViewCollider"))
        {
            Destroy(gameObject);
        }

    }


    void OnDestroy()
    {
        Spawner.enemyList.Remove(this);
    }
}
