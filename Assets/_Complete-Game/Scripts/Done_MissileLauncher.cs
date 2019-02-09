using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class handle the missile launcher of the player
//it is activated when the missile power up is taken
public class Done_MissileLauncher : MonoBehaviour
{
    public float duration; //the duration of the power
    public float fireRate; //the fire rate at which missiles are shot
    public GameObject missile;
    public Transform shotSpawn;

    private bool active; //if the power is active
    private float timeActive; //time left until the power expires
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
