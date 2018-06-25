using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public bool braking;

	public float torque;
    
    public int maxHealth;
    public int health;

    public GameObject InGamecv;
    public GameObject GameOvercv;
    public Text ScorePoints;

    public Sprite[] sprites;
    public Image noiseImage;
    float sprInt;

    void Start () {
        maxHealth = health = 5;
        if (playerGO == null)
            playerGO = this.gameObject;
	}
	
	void Update () {
#if UNITY_EDITOR
        torque = Input.GetAxis ("Horizontal") * rotSpeed;
        braking = Input.GetKey(KeyCode.Space);
#endif
        grounded = Physics.Raycast(transform.position - transform.up*0.5f, -transform.up, 1.2f);//Raycast para chequear si esta tocando el suelo
        Vector3 auxVel = transform.InverseTransformDirection(rBody.velocity);
        leftTire.transform.localRotation = Quaternion.Euler(new Vector3(0, -90 + torque * 10, 0));
        rightTire.transform.localRotation = Quaternion.Euler(new Vector3(0, 90 + torque * 10, 0));
        if (grounded)                                                                       //Comportamiento del player cuando esta tocando el suelo
        {
            rBody.angularVelocity = new Vector3(rBody.angularVelocity.x, torque * Mathf.Clamp(rBody.velocity.magnitude/10f, 0.5f, 1), rBody.angularVelocity.z);
            if (!braking)
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
        //Ruido estatico al alejarse de la zona de juego
        if (sprInt < 3)
            sprInt+=0.3f;
        else
            sprInt = 0;
        noiseImage.sprite = sprites[Mathf.FloorToInt(sprInt)];

        float lerpValue = 0;
        if (transform.position.x < -260f)
            lerpValue = (transform.position.x + 260) * -0.007f;
        else if (transform.position.x > 540f)
            lerpValue = (transform.position.x - 540) * 0.007f;
        else if (transform.position.z < 370f)
            lerpValue = (transform.position.z + 370) * -0.007f;
        else if (transform.position.z > 350f)
            lerpValue = (transform.position.z - 350) * 0.007f;
        if (lerpValue != 0)
        {
            noiseImage.color = Color.Lerp(Color.clear, Color.white, lerpValue);
            if (lerpValue >= 1)
            {
                transform.position = new Vector3(0, 2, 0);
                rBody.velocity = rBody.angularVelocity = Vector3.zero;
            }
        }
        
        //final timer
        if (InGamecv.GetComponent<CanvasController>().timer <= 0) menuGameOver();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "CollisionDmg" && other.impulse.magnitude > 45)           //Resta vida al chocarse con los muros muy fuerte
        {
            health--;
            if (health >= 1) CanvasController.canvasController.SetHealth(health);
            if (health <= 2)
                pSys.SetActive(true);
            if (health == 0) menuGameOver();
        }
    }

    public static IEnumerator LoadChunk(int index)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

        while (!asyncOp.isDone)
            yield return null;
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

    public void menuGameOver()                                                               //Cambia el canvas por el GameOver
    {
        ScorePoints.text = InGamecv.GetComponent<CanvasController>().enemigosDestruidos.ToString("00");
        InGamecv.SetActive(false);
        GameOvercv.SetActive(true);
        rBody.isKinematic = true;
    }
}