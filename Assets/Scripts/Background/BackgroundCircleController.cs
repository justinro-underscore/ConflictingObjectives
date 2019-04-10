using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundCircleController : MonoBehaviour
{
    private Vector3 SCALE_VECTOR = Vector3.one / 200f;

    private Color spriteColor;
    private Image spriteImg;
    private bool fadeIn = true;
    private bool fadeOut = false;

	void Start ()
    {
        spriteColor = new Color(Random.value / 4, Random.value / 4, 0.5f, 0f);
        spriteImg = gameObject.GetComponent<Image>();
        spriteImg.color = spriteColor;
    }
	
	void Update ()
    {
        if(fadeIn)
        {
            spriteColor.a += 0.001f;
            spriteImg.color = spriteColor;
            if (spriteColor.a > 0.2f)
            {
                fadeIn = false;
                Invoke("BeginFadeOut", 2f);
            }
        }
        gameObject.transform.localScale += SCALE_VECTOR;
        if(fadeOut)
        {
            spriteColor.a -= 0.001f;
            spriteImg.color = spriteColor;
            if (spriteColor.a <= 0)
                Destroy(gameObject);
        }
	}

    void BeginFadeOut()
    {
        fadeOut = true;
    }
}
