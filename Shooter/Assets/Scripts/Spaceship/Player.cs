using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Spaceship
{
    AudioSyncer audioSyncer;
    void Start()
    {
        audioSyncer = GetComponent<AudioSyncer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAtMouse();

        // if (Input.GetMouseButton(0))
        {
            if (audioSyncer.isBeat)
            {
                Shoot(transform.right);
                audioSyncer.isBeat = false;
            }


        }
    }

    void Move()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), (Input.GetAxis("Vertical")));
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(input.x * moveSpeed, input.y * moveSpeed, 0);
    }

    void LookAtMouse()
    {

        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
