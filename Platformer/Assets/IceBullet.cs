using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 5;
    public int slowTime = 2;
    public int slowAmount = 1;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            enemy.slow(slowAmount, slowTime);
        }
        Destroy(gameObject);
    }
}
