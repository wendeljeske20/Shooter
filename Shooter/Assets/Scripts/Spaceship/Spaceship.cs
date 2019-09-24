using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public Weapon[] weapons;
    public float moveSpeed = 5f;


    protected  void Shoot(Vector3 direction)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].canShoot)
                weapons[i].Shoot(direction);
        }
    }

    
}
