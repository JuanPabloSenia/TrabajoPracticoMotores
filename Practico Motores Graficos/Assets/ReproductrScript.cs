using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproductrScript : MonoBehaviour {

    public static GameObject musica;

	void Start () {
        if (musica == null)
        {
            musica = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
