﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	// Attach to the Camera

	public GameObject player;
	
	private Vector3 offset;

	// Use this for initialization
	void Start () {
			offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (player != null)
		{
			transform.position = player.transform.position + offset;
		}
	}
}
