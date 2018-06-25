using UnityEngine;

/*
    Script atachado a todos los objetos destructibles del mapa (Basureros, Semáforos, Carteles) que activa la version destruida de cada uno
*/
public class DestructibleLogic : MonoBehaviour {

    public GameObject destructibleVersion;//Referencia al Clon

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")//Al entrar el Jugador en el trigger, cambia de objeto y luego de 3 segundos, lo destruye
        {
            destructibleVersion.SetActive(true);
            Destroy(destructibleVersion, 3);
            Destroy(gameObject);
        }
    }
}
