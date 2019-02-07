using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public int scoreValue;
    public bool doubleLife;
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
            setRed();
    }

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}
        if (other.tag != "Player")
            Destroy(other.gameObject);
        if (!doubleLife)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<Done_PlayerController>().loseLife();
            }
            gameController.AddScore(scoreValue);
            Destroy(gameObject);
        }
        else
        {
            doubleLife = false;
            setStandard(); 
        }
	}
    
    void setRed()
    {
        Renderer rend = gameObject.transform.GetChild(2).GetComponent<Renderer>();
        original = rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", new Color(1.0f,0.0f,0.0f));
    }

    void setStandard()
    {
        Renderer rend = gameObject.transform.GetChild(2).GetComponent<Renderer>();
        rend.material.SetColor("_Color", original);
    }

}