using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;

    public int health = 100;
    public float moveSpeed;
    public bool respawnOn = false;
    public float respawnTime = 5.0f;
    public Image healthBar;
    private bool onFire = false;
    private bool slowed = false;
    private int startHealth;


    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        startHealth = health;
        if (healthBar != null)
        {
            float healthPercent = (float)health / startHealth;
            Debug.Log(healthPercent);
            healthBar.fillAmount = healthPercent;
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (healthBar != null)
        {
            Debug.Log(health);
            Debug.Log(startHealth);
            float healthPercent = (float)health / startHealth;
            Debug.Log(healthPercent);
            healthBar.fillAmount = healthPercent;
        }
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
            if (healthBar != null)
            {
                float healthPercent = (float)health / startHealth;
                Debug.Log(healthPercent);
                healthBar.fillAmount = healthPercent;
            }
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
        if (respawnOn)
        {
            gameObject.SetActive(false);
            health = startHealth;
            Invoke("Respawn", respawnTime);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }
}
