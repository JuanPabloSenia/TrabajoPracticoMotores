using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGameoverController : MonoBehaviour {
    public GameObject inGameCanvas;
	// Use this for initialization
	void Start () {
		
	}
	

    public void Restart()
    {
        SceneManager.LoadScene("GameWBuildings");
    }
    public void gotoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Continue()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        inGameCanvas.SetActive(true);
    }
}
