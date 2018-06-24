using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public bool initialized = false;

    EnemySpawner enemySpawner;

    public GameObject car;
    public float hp;
    public GameObject pSys;
    public NavMeshAgent agent;
    Rigidbody rBody;

    Vector3 lastPos;

    Vector3 active;

	void Start () {
        rBody = GetComponent<Rigidbody>();
        enemySpawner = EnemySpawner.enemySpawner;
        Spawn();
        if (!initialized) gameObject.SetActive(false);
        initialized = true;
    }

    private void Update()
    {
        rBody.velocity *= 0.99f;                    //El enemigo salia volando al chocarlo, de manera exagerada, esta linea evita eso
        if (hp < 13f && !pSys.activeSelf)           //activa sistema de particulas del fuego
            pSys.SetActive(true);
        if (hp < 0f)                                //Spawnea el siguiente auto
        {
            CanvasController.canvasController.CarDestroyed();
            car.SetActive(true);
            car.GetComponent<EnemyController>().Spawn();
            gameObject.SetActive(false);
        }
        if (Vector3.Distance(transform.position, active) < 5)
        {
            SetTargetPos(Random.Range(0, enemySpawner.enemyTargetLoc.Length));
        }
    }

    void Spawn()                                    //Al agregar un auto, se lo spawnea lo mas cerca posible del jugador
    {
        hp = 20;
        transform.position = enemySpawner.GetSpawnPoint();
        if (initialized) PointerLogic.pointerTarget = this.gameObject;
        rBody.velocity = rBody.angularVelocity = Vector3.zero;
        pSys.SetActive(false);
        agent.enabled = true;
        SetTargetPos(Random.Range(0, enemySpawner.enemyTargetLoc.Length));
    }

    void SetTargetPos(int targetIndex)              //Cuando el enemigo spawnea, llega a un destino o es chocado, cambia de objetivo, para matener la jugabilidad lo mas random posible
    {
        active = enemySpawner.enemyTargetLoc[targetIndex];
        agent.SetDestination(active);
    }

    IEnumerator setEnabled()                        //Al ser chocado, se deshabilita el pathfinding por la duracion de esta corrutina
    {
        yield return new WaitForSeconds(2f);
        agent.enabled = true;
        if (!agent.hasPath) SetTargetPos(Random.Range(0, enemySpawner.enemyTargetLoc.Length));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            agent.enabled = false;
            hp -= other.impulse.magnitude/1.3f;
            StartCoroutine(setEnabled());
        }
    }
}
