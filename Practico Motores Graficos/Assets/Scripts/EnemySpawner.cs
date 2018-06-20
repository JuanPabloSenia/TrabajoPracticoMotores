using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner enemySpawner;

    public Vector3[] enemyTargetLoc;
    public Vector3[] enemySpawnLoc;

    public Transform targetHolder;
    public Transform spawnHolder;

    void Awake () {
        if (enemySpawner == null)
            enemySpawner = this;

        enemyTargetLoc = new Vector3[targetHolder.childCount];
        for (int i = 0; i < targetHolder.childCount; i++)
        {
            enemyTargetLoc[i] = targetHolder.GetChild(i).position;
        }
        enemySpawnLoc = new Vector3[spawnHolder.childCount];
        for (int i = 0; i < spawnHolder.childCount; i++)
        {
            enemySpawnLoc[i] = spawnHolder.GetChild(i).position;
        }
    }
	
	public Vector3 GetSpawnPoint () {
        Vector3 pPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        for (int i = 0; i < enemySpawnLoc.Length-1; i++)
        {
            float thisDistance = Vector3.Distance(enemySpawnLoc[i], pPos);
            int shortestIndex = i;
            for (int j = i+1; j < enemySpawnLoc.Length-i; j++)
            {
                if (thisDistance > Vector3.Distance(enemySpawnLoc[j], pPos))
                    shortestIndex = j;
            }
            Vector3 auxLoc = enemySpawnLoc[i];
            enemySpawnLoc[i] = enemySpawnLoc[shortestIndex];
            enemySpawnLoc[shortestIndex] = auxLoc;
        }
        float distance;
        int auxIndex = 0;
        do
        {
            distance = Vector3.Distance(enemySpawnLoc[auxIndex], pPos);
            auxIndex++;
        } while (distance < 50);
        return enemySpawnLoc[auxIndex - 1];
    }
}
