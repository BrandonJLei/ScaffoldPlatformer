using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFireRate : MonoBehaviour
{

    private float timeBtwShots; 
    public float startTimeBtwShots;
    public GameObject projectile;
	public Transform enemyFirePoint;

	void Start()
	{
		timeBtwShots = startTimeBtwShots;
	}


	void Update() 
	{

	if (timeBtwShots <= 0) {
	    Instantiate(projectile, enemyFirePoint.position, Quaternion.identity); 
	    timeBtwShots = startTimeBtwShots;  


	}

	else {
	    timeBtwShots -= Time.deltaTime; 
	}

	}



}