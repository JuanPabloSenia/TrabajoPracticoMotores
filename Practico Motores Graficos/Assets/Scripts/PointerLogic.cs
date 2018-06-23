using UnityEngine;

/*
    Controlador de la flecha que apunta al enemigo
*/

public class PointerLogic : MonoBehaviour {

    public static GameObject pointerTarget;
    public GameObject player;
    public MeshRenderer a;
    public MeshRenderer b;

    bool renderOn = false;
	
	void Update () {
		if (pointerTarget != null) //Si hay un objetivo en escena, enciende los renderers de la flecha y en cada frame gira hacia el target
        {
            if (!renderOn)
            {
                a.enabled = b.enabled = renderOn = true;
            }
            transform.LookAt(pointerTarget.transform.position);
        }
        else //Si no hay un objetivo en escena, apaga los renderers de la flecha
        {
            if (renderOn)
            {
                a.enabled = b.enabled = renderOn = false;
            }
        }
        if (player == null) //Por si en algun momento se destruye al player
            player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
	}
}
