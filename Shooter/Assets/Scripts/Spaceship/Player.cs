using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Spaceship
{
    public Weapon weapon;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAtMouse();

        if (Input.GetMouseButton(0) && weapon.canShoot)
        {
            weapon.Shoot(transform.right);
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
