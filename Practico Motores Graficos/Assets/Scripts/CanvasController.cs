using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public static Text hpText;
    public static Image hpImage;
    public Image hpsprites;

	void Start () {
        hpText = transform.GetChild(0).GetComponent<Text>();
        hpImage = transform.GetChild(1).GetComponent<Image>();
        
	}
	
    public static void SetHealth(int hp)
    {
        hpText.text = hp.ToString();
        
        hpImage.sprite = Resources.Load<Sprite>(hp + "hp")  ;
        
    }
}
