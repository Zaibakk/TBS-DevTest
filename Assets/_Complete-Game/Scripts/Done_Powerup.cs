using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class handle the powerups object in the game
public class Done_Powerup : MonoBehaviour
{
    public bool health; //true if it has the health power
    public bool shield; //true if it has the shield power
    public bool missile; //true if it has the missile power

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            if (health)
                DoHealth(other);
            if (shield)
                DoShield(other);
            if (missile)
                DoMissile(other);
            GetComponent<AudioSource>().Play();
            float lenght = GetComponent<AudioSource>().clip.length;
            Destroy(gameObject, lenght);
        }
    }

    public void DoHealth(Collider other)
    {
        other.gameObject.GetComponent<Done_PlayerController>().AddLife();
    }

    public void DoShield(Collider other)
    {
        other.gameObject.GetComponent<Done_PlayerController>().ActivateShield();
    }

    public void DoMissile(Collider other)
    {
        other.gameObject.GetComponent<Done_PlayerController>().ActivateMissile();
    }


}
