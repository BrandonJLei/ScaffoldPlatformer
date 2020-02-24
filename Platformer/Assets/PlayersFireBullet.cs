using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersFireBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public int damageDealtOverTime = 10;
    public int damageTime = 5;
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
            enemy.DamageOverTime(damageDealtOverTime, damageTime);
        }
        Destroy(gameObject);
    }
}
