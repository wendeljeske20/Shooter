using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public enum Team
    {
        Enemy,
        Player,
    }


    public Team team;
    AudioSyncer audioSyncer;

    public float maxHealth;
    protected float currentHealth;
    [HideInInspector] public Weapon[] weapons;
    public float moveSpeed = 5f;


    protected virtual void Start()
    {
        currentHealth = maxHealth;

        weapons = transform.GetComponentsInChildren<Weapon>();


        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].team = team;
        }

        audioSyncer = GetComponent<AudioSyncer>();
    }

    protected virtual void Update()
    {
        UpdateHealth();

    }

    void UpdateHealth()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        else if (currentHealth <= 0)
        {

            Instantiate(Spawner.bigExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    protected void Shoot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].canShoot)
                weapons[i].Shoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile.team != team)
            {
                TakeDamage(projectile.damage);
                Instantiate(Spawner.smallExplosion, projectile.transform.position, Quaternion.identity);

                Destroy(projectile.gameObject);
            }

        }
    }



}
