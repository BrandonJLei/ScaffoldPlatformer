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
    public float cameraDistanceMultiplier = 0.75f;

    private float fireDistance;

    void Start()
    {
        timeBtwShots = startTimeBtwShots;
        // fireDistance is exactly the camera's width in worldspace units multipled by the cameraDistanceMultiplier
        fireDistance = 2.0f * Camera.main.orthographicSize * Camera.main.aspect * cameraDistanceMultiplier;
    }


    void Update()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 playerVector = new Vector2(playerTransform.position.x - gameObject.transform.position.x, playerTransform.position.y - gameObject.transform.position.y);
        Debug.Log("PLAYER DISTANCE: " + playerVector.magnitude);
        Debug.Log("PLAYER VECTOR...");
        Debug.Log(playerVector);
        Debug.Log("FIRE DISTANCE: " + fireDistance);
        Debug.DrawLine(gameObject.transform.position, enemyFirePoint.position, Color.white);
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
        // timeBtwShots = timeBtwShots <= 0 ? startTimeBtwShots : timeBtwShots - Time.deltaTime;
    }



}