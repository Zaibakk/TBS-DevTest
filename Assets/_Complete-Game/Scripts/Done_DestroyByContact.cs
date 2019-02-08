using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public int scoreValue;
    public bool doubleLife;
    public bool isShip;
	private Done_GameController gameController;
    private Color original;


	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
        if (doubleLife)
            SetRed();
    }

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Powerup")
		{
			return;
		}
        if (other.tag != "Player")
            Destroy(other.gameObject);
        else
        {
            other.gameObject.GetComponent<Done_PlayerController>().LoseLife();
        }
        if (!doubleLife)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            gameController.AddScore(scoreValue);
            if (isShip)
                gameObject.GetComponent<Done_DropPowerup>().Drop();
            Destroy(gameObject);
        }
        else
        {
            doubleLife = false;
            SetStandard(); 
        }
	}
    
    void SetRed()
    {
        Renderer rend = gameObject.transform.GetChild(2).GetComponent<Renderer>();
        original = rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", new Color(1.0f,0.0f,0.0f));
    }

    void SetStandard()
    {
        Renderer rend = gameObject.transform.GetChild(2).GetComponent<Renderer>();
        rend.material.SetColor("_Color", original);
    }

}