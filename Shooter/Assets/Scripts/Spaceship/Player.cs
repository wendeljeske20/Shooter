using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Spaceship
{
    public Image healthBar;

    protected override void Update()
    {
        base.Update();

        Move();
        LookAtMouse();
        Shoot();

        UpdateHealthBar();
    }

    void Move()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), (Input.GetAxis("Vertical")));
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 newPosition = transform.position;
        if (transform.position.x < -13)
            newPosition.x = -13;
        else if (transform.position.x > 13)
            newPosition.x = 13;
            
        if (transform.position.y < -7)
            newPosition.y = -7;
        else if (transform.position.y > 7)
            newPosition.y = 7;



        transform.position = newPosition;


        rb.velocity = new Vector3(input.x * moveSpeed, input.y * moveSpeed, 0);
    }


    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    void LookAtMouse()
    {

        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.forward);
        transform.rotation = targetRotation;

    }
}
