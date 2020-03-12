using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 5;
    public int slowTime = 2;
    public int slowAmount = 1;
    public int patrolSlowAmount = 1;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = hitInfo.GetComponent<EnemyHealth>();
            Patrol enemySpeed = hitInfo.GetComponent<Patrol>();
            PatrolReact enemyReactSpeed = hitInfo.GetComponent<PatrolReact>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);

            }
            if (enemySpeed != null)
            {
                enemySpeed.slow(slowAmount, slowTime);
            }
            if (enemyReactSpeed != null)
            {
                enemyReactSpeed.slow(patrolSlowAmount, slowTime);
            }
            Destroy(gameObject);
        }
        if (hitInfo.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
        
    }
}
