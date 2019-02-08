using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_Powerup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<Done_PlayerController>().AddLife();
            GetComponent<AudioSource>().Play();
            float lenght = GetComponent<AudioSource>().clip.length;
            Destroy(gameObject, lenght);
        }
    }


}
