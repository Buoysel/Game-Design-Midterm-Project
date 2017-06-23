using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject barrelExplosion; 	//The barrel, player and shockwave explosions
	public GameObject playerExplosion;
	public GameObject shockwaveExplosion;

	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) 
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	/*This next section handles the player and barrel explosions for when they come in contact with each other
	  or if they come in contact with the grinder. It uses some repeated code, but if I made a seperate method,
	  I would have to include the Collision/Collider parameter as an argument to get the location of the object
	  involved. I wasn't sure if I could make a method hold both of them and Unity would decide which one to use,
	  or make two separate methods -- each using Collision and Collider -- and have them create explosions, but
	  would still result in repeating code... */


	//Barrel Collides with player
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") 
		{
			//Deactivate the player gameObject and explode 
			Instantiate(shockwaveExplosion, other.transform.position, other.transform.rotation);
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			Instantiate(barrelExplosion, transform.position, transform.rotation);

			if (gameController.lives > 0)
			{
				gameController.LoseLife();
				StartCoroutine(gameController.Respawn());
			}
			else 
			{
				other.gameObject.SetActive (false);
				gameController.GameOver();
			}

			/*Disabling all the componenets one at a time because the player doesn't respawn
			 from a barrel attack if I use Destroy or SetActive... And if I delay the Destroy function,
			 then only left a phantom barrel to interact with the world. */
			this.gameObject.GetComponent<Renderer>().enabled = false;
			this.gameObject.GetComponent<Light>().enabled = false;
			this.gameObject.GetComponent<Collider>().enabled = false;
			Destroy(this.gameObject, 3.1f);
		}
	}

	//Grinder touches player or Barrel
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			//player explosion
			Instantiate(shockwaveExplosion, other.transform.position, other.transform.rotation);
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

			//other.gameObject.SetActive (false);
			if (gameController.lives > 0)
			{
				gameController.LoseLife();
				StartCoroutine(gameController.Respawn());
			}
			else 
			{
				other.gameObject.SetActive (false);
				gameController.GameOver();
			}
		}
		if (other.gameObject.tag == "Barrel") 
		{
			//barrel explosion
			Instantiate(barrelExplosion, other.transform.position, other.transform.rotation);
			Destroy (other.gameObject);
		}
	}
	
}
