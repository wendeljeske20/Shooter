using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    AudioSyncer audioSyncer;
    public Weapon[] weapons;
    public float moveSpeed = 5f;


    protected virtual void Start()
    {
        //for (int i = 0; i < transform.childCount; i++)
        {
            weapons = transform.GetComponentsInChildren<Weapon>();
        }
        
        audioSyncer = GetComponent<AudioSyncer>();
    }
    protected void Shoot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].canShoot)
                weapons[i].Shoot();
        }
    }


}
