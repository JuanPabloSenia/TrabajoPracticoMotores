using UnityEngine;

public class TireLogic : MonoBehaviour {

    public GameObject tire;
    Vector3 targetPos;

	void Start () {
	}
	
	void Update () {
        Debug.Log(Time.fixedUnscaledDeltaTime);
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.right, out hit);
        float hitDist = Mathf.Clamp01(hit.distance);
        targetPos = transform.position - transform.right * hitDist + transform.right * 0.5f;
        tire.transform.position = Vector3.Lerp(tire.transform.position, targetPos, 13 * Time.deltaTime);
	}
}