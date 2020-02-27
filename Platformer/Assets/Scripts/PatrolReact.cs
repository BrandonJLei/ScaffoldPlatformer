using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PatrolReact : MonoBehaviour
{

    //-////////////////////////////////////////////////////
    ///
    /// Patrol takes a GameObject and makes such object to patrol specified locations at the given speed
    ///
    public float moveSpeed; //Patrol speed
    public float followMoveSpeed; //Speed when following player
    public float range; //Minimum distance for enemy to react to player
    public float verticalRange; //How far up and down the enemy moves
    public float verticalChange; //How much the y value of the GameObject will change each frame
    //verticalChange is best between 0.001f and 0.01f

    private bool movingUp; //Whether or not the enemy is moving up
    private float verticalHigh; //Highest y value for the enemy to reach
    private float verticalLow; //Lowest y value for the enemy to reach
    private Transform player; //Player to follow


    [Header("Agent's patrol areas")]
    public List<Transform> patrolLocations; //List of all the Transform locations the gameObject will patrol

    [Space, Header("Agent")]
    public GameObject patrollingGameObject; //Unity GameObject that patrols
    private int nextPatrolLocation; //Keeps track of the patrol location

    private SpriteRenderer mySpriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        verticalHigh = patrollingGameObject.transform.position.y + verticalRange;
        verticalLow = patrollingGameObject.transform.position.y - verticalRange;
        movingUp = true;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    //-////////////////////////////////////////////////////
    ///
    /// Update is called once per frame
    ///
    void Update()
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
        if (Vector2.Distance(patrollingGameObject.transform.position, player.position) <= range)
        {
            Flip(player);

            patrollingGameObject.transform.position = Vector2.MoveTowards(patrollingGameObject.transform.position,
            new Vector2(player.position.x, patrollingGameObject.transform.position.y), followMoveSpeed * Time.deltaTime);
        }
        else
        {
            Flip(patrolLocations[nextPatrolLocation]);

            patrollingGameObject.transform.position = Vector2.MoveTowards(patrollingGameObject.transform.position,
            new Vector2(patrolLocations[nextPatrolLocation].position.x, patrollingGameObject.transform.position.y), moveSpeed * Time.deltaTime);

            if (Vector2.Distance(patrollingGameObject.transform.position, patrolLocations[nextPatrolLocation].position) <= 2)
                {
                nextPatrolLocation = (nextPatrolLocation + 1) % patrolLocations.Count; //Prevents IndexOutofBound by looping back through list
                }
        }
        Vertical();
    }

    //-////////////////////////////////////////////////////
    ///
    /// Makes the patrollingGameObject always be facing the next patrol location
    ///
    private void Flip(Transform nextLocation)
    {
        if (patrollingGameObject.transform.position.x - nextLocation.position.x > 0)
            mySpriteRenderer.flipX = false;
        else
            mySpriteRenderer.flipX = true;
    }

    private void Vertical()
    {
        if (movingUp && patrollingGameObject.transform.position.y >= verticalHigh)
        {
            movingUp = false;
            verticalChange = -verticalChange;
        }
        else if (!movingUp && patrollingGameObject.transform.position.y <= verticalLow)
        {
            movingUp = true;
            verticalChange = -verticalChange;
        }
        Vector3 newPos = patrollingGameObject.transform.position;
        newPos.y += verticalChange;
        patrollingGameObject.transform.position = newPos;
    }
}
