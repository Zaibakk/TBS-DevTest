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

    //this method take away a life from the player, and eventually kill him if he reaches 0 lives
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

    //this method add a life to the player, without exceeding the starting limit
    public void AddLife()
    {
        if (lives < maxLives)
        {
            lives++;
            UpdateHealthBar();
        }
    }

    //this method update the health bar in the UI
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

    //this method activate the shield when the shield powerup is taken
    public void ActivateShield()
    {
        transform.GetChild(2).gameObject.GetComponent<Done_Shield>().Activate();
    }

    //this method activate the missile launcher when the missile powerup is taken
    public void ActivateMissile()
    {
        GetComponent<Done_MissileLauncher>().Activate();
    }

}
