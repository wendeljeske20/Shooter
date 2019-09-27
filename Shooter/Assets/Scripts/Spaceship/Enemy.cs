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

    protected void Update()
    {
        if (path)
        {
            //if (pathPosition >= path.less && pathPosition <= path.greater)
            float distanceToNextPoint = Vector3.Distance(path.CreateCurve(pathPosition), path.CreateCurve(pathPosition + 1));
            pathPosition += Time.deltaTime * moveSpeed * moveDirection * (1 / distanceToNextPoint);

            FollowPath();


        }
        // transform.LookAt(target.transform.position, transform.forward);

        // Vector3 lookPosition = target.transform.position - transform.position;
        // lookPosition.z = -1;
        // transform.rotation = Quaternion.LookRotation(lookPosition);

        LookAtTarget();
        //transform.LookAt(target.transform.position);

        //if (audioSyncer.isBeat)
        {
            //(target.transform.position - transform.position).normalized
            Shoot();
            // audioSyncer.isBeat = false;
        }
    }

    void LookAtTarget()
    {
        //transform.LookAt(target.transform.position);
        angle = Vector3.SignedAngle(transform.position - target.transform.position, Vector3.right, Vector3.forward);
        Vector3 eulers = transform.eulerAngles;
        eulers.z = angle;
        transform.eulerAngles = eulers;

        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        // Vector3 lookPos = Vector3.zero;// target.transform.position - transform.position;
        // //lookPos.z = 0;
        // Quaternion rotation = Quaternion.LookRotation(lookPos);
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);

        // Vector3 angles = transform.rotation.eulerAngles;
        // angles.z = Vector3.Angle(target.transform.position,transform.position);
        // transform.rotation = Quaternion.Euler(angles);

        // Vector3 dir = target.transform.position - transform.position;
        // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // transform.eulerAngles = 
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
