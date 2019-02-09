using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public int scoreValue;
    public bool doubleLife; //true if it has 2 lives, like superships
    public bool isShip; //true if it is a ship
    public bool isKamikaze; //true if it is a kamikaze
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
        if (doubleLife || isKamikaze)
            SetColor();
    }

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Powerup")
		{
			return;
		}
        if (other.tag != "Player" && other.tag != "Shield")
            Destroy(other.gameObject);
        if (other.tag == "Player")
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
    
    //this method set the colour of a super ship or a kamikaze ship
    void SetColor()
    {
        Renderer rend = gameObject.transform.GetChild(2).GetComponent<Renderer>();
        original = rend.material.GetColor("_Color");
        if (doubleLife)
            rend.material.SetColor("_Color", new Color(1.0f,0.0f,0.0f));
        if (isKamikaze)
            rend.material.SetColor("_Color", new Color(0.0f, 1.0f, 0.0f));
    }

    //this method set the colours of a ship back to his standard coloration
    void SetStandard()
    {
        Renderer rend = gameObject.transform.GetChild(2).GetComponent<Renderer>();
        rend.material.SetColor("_Color", original);
    }

}