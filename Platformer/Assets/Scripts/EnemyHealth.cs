using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;

    public int health = 100;
    public float moveSpeed; 
    private bool onFire = false;
    private bool slowed = false;


    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void DamageOverTime(int damage, int damageDuration)
    {
        if(onFire == false && slowed == false)
        {
            onFire = true;
            StartCoroutine(DamageOverTimeCoroutine(damage, damageDuration));
        }
    }

    public void slow(int slowAmount, int slowDuration)
    {
        if (slowed == false && onFire == false)
        {
            slowed = true;
            StartCoroutine(SlowCoroutine(slowAmount, slowDuration));
        }
            
    }

    IEnumerator DamageOverTimeCoroutine(int damageAmount, int duration)
    {
        
        int amountDamaged = 0;
        int damagePerLoop = damageAmount / duration;
        while(amountDamaged < damageAmount)
        {
            m_SpriteRenderer.color = Color.red;
            health -= damagePerLoop;
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(.5f);
            m_SpriteRenderer.color = Color.white;   
            yield return new WaitForSeconds(.5f);
        }
        onFire = false;
    }

    IEnumerator SlowCoroutine(int slowAmount, int slowTime)
    {
        Color frozenColor = new Color(0.8f, 1.0f, 1.0f, .4f);
        if (moveSpeed > slowAmount)
            moveSpeed -= slowAmount;
        int timeSlowed = 0;
        while(timeSlowed < slowTime)
        {
            m_SpriteRenderer.color = frozenColor;
            timeSlowed += 1;
            yield return new WaitForSeconds(.5f);
            m_SpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(.5f);
        }
        moveSpeed += slowAmount;
        slowed = false;
    }
    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
