using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallTrapSpawn : MonoBehaviour
{
    public float launchTick = 3.0f;
    public GameObject fireball;
    public Transform launchPoint;
    private float tick;

    // Start is called before the first frame update
    void Start()
    {
        tick = launchTick;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("TICK: " + tick);
        if (tick <= 0)
        {
            Debug.Log("LAUNCHING");
            Instantiate(fireball, launchPoint.position, launchPoint.rotation);
            tick = launchTick;
        }
        else
            tick -= Time.deltaTime;
    }
}
