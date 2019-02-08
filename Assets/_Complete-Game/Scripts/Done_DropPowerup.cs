using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_DropPowerup : MonoBehaviour
{

    public float dropChance;
    public GameObject[] powerups;

    public void Drop()
    {
        float rng = UnityEngine.Random.value;
        if (rng <= dropChance)
        {
            GameObject powerup = powerups[Random.Range(0, powerups.Length)];
            Instantiate(powerup, transform.position, Quaternion.identity);
        }
    }




}
