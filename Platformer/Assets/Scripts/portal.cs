using System.Collections;
using System.Collections.Generic;
using UnityEngine;
Using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(collider2D other)
    {
	if (other.CompareTag("Player"))
		{
			// put level name where one is
			SceneManager.LoadScrene(1) 
		}	    
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
