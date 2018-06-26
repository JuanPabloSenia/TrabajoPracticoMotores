using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour {
    public Object SiguienteEscena;
	// Use this for initialization
	void Start () {
		
	}

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
