using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_Powerup : MonoBehaviour
{
    public bool health;
    public bool shield;
    public bool missile;

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
