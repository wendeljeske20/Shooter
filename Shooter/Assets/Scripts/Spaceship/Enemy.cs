using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spaceship
{

    public Path path;

    public GameObject target;

    [HideInInspector] public Vector3 spawnPosition;
    public int currentPathIndex = 0;

    int moveDirection = 1;

    public Vector3 exitDirection;

    AudioSyncer audioSyncer;

    public float pathPosition;

    private void Start()
    {
        audioSyncer = GetComponent<AudioSyncer>();
        if (path)
        {
            exitDirection = (path.positionList[path.positionList.Count - 2] - path.positionList[path.positionList.Count - 1]).normalized;

            if (spawnPosition == path.EndPosition)
            {
                moveDirection = -1;
                //currentPathIndex = path.positionList.Count - 1;
                exitDirection = (path.positionList[1] - path.positionList[0]).normalized;
            }


            pathPosition = path.less;
        }

        target = GameObject.Find("Player");
    }

    protected void Update()
    {
        if (path)
        {
            if (pathPosition >= path.less)// && pathPosition <= path.greater)
                pathPosition += Time.deltaTime * moveSpeed * moveDirection;

            FollowPath();


        }

        if (audioSyncer.m_isBeat)
        {
            Shoot((target.transform.position - transform.position).normalized);
            audioSyncer.m_isBeat = false;
        }
    }



    void FollowPath()
    {
        Vector3 targetPosition = transform.position - exitDirection;

        //if (currentPathIndex >= 0 && currentPathIndex < path.positionList.Count)
        // targetPosition = path.CreateSimpleCurve(currentTime);// path.positionList[currentPathIndex];

        if (pathPosition >= path.less && pathPosition <= path.greater)
            transform.position = path.CreateCurve(pathPosition);
        //else
        //Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);


        if ((transform.position - targetPosition).sqrMagnitude < 0.05)
        {
            //currentPathIndex += moveDirection;
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
