using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

    public static GameObject playerGO;

	public bool grounded;
	public Rigidbody rBody;

	public int speed;
    public float maxSpeed;
	public float rotSpeed;

	float torque;

    [Header("PlayerAttributes")]

    public int maxHealth;
    public int health;

	void Start () {
        maxHealth = health = 5;
        if (playerGO == null)
            playerGO = this.gameObject;
	}
	
	void Update () {
		torque = Input.GetAxis ("Horizontal") * rotSpeed;
        grounded = Physics.Raycast(transform.position - transform.up*0.5f, -transform.up, 1.2f);
        Vector3 auxVel = transform.InverseTransformDirection(rBody.velocity);
        if (grounded)
        {
			rBody.angularVelocity = new Vector3(rBody.angularVelocity.x, torque * Mathf.Clamp(rBody.velocity.magnitude/10f, 0.5f, 1), rBody.angularVelocity.z);
            if(!Input.GetKey(KeyCode.Space))rBody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
        }
        auxVel.x /= 1.023f;
        rBody.velocity = transform.TransformDirection(auxVel);
        grounded = false;
        if (Input.GetKey(KeyCode.Space)) rBody.velocity /= 1.01f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "CollisionDmg" && other.impulse.magnitude > 45)
        {
            Debug.Log(other.impulse.magnitude);
            health--;
            CanvasController.SetHealth(health);
        }
    }
}