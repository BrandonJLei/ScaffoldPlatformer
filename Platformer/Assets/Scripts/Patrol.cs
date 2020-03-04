using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    //-////////////////////////////////////////////////////
    ///
    /// Patrol takes a GameObject and makes such object to patrol specified locations at the given speed
    ///
    public float moveSpeed; //Patrol speed
    SpriteRenderer m_SpriteRenderer;
    private bool slowed = false;
    [Header("Agent's patrol areas")]
    public List<Transform> patrolLocations; //List of all the Transform locations the gameObject will patrol

    [Space, Header("Agent")]
    public GameObject patrollingGameObject; //Unity GameObject that patrols
    private int nextPatrolLocation; //Keeps track of the patrol location
	
    //-////////////////////////////////////////////////////
    ///
	/// Update is called once per frame
    ///
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
	void Update () 
    {
        PatrolArea();
	}

    //-////////////////////////////////////////////////////
    ///
    /// Moves the patrollingGameObject towards patrol location,
    /// when reach destination switch to next patrol position in the list
    ///
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

    //-////////////////////////////////////////////////////
    ///
    /// Makes the patrollingGameObject always be facing the next patrol location
    ///
    private void Flip()
    {
        Vector2 localScale = patrollingGameObject.transform.localScale;
        if (patrollingGameObject.transform.position.x - patrolLocations[nextPatrolLocation].position.x > 0)
            localScale.x = 1;
        else
            localScale.x = -1;
        patrollingGameObject.transform.localScale = localScale;
    }

    public void slow(int slowAmount, int slowDuration)
    {
        if (slowed == false)
        {
            slowed = true;
            StartCoroutine(SlowCoroutine(slowAmount, slowDuration));
        }
            
    }


    IEnumerator SlowCoroutine(int slowAmount, int slowTime)
    {
        Color frozenColor = new Color(0.8f, 1.0f, 1.0f, .4f);
        if (moveSpeed > slowAmount)
            moveSpeed -= slowAmount;
        int timeSlowed = 0;
        while (timeSlowed < slowTime)
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
}
