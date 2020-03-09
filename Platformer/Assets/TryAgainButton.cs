using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour
{
    public void loadMenu()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("currentScene")); // "currentScene" is set in PlayerHealthCollision once player health <= 0
    }

}
