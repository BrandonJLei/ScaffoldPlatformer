﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upFireBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public int damageDealtOverTime = 10;
    public int damageTime = 5;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                enemy.DamageOverTime(damageDealtOverTime, damageTime);
            }
            Destroy(gameObject);
        }
        if (hitInfo.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}