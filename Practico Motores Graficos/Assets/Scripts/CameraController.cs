using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	Vector3 ogPos;

	public Transform target;
    public Transform cam;
    public Quaternion camRotation;
	public float camSpeed;

    public bool isOnBuilding;


    void Start () {
		ogPos = transform.position;
        camRotation = cam.rotation;
    }
	
	void FixedUpdate ()
    {
        transform.position = Vector3.Lerp (transform.position, target.position + (target.forward * 10) + ogPos, camSpeed);
        if (isOnBuilding)
        {
            cam.rotation = Quaternion.Lerp(cam.rotation, Quaternion.Euler(81, 0, 0), 5 * Time.deltaTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x, cam.transform.position.y, target.transform.position.z), 5*Time.deltaTime);
        }
        else
        {
            cam.rotation = Quaternion.Lerp(cam.rotation, camRotation, 5*Time.deltaTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        isOnBuilding = false;
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CollisionDmg")
        {
            isOnBuilding = true;
        }
    }
}
