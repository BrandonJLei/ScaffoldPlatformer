using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    Color m_NewColor;

    public int health = 100;
    private bool onFire = false;
    //public GameObject deathEffect;
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

    public void DamageOverTime(int damage, int damageTime)
    {
        if(onFire == false)
        {
            StartCoroutine(DamageOverTimeCoroutine(damage, damageTime));
        }
        
    }

    IEnumerator DamageOverTimeCoroutine(int damageAmount, int duration)
    {
        onFire = true;
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
    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
