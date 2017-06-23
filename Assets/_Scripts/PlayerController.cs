using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;					//Speed the player can move
	public float jumpPower;				//Jump power

	private bool isGrounded = false; 	//Is the player on the ground?
	private bool hasKey = false;		//Did the player pick up the key?

	private Rigidbody rb;
	private GameController gameController;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();

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

	void Update()
	{
		if (Physics.Raycast (transform.position, -Vector3.up, 0.13f /*This is the minimum distance from the Raycast
		                                                             starting position before the player can jump again.*/)) {
			//something is beneath the player. They can jump.
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}

		//Jump when the space bar is pressed and the player is on the ground.
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.AddForce(transform.up * jumpPower);
		}
	}

	void FixedUpdate()
	{

		//Move the player left or right

		Vector3 moveDir = Vector3.zero;
		moveDir.x = Input.GetAxis("Horizontal"); // get result of <- -> keys in X
		// move this object at frame rate independent speed:
		transform.position += moveDir * speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		//Collect the key and end the game.
		if (other.gameObject.tag == "Key")
		{
			other.gameObject.SetActive(false);
			hasKey = true;

			//Add a life to the player
			gameController.lives += 1;
			gameController.UpdateLives();

			//Set the new spawn point to the key's location.
			gameController.spawnValues = other.transform.position;
		}

		if (other.gameObject.tag == "Finish" && hasKey == true) 
		{
			//Stop time and display "You Win!" text, ending the game.
			gameController.WinGame();
		}
	}
	
}
