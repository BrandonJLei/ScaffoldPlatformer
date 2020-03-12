using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballTrap : MonoBehaviour
{
    public float damage = 20.0f;
    public float launchForce = 650f;
    // private bool travelling = false;
    private Vector2 startLocation;
    private Rigidbody2D rb;

    void Start()
    {
        startLocation = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        launchFireball();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            PlayerHealthCollision player = hitInfo.GetComponent<PlayerHealthCollision>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (gameObject.transform.position.y < startLocation.y)
            Destroy(gameObject);
    }

    void launchFireball()
    {
        rb.AddForce(new Vector2(0f, launchForce));
    }
}
