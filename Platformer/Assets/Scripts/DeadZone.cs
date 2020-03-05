using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//-////////////////////////////////////////////////////
///
/// Used to define areas in which the player instantly dies and triggers Game Over sequence
///
public class DeadZone : MonoBehaviour 
{

    //-////////////////////////////////////////////////////
    ///
    /// Triggers GameOver if player hits a DeadZone area on the game scene
    ///
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("DeathScene");
        }
    }
}
