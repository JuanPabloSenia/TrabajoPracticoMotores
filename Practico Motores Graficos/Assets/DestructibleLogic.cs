using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleLogic : MonoBehaviour {

    public GameObject destructibleVersion;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            destructibleVersion.SetActive(true);
            Destroy(gameObject);
        }
    }
}
