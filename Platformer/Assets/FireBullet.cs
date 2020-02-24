using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 20;
    public int damageDealtOverTime = 10;
    public int damageTime = 5;
    public Rigidbody2D rb;

    private Transform player;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = (player.position - transform.position).normalized;
    }

    void Update()
    {
        transform.position += target * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
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
}
