using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController: MonoBehaviour {
	public Text lifeText;
	public Text restartText;
	public Text exitText;
	public Text gameoverText;
	public Text winText;

	public int lives; 				//player lives
	public Vector3 spawnValues;		//Respawn location
	private bool gameOver = false;	//Is the game still running?
	private bool winGame = false;

	private PlayerController player;
	
	void Start ()
	{
		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject != null) 
		{
			player = playerObject.GetComponent<PlayerController>();
		}
		if (player == null) 
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		// Set the text values
		UpdateLives ();
		restartText.text = "";
		exitText.text = "EXIT";
		gameoverText.text = "";
		winText.text = "";
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameOver == true) 
		{
			Restart ();
		}
		if (winGame == true) 
		{
			Restart();
		}
	}

	public void UpdateLives()
	{
		lifeText.text = "Lives: " + lives;
	}

	public void LoseLife()
	{
		lives -= 1;
		UpdateLives ();
	}

	public void GameOver()
	{
		gameoverText.text = "Game Over!";
		gameOver = true;
	}

	public void WinGame()
	{
		Time.timeScale = 0;
		winText.text = "You Win!";
		winGame = true;
	}

	public void Restart()
	{
		restartText.text = "Press 'R' to restart";
		if (Input.GetKeyDown(KeyCode.R))
		{
			Time.timeScale = 1;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	public IEnumerator Respawn()
	{
		player.gameObject.SetActive (false);

		yield return new WaitForSeconds (3);

		player.gameObject.SetActive (true);

		player.transform.position = spawnValues;
	}

	public void ExitGame()
	{
		Application.Quit ();
	}
}
