using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogicDestructible : MonoBehaviour {

    AsyncOperation unloadScene;

    bool isLoaded = true;
	
	void Update () {
        if (Vector3.Distance(transform.position, CarMovement.playerGO.transform.position) > 250)
        {
            if (!isLoaded)
                StartCoroutine(RespawnChunk());
        }
        else if (isLoaded)
            isLoaded = false;
	}

    IEnumerator RespawnChunk()
    {
        isLoaded = true;
        unloadScene = SceneManager.UnloadSceneAsync(gameObject.scene);
        Resources.UnloadUnusedAssets();
        StartCoroutine(CarMovement.LoadChunk(gameObject.scene.buildIndex));
        while (!unloadScene.isDone)
            yield return null;
    }
}