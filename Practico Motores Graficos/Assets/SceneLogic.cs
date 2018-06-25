using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour {

    AsyncOperation unload;

	void Start () {

	}
	
	void Update () {
		
	}

    IEnumerator UnloadThisScene()
    {
        unload = SceneManager.UnloadSceneAsync(gameObject.scene);
        while (!unload.isDone)
            yield return null;
    }
}
