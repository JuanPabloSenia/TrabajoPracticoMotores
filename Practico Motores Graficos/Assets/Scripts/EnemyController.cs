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

    int done;

	void Start () {
        rBody = GetComponent<Rigidbody>();
        enemySpawner = EnemySpawner.enemySpawner;
        Spawn();
    }

    private void Update()
    {
        rBody.velocity *= 0.95f;
        if (Vector3.Distance(transform.position, active) < 5)
        {
            done = Random.Range(0, enemySpawner.enemySpawnLoc.Length);
            SetTargetPos(Random.Range(0, enemySpawner.enemyTargetLoc.Length));
        }
    }

    void Spawn()
    {
        transform.position = enemySpawner.GetSpawnPoint();
        SetTargetPos(Random.Range(0, enemySpawner.enemyTargetLoc.Length));
        PointerLogic.pointerTarget = this.gameObject;
    }

    void SetTargetPos(int targetIndex)
    {
        active = enemySpawner.enemyTargetLoc[targetIndex];
        agent.SetDestination(active);
    }

    IEnumerator setEnabled()
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
