using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurtainScript : MonoBehaviour
{

    private float time;
    public Color initColor, finalColor;
    public Image Curtain;
    public float TotalTime;
    public bool FadeIn;
    public bool exit;

    // Start is called before the first frame update
    public void Start()
    {
        if(FadeIn)
        {
            time = 0f;
            Curtain.raycastTarget = FadeIn;
            StartCoroutine(LerpColor());
        }
    }

    public void OnClick()
    {
        time = 0f;
        Curtain.raycastTarget = !FadeIn;
        StartCoroutine(LerpColor());
    }
        
    private IEnumerator LerpColor()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            time += Time.deltaTime;

            Curtain.color = Color.Lerp(initColor, finalColor, time / TotalTime);

            if (time > TotalTime)
            {
                Curtain.raycastTarget = !FadeIn;
                if(!FadeIn)
                {
                    if(exit)
                    {
                        Application.Quit();
                    }
                }
                yield break;
            }

        }

    }
}
