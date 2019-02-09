using UnityEngine;
using System.Collections;

public class Done_EvasiveManeuver : MonoBehaviour
{
	public Done_Boundary boundary;
	public float tilt;
	public float dodge;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;

    //the distance in the Z axis at which a kamikaze ship will start his kamikaze attack
    public float kamikazeDodgeAbility;
    public float distanceToStartKA; 

	private float currentSpeed;
	private float targetManeuver;
    private bool isKamikaze; //if it is a kamikaze, he will uses the superdodge method

	void Start ()
	{
        isKamikaze = GetComponent<Done_DestroyByContact>().isKamikaze;
		currentSpeed = GetComponent<Rigidbody>().velocity.z;
        if (!isKamikaze)
		    StartCoroutine(Evade());
    }
	
	IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
		while (true)
		{
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}

    void FixedUpdate ()
	{
        if (isKamikaze)
        {
            KamikazeBehaviour();
        }

		float newManeuver = Mathf.MoveTowards (GetComponent<Rigidbody>().velocity.x, targetManeuver, smoothing * Time.deltaTime);
        GetComponent<Rigidbody>().velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0, 0, GetComponent<Rigidbody>().velocity.x * -tilt);
	}

    //this method is the behaviour of the kamikaze. 
    //It try to dodge player's shots, and when near to the player it try crash into him
    public void KamikazeBehaviour()
    {
        // verify if player is roughly on the same x axis
        //if it's the case start the kamikaze manouvre
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && (transform.position.z - player.transform.position.z) <= distanceToStartKA)
        {
            //this is the kamikaze manouvre. 
            //The ship is guided towards the player.
            targetManeuver = (player.transform.position.x - transform.position.x)*2.0f;
            
        }
        else
        {
            SuperDodge();
        }
    }


    //this method is the dodge method of the kamikaze. 
    //It try to dodge player's shots.
    public void SuperDodge()
    {
        GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Bullet");
        if (allBullets.Length == 0)
            return;
        float minDistance = 100.0f;
        targetManeuver = 0.0f;
        bool dangerZone = false;
        for (float i= boundary.xMin; i <= boundary.xMax; i++)
        {
            dangerZone = false;
            for (int j=0; j<allBullets.Length; j++)
            {
                if (Mathf.Abs(i - allBullets[j].transform.position.x) < kamikazeDodgeAbility)
                {
                    dangerZone = true;
                }
            }
            if (!dangerZone && Mathf.Abs(transform.position.x - i) < minDistance)
            {
                minDistance = Mathf.Abs(transform.position.x - i);
                targetManeuver = (i - transform.position.x);
            }
        }
    }

}
