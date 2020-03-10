using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrap : MonoBehaviour
{

    [SerializeField]
    private LayerMask GroundLayer;
    [SerializeField]
    private Transform GroundCheck;
    public float damage = 20.0f;
    public bool isActive = true;
    public float despawnTime = 1.5f;

    private bool hitGround;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
            if (collision.CompareTag("Player"))
            {
                rb.isKinematic = false;
            }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive)
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerHealthCollision player = collision.gameObject.GetComponent<PlayerHealthCollision>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                    isActive = false;
                }
            }
    }

    void FixedUpdate()
    {
        bool grounded = Physics2D.Linecast(transform.position, GroundCheck.position, GroundLayer);
        if (grounded && isActive)
        {
            isActive = false;
            hitGround = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hitGround && !isActive)
            despawn();
    }

    void despawn()
    {
        if (despawnTime <= 0)
            Destroy(gameObject);
        else
            despawnTime -= Time.deltaTime;
    }
}
