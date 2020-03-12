using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMinionFireSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy1;
    public GameObject enemy2;

    void Start()
    {
        enemy1.SetActive(false);
        enemy2.SetActive(false);
        Invoke("spawnEnemy", 5.0f);
        Invoke("spawnEnemy2", 20.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnEnemy()
    {
        enemy1.SetActive(true);
    }

    void spawnEnemy2()
    {
        enemy2.SetActive(true);
    }
}
