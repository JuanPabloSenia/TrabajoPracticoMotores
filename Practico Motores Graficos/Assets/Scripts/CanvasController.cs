using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public static CanvasController canvasController;
    public Image hpImage;
    public Text enemyCount;
    public int DestroyedCars;
    public int enemigosDestruidos = 0;

    private void Awake()
    {
        if (canvasController == null)
        {
            canvasController = this;
        }
    }

    void Start ()
    {
        

	}
    private void Update()
    {
        if (enemigosDestruidos<=9)
        {
            enemyCount.text = "0" + enemigosDestruidos.ToString();
        }
        else
        {
            enemyCount.text = enemigosDestruidos.ToString();
        }
        
    }

    public void SetHealth(int hp)
    {
        
        hpImage.sprite = Resources.Load<Sprite>(hp + "hp");
    }
}
