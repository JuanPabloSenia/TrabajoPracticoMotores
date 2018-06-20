using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	Vector3 ogPos;

	public Transform target;
	public float camSpeed;

	void Start () {
		ogPos = transform.position;
	}
	
	void FixedUpdate () {
		transform.position = Vector3.Lerp (transform.position, target.position + (target.forward * 10) + ogPos, camSpeed);
	}
}
