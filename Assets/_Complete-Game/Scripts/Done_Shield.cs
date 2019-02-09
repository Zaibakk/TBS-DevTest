using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class handle the shield power
public class Done_Shield : MonoBehaviour
{
    private bool active; //true when the shield power is active

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
