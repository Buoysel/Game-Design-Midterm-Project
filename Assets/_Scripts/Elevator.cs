using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public Transform destinationSpot;
	public Transform originSpot;
	public float speed;
	private bool switchDirection = false;

	void FixedUpdate()
	{
		//Checking the position of the platform. Set's Switch to true if it has reached the destination spot
		if (transform.position == destinationSpot.position) 
		{
			switchDirection = true;
		}
		if (transform.position == originSpot.position)
		{
			switchDirection = false;
		}

		//If switchDirection becomes true, the destination has been reached, and move the other way.
		if (switchDirection) 
		{
			transform.position = Vector3.MoveTowards(transform.position, originSpot.position, speed);
		}
		else 
		{
			//If switchDirection is false, move to the destination
			transform.position = Vector3.MoveTowards(transform.position, destinationSpot.position, speed);
		}
	}
}
