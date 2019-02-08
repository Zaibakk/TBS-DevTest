using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_Shield : MonoBehaviour
{
    private bool active;

    void Start()
    {
        active = false;
    }

    public void Activate()
    {
        if (!active)
        {
            active = true;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            active = false;
            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }


}
