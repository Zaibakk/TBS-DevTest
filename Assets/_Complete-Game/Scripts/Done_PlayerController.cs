using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

    public int lives;
    public GameObject playerExplosion;

    private float nextFire;
    private Done_GameController gameController;
    private Transform healthBar;
    private int maxLives;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<Done_GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        GameObject gameHealthBarObject = GameObject.FindGameObjectWithTag("Healthbar");
        if (gameHealthBarObject != null)
        {
            healthBar = gameHealthBarObject.transform;
        }
        maxLives = lives;
    }

    void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}

    public void LoseLife()
    {
        lives--;
        UpdateHealthBar();
        if (lives<=0)
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
            gameController.GameOver();
            Destroy(gameObject);
        }
    }

    public void AddLife()
    {
        if (lives < maxLives)
        {
            lives++;
            UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        for (int i=0; i<healthBar.childCount; i++)
        {
            if (i<lives)
            {
                healthBar.GetChild(i).gameObject.GetComponent<RawImage>().enabled = true;
            }
            else
            {
                healthBar.GetChild(i).gameObject.GetComponent<RawImage>().enabled = false;
            }
        }
    }

    public void ActivateShield()
    {
        transform.GetChild(2).gameObject.GetComponent<Done_Shield>().Activate();
    }

    public void ActivateMissile()
    {

    }

}
