using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionRespawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemy1;
    public GameObject enemy2;
    private Transform loc1;
    private Transform loc2;
    private bool spawning1;
    private bool spawning2;

    // Start is called before the first frame update
    void Start()
    {
        loc1 = enemy1.transform;
        loc2 = enemy2.transform;
        enemy1 = enemy1.transform.GetChild(0).gameObject;
        enemy2 = enemy2.transform.GetChild(0).gameObject;
        Debug.Log(enemy1);
        Debug.Log(enemy2);
        Debug.Log(loc1);
        Debug.Log(loc2);
        spawning1 = false;
        spawning2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy1 == null && !spawning1)
        {
            spawning1 = true;
            StartCoroutine(RespawnEnemy1());
        }
        
        if (enemy2 == null && !spawning2)
        {
            spawning2 = true;
            StartCoroutine(RespawnEnemy2());
        }
    }

    IEnumerator RespawnEnemy1()
    {
        yield return new WaitForSeconds(5);
        Debug.Log(loc1);
        GameObject newEnemy1 = Instantiate(enemyPrefab, loc1);
        enemy1 = newEnemy1.transform.GetChild(0).gameObject;
        loc1 = newEnemy1.transform.GetChild(0).transform;
        spawning1 = false;
    }

    IEnumerator RespawnEnemy2()
    {
        yield return new WaitForSeconds(5);
        Debug.Log(loc2);
        GameObject newEnemy2 = Instantiate(enemyPrefab, loc2);
        enemy2 = newEnemy2.transform.GetChild(0).gameObject;
        loc2 = newEnemy2.transform.GetChild(0).transform;
        spawning2 = false;
    }
}
