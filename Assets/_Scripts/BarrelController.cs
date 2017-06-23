using UnityEngine;
using System.Collections;

public class BarrelController : MonoBehaviour {
	
	public Vector3 force; 				//The constant force a barrel will roll
	public float speed;

	void Start()
	{
		//If the barrel is facing 90 degrees when it spawns...
		if (this.transform.rotation.y > 0) {
			//...Roll to the right. (Absolute value of speed)
			speed = Mathf.Abs (speed);
		}
		// If the barrel is facing -90 degrees when it spawns...
		else if (this.transform.rotation.y < 0) 
		{
			//...Roll to the left. (Negative absolute value of speed)
			speed = -Mathf.Abs(speed);
		}

	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		//Move the barrel forward in a constant force.x * speed
		if ( this.gameObject.GetComponent<Rigidbody>().velocity.magnitude < 2.5f)
		{ 
			this.GetComponent<Rigidbody>().AddForce(Vector3.right * speed); 
		}

	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Border" || other.gameObject.tag == "Barrel")
		{
			//Roll in the opposite direction.
			if (speed > 0)
				speed =  -Mathf.Abs(speed);
			else if (speed < 0)
				speed = Mathf.Abs(speed);
		}
	}
}
