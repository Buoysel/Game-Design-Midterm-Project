﻿using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public Vector3 rotate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Rotate(rotate);
	}
}
