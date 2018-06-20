using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    EnemySpawner enemySpawner;

    public NavMeshAgent agent;

    public GameObject destroyedCar;

    Vector3 active;

    int done;

	void Start () {
        enemySpawner = EnemySpawner.enemySpawner;
        Spawn();
    }

    private void Update()
    {
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
        yield return new WaitForSeconds(1.6f);
        agent.enabled = true;
        SetTargetPos(Random.Range(0, enemySpawner.enemyTargetLoc.Length));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            agent.enabled = false;
            GameObject aux = Instantiate(destroyedCar, transform.position, transform.rotation);
            aux.transform.parent = transform.parent;
            Destroy(gameObject);
            //StartCoroutine(setEnabled());
        }
    }
}
