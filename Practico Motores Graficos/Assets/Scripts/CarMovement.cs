using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour {

    public static GameObject playerGO;
    public Transform leftTire;
    public Transform rightTire;

    public bool grounded;
    public bool activeCor;
	public Rigidbody rBody;
    public GameObject pSys;

	public int speed;
    public float maxSpeed;
	public float rotSpeed;

	float torque;
    
    public int maxHealth;
    public int health;

	void Start () {
        maxHealth = health = 5;
        if (playerGO == null)
            playerGO = this.gameObject;
	}
	
	void Update () {
		torque = Input.GetAxis ("Horizontal") * rotSpeed;
        grounded = Physics.Raycast(transform.position - transform.up*0.5f, -transform.up, 1.2f);//Raycast para chequear si esta tocando el suelo
        Vector3 auxVel = transform.InverseTransformDirection(rBody.velocity);
        leftTire.transform.localRotation = Quaternion.Euler(new Vector3(0, -90 + torque * 10, 0));
        rightTire.transform.localRotation = Quaternion.Euler(new Vector3(0, 90 + torque * 10, 0));
        if (grounded)                                                                       //Comportamiento del player cuando esta tocando el suelo
        {
            rBody.angularVelocity = new Vector3(rBody.angularVelocity.x, torque * Mathf.Clamp(rBody.velocity.magnitude/10f, 0.5f, 1), rBody.angularVelocity.z);
            if (!Input.GetKey(KeyCode.Space))
                rBody.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
            else
                if (rBody.velocity.magnitude < 10) rBody.AddForce(transform.forward * (-speed / 1.1f) * Time.deltaTime, ForceMode.Impulse);
            activeCor = false;
        }
        auxVel.x /= 1.023f;                                                                 // Disminuye la velocidad lateral del player
        if (auxVel.z > maxSpeed) auxVel.z = maxSpeed;                                       //Impide que el player supere la velocidad maxima
        rBody.velocity = transform.TransformDirection(auxVel);
        if (!grounded && !activeCor) StartCoroutine(RespawnTimer());
        grounded = false;
        if (Input.GetKey(KeyCode.Space)) rBody.velocity /= 1.01f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "CollisionDmg" && other.impulse.magnitude > 45)           //Resta vida al chocarse con los muros muy fuerte
        {
            Debug.Log(other.impulse.magnitude);
            health--;
            if (health >= 0) CanvasController.canvasController.SetHealth(health);
            if (health <= 2)
                pSys.SetActive(true);
        }
    }

    IEnumerator RespawnTimer()                                                              //chequea si el jugador no esta tocando el suelo por 2 segundos
    {
        activeCor = true;
        yield return new WaitForSeconds(2);
        if (activeCor) Respawn();
    }

    void Respawn()                                                                          //Resetea la rotacion y velocidad del player
    {
        rBody.velocity = rBody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}