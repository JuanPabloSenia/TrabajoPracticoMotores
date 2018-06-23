using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour {

    public static GameObject playerGO;
    public Transform leftTire;
    public Transform rightTire;

    public bool grounded;
    public bool activeCor;
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
        leftTire.transform.localRotation = Quaternion.Euler(new Vector3(0, -90 + torque * 10, 0));
        rightTire.transform.localRotation = Quaternion.Euler(new Vector3(0, 90 + torque * 10, 0));
        if (grounded)
        {
            rBody.angularVelocity = new Vector3(rBody.angularVelocity.x, torque * Mathf.Clamp(rBody.velocity.magnitude/10f, 0.5f, 1), rBody.angularVelocity.z);
            if (!Input.GetKey(KeyCode.Space))
                rBody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
            else
                if (rBody.velocity.magnitude < 10) rBody.AddForce(transform.forward * (-speed / 1.1f) * Time.deltaTime, ForceMode.Impulse);
            activeCor = false;
        }
        auxVel.x /= 1.023f;
        if (auxVel.z > maxSpeed) auxVel.z = maxSpeed;
        rBody.velocity = transform.TransformDirection(auxVel);
        if (!grounded && !activeCor) StartCoroutine(RespawnTimer());
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

    IEnumerator RespawnTimer()
    {
        activeCor = true;
        yield return new WaitForSeconds(2);
        if (activeCor) Respawn();
    }

    void Respawn()
    {
        rBody.velocity = rBody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}