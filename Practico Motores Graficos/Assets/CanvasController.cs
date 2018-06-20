using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public static Text hpText;

	void Start () {
        hpText = transform.GetChild(0).GetComponent<Text>();
	}
	
	void Update () {
		
	}

    public static void SetHealth(int hp)
    {
        hpText.text = hp.ToString();
    }
}
