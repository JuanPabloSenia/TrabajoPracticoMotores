using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public static CanvasController canvasController;
    public Image hpImage;
    public Text enemyCount;
    public Text timerText;
    public int enemigosDestruidos = 0;
    public int timer;
    public int minutos;

    public GameObject CanvasPausa;

    CarMovement player;
    float torque;
    float lPress, rPress;
    public Image left;
    public Image right;

    private void Awake()
    {
        if (canvasController == null)
        {
            canvasController = this; // Singleton del canvas
        }
        timer = minutos*60;
        StartCoroutine(TimerUpdater());
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CarMovement>();
    }

    private void Update()
    {
        player.torque = Mathf.Lerp(player.torque, (rPress-lPress)*player.rotSpeed, 9.5f*Time.deltaTime);
        if (lPress == 1 && rPress == 1)
            player.braking = true;
        else
            player.braking = false;
    }

    public void ButtonPressed(bool leftOne)
    {
        if (leftOne)
            lPress = 1;
        else
            rPress = 1;
    }

    public void ButtonReleased(bool leftOne)
    {
        if (leftOne)
            lPress = 0;
        else
            rPress = 0;
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

    public void Pausa()
    {
        Time.timeScale = 0;
        CanvasPausa.GetComponent<CanvasGameoverController>().setScore(enemigosDestruidos);
        CanvasPausa.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void timerupdatePlay()
    {
        StartCoroutine(TimerUpdater());
    }
}
