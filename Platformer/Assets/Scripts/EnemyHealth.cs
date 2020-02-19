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
    //public GameObject deathEffect;
    [Header("Agent's patrol areas")]
    public List<Transform> patrolLocations; //List of all the Transform locations the gameObject will patrol

    [Space, Header("Agent")]
    public GameObject patrollingGameObject; //Unity GameObject that patrols
    private int nextPatrolLocation; //Keeps track of the patrol location

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PatrolArea();
    }

    private void PatrolArea()
    {
        Flip();
        patrollingGameObject.transform.position = Vector2.MoveTowards(patrollingGameObject.transform.position,
            patrolLocations[nextPatrolLocation].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(patrollingGameObject.transform.position, patrolLocations[nextPatrolLocation].position) <= 2)
        {
            nextPatrolLocation = (nextPatrolLocation + 1) % patrolLocations.Count; //Prevents IndexOutofBound by looping back through list
        }
    }

    private void Flip()
    {
        Vector2 localScale = patrollingGameObject.transform.localScale;
        if (patrollingGameObject.transform.position.x - patrolLocations[nextPatrolLocation].position.x > 0)
            localScale.x = 1;
        else
            localScale.x = -1;
        patrollingGameObject.transform.localScale = localScale;
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

    public void slow(int slowAmount, int slowTime)
    {
        if (slowed == false)
        {
            StartCoroutine(SlowCoroutine(slowAmount, slowTime));
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

    IEnumerator SlowCoroutine(int slowAmount, int slowTime)
    {
        Color frozenColor = new Color(0.8f, 1.0f, 1.0f, .4f);
        slowed = true;
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
