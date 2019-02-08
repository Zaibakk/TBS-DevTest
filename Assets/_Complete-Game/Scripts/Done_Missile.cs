using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_Missile : MonoBehaviour
{
    public float speed;
    public float force;
    public float maxSpeed;

    private Transform locked;

    void Start()
    {
        transform.Rotate(Vector3.right*90.0f);
        TargetEnemy();
        GetComponent<Rigidbody>().velocity = transform.up * speed;
    }

    void FixedUpdate()
    {
        TargetEnemy();
    }

    private void TargetEnemy()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> enemiesObjects = new List<GameObject>();
        if (allEnemies.Length > 0)
        {
            for (int i = 0; i < allEnemies.Length; i++)
            {
                if (allEnemies[i].GetComponentInParent<Done_DestroyByContact>() != null && allEnemies[i].GetComponentInParent<Done_DestroyByContact>().isShip)
                {
                    enemiesObjects.Add(allEnemies[i]);
                }
            }
        }
        if (enemiesObjects.Count > 0)
        {
            Transform[] enemies = new Transform[enemiesObjects.Count];
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = enemiesObjects[i].transform;
            }
            Transform closestEnemy = GetClosestEnemy(enemies);
            RotateMissile(closestEnemy);
        }
    }

    private Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    private void RotateMissile (Transform target)
    {
        Vector3 heading = target.position - transform.position;
        transform.LookAt(target);
        transform.Rotate(Vector3.right * 90.0f);
        transform.GetChild(0).transform.LookAt(target);

        GetComponent<Rigidbody>().AddForce(heading * force);
        /*
        float totalVelocity =  + GetComponent<Rigidbody>().velocity.z;
        if (GetComponent<Rigidbody>().velocity.x > maxSpeed)
        {
            GetComponent<Rigidbody>().velocity = new Vector3 (maxSpeed, 0.0f, GetComponent<Rigidbody>().velocity.z);
        }
        if (GetComponent<Rigidbody>().velocity.z > maxSpeed)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0.0f, maxSpeed);
        }
        */
    }


}
