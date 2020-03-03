using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{   
    public string levelName;

    public void OnTriggerEnter2D(Collider2D collision)

    {
	    if(collision.tag == "Player")
		{
			// put level name where one is
			UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
		}	    
        
    }
}
