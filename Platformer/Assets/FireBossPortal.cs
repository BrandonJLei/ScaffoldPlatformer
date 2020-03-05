using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossPortal : MonoBehaviour
{
    public GameObject fireBossPortal;
    public GameObject fireBoss;

    // Start is called before the first frame update
    void Start()
    {
        fireBossPortal.SetActive(false);
    }

    private void OnDestroy()
    {
        fireBossPortal.SetActive(true);
    }
}
