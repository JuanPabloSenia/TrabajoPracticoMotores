using UnityEngine;

public class PointerLogic : MonoBehaviour {

    public static GameObject pointerTarget;
    public GameObject player;
    public MeshRenderer a;
    public MeshRenderer b;

    bool renderOn = false;

	void Start () {
		
	}
	
	void Update () {
		if (pointerTarget != null)
        {
            if (!renderOn)
            {
                a.enabled = b.enabled = renderOn = true;
            }
            transform.LookAt(pointerTarget.transform.position);
        }
        else
        {
            if (renderOn)
            {
                a.enabled = b.enabled = renderOn = false;
            }
        }
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
	}
}
