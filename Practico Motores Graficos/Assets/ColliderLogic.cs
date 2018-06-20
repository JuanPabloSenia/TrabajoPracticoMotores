using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLogic : MonoBehaviour {

	public enum CollType{
		none, groundDetector, carCollider
	}
	public CollType collType;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay(Collision other){
		if (collType == CollType.groundDetector) {
			if (other.gameObject.tag == "Ground") {
				//GetComponent<CarMovement> ().grounded = true;
			}
		}
	}
}
