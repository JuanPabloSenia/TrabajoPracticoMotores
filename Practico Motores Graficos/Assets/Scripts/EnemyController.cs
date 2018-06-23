using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    EnemySpawner enemySpawner;

    public NavMeshAgent agent;
    Rigidbody rBody;

    Vector3 lastPos;

    Vector3 active;

	void Start () {
        rBody = GetComponent<Rigidbody>();
        enemySpawner = EnemySpawner.enemySpawner;
        Spawn();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, active) < 5)
        {
            SetTargetPos(Random.Range(0, enemySpawner.enemyTargetLoc.Length));
        }
    }

    void Spawn() //Al agregar un auto, se lo spawnea lo mas cerca posible del jugador
    {
        transform.position = enemySpawner.GetSpawnPoint();
        SetTargetPos(Random.Range(0, enemySpawner.enemyTargetLoc.Length));
        PointerLogic.pointerTarget = this.gameObject;
    }

    void SetTargetPos(int targetIndex) //Cuando el enemigo spawnea, llega a un destino o es chocado, cambia de objetivo, para matener la jugabilidad lo mas random posible
    {
        active = enemySpawner.enemyTargetLoc[targetIndex];
        agent.SetDestination(active);
    }

    IEnumerator setEnabled() //Al ser chocado, se deshabilita el pathfinding por la duracion de esta corrutina
    {
        yield return new WaitForSeconds(2f);
        agent.enabled = true;
        SetTargetPos(Random.Range(0, enemySpawner.enemyTargetLoc.Length));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            agent.enabled = false;
            StartCoroutine(setEnabled());
        }
    }
}
