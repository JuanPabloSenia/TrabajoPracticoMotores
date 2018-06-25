using UnityEngine;
using UnityEngine.SceneManagement;
/*
    Script base de los enemigos: Se encarca de buscar el punto de spawn mas cercano fuera del area de vision del
    jugador, poner el enemigo allí y darle una posicion de destino.

*/
public class EnemySpawner : MonoBehaviour {

    Scene[] scenes = new Scene[9];

    public static EnemySpawner enemySpawner;//Singleton de la clase

    //Array de puntos objetivos
    public Transform targetHolder;
    public Vector3[] enemyTargetLoc;

    //Array de puntos de spawn
    public Transform spawnHolder;
    public Vector3[] enemySpawnLoc;


    void Awake () {
        for (int i = 0; i < 9; i++)
        {
            SceneManager.LoadScene(i+2, LoadSceneMode.Additive);
        }
        if (enemySpawner == null)
            enemySpawner = this;

        //Agrega los puntos de spawn y de destino (Son childs de "targetHolder" y "spawnHolder") a sus respectivos arrays

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
	
    //Devuelve un punto de spawn
	public Vector3 GetSpawnPoint () {
        Vector3 pPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        //Ordenamiento de los puntos de spawn según su distancia al jugador
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
        do//Omite los puntos de spawn que esten dentro del área de visión del jugador
        {
            distance = Vector3.Distance(enemySpawnLoc[auxIndex], pPos);
            auxIndex++;
        } while (distance < 80);
        return enemySpawnLoc[auxIndex - 1];
    }
}
