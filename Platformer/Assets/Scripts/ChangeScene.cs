using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string toScene; // Name of the scene you want to load after clicking

    public void loadMenu()
    {
        SceneManager.LoadScene(toScene);
    }
    
}
