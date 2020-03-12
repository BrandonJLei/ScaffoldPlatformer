using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballTrap : MonoBehaviour
{
    public float damage = 20.0f;
    public float launchTick = 3.0f;
    public float launchForce = 500f;
    private float tick;
    private bool travelling = false;
    private Vector2 startLocation;
    private Rigidbody2D rb;

    void Start()
    {
        tick = launchTick;
        startLocation = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthCollision player = collision.gameObject.GetComponent<PlayerHealthCollision>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

    void FixedUpdate()
    {
        Debug.Log("TICK: " + tick);
        if (tick <= 0)
        {
            rb.isKinematic = false;
            launchFireball();
            travelling = true;
            tick = launchTick;
        }
        else if (travelling && gameObject.transform.position.y <= startLocation.y)
        {
            gameObject.transform.position = startLocation;
            travelling = false;
            rb.isKinematic = true;
        }
        else
        {
            tick -= Time.deltaTime;
        }
    }

    void launchFireball()
    {
        rb.AddForce(new Vector2(0f, launchForce));
    }
}
