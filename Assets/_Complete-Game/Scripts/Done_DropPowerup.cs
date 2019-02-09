using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this method let the enemy ships to drop powerups when they die
public class Done_DropPowerup : MonoBehaviour
{
    public float dropChance; //the chance to drop a powerup
    public GameObject[] powerups;

    //this method have a chance to drop a powerup. It is called when the ship is dead
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
