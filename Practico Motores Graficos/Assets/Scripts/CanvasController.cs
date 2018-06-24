using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public static CanvasController canvasController;
    public Image hpImage;
    public Text enemyCount;
    public Text timerText;
    public int DestroyedCars;
    public int enemigosDestruidos = 0;
    public int timer;

    private void Awake()
    {
        if (canvasController == null)
        {
            canvasController = this; // Singleton del canvas
        }
        timer = 240;
        StartCoroutine(TimerUpdater());
    }

    public void CarDestroyed()// Se actualiza el contador de enemigos destruidos
    {
        enemigosDestruidos++;
        enemyCount.text = enemigosDestruidos.ToString("00");
    }

    IEnumerator TimerUpdater()// Contador, se transforma el int a minutos y segundos y se los muestra en pantalla
    {
        if (timer >= 0)
        {
            string mins = Mathf.Floor(timer / 60).ToString("00");
            string secs = (timer % 60).ToString("00");
            timerText.text = mins + ":" + secs;
            yield return new WaitForSeconds(1);
            timer--;
            StartCoroutine(TimerUpdater());
        }
        else //Abre el menu de GameOver
        {
            //GameOver menu :D
        }
    }

    public void SetHealth(int hp) //Actualiza la imagen que muestra la Vida
    {
        hpImage.sprite = Resources.Load<Sprite>(hp + "hp");
    }
}
