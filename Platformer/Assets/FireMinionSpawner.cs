using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMinionSpawner : MonoBehaviour
{
    public GameObject fireMinion;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health = fireMinion.GetComponent<EnemyHealth>().health;
        if (health <= 50)
        {
            Activator();
        }
    }

    IEnumerator Activator()
    {
        fireMinion.SetActive(false);
        yield return new WaitForSeconds(5);
        fireMinion.GetComponent<EnemyHealth>().health = 150;
        fireMinion.SetActive(true);
    }
}
