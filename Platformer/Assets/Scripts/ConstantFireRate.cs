using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFireRate : MonoBehaviour
{

    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    public Transform enemyFirePoint;
    [SerializeField, Range(0, 2.0f)]
    public float playerRangeMultiplier = 0.75f;

    private float fireDistance;

    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        // fireDistance is exactly the camera's width in worldspace units multipled by the playerRangeMultiplier
        fireDistance = 2.0f * Camera.main.orthographicSize * Camera.main.aspect * playerRangeMultiplier;
    }


    void Update()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 playerVector = new Vector2(playerTransform.position.x - gameObject.transform.position.x, playerTransform.position.y - gameObject.transform.position.y);
        if (playerVector.magnitude <= fireDistance)
        {
            if (timeBtwShots <= 0)
            {
                Instantiate(projectile, enemyFirePoint.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        /// If we ever want the timeBtwShots to keep going no matter what
        /// then we can use something like the code here:
        // timeBtwShots = timeBtwShots <= 0 ? startTimeBtwShots : timeBtwShots - Time.deltaTime;
    }



}