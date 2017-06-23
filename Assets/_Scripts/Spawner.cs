using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject barrelPrefab;
	public float spawnTime = 3f;


	// Use this for initialization
	void Start () 
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	void Spawn()
	{
		//Get this spawner's position and rotatin.
		Vector3 spawnPosition = transform.position;
		Quaternion spawnRotation = transform.rotation;

		//Spawn a barrel
		Instantiate (barrelPrefab, spawnPosition, spawnRotation);

	}
}
