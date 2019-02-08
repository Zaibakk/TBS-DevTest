using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_MissileLauncher : MonoBehaviour
{
    public float duration;
    public float fireRate;
    public GameObject missile;
    public Transform shotSpawn;

    private bool active;
    private float timeActive;
    private float nextFire;

    void Start()
    {
        active = false;
        timeActive = 0.0f;
        nextFire = 0.0f;
    }

    public void Activate ()
    {
        if (!active)
            active = true;
        timeActive += duration;
    }

    void Update()
    {
        if (active)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(missile, shotSpawn.position, shotSpawn.rotation);
            }
            timeActive -= Time.deltaTime;
            if (timeActive < 0.0f)
            {
                active = false;
                timeActive = 0.0f;
                nextFire = 0.0f;
            }
        }

    }



}
