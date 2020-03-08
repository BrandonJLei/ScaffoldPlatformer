using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D rigidBody;
    public float damage = 20.0f;
    public float damageTick = 1.5f;
    private float currentTick = 0;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("currentTick: " + currentTick);
        if (collision.gameObject.CompareTag("Player") && currentTick <= 0)
        {
            PlayerHealthCollision player = collision.gameObject.GetComponent<PlayerHealthCollision>();
            if (player != null)
                player.TakeDamage(damage);
            currentTick = damageTick;
        }
        currentTick -= Time.deltaTime;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // currentTick = damageTick;
        Debug.Log("EXITED");
    }

    void FixedUpdate()
    {
        rigidBody.WakeUp();
    }
}
