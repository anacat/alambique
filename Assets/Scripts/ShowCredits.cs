using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowCredits : MonoBehaviour
{

    public TextMeshProUGUI Text;
    public Color initColor, finalColor;
    public float MaxTime;
    private float time;

    public void ShowText()
    {
        if( time == 0f )
        {
            if (Text.color == initColor)
            {
                StartCoroutine(LerpColor(initColor, finalColor));
            }
            else
            {
                StartCoroutine(LerpColor(finalColor, initColor));
            }
        }
    }

    private IEnumerator LerpColor( Color initColor, Color finalColor)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            time += Time.deltaTime;

            Text.color = Color.Lerp(initColor, finalColor, time / MaxTime);

            if (time > MaxTime)
            {
                time = 0f;
                yield break;
            }

        }

    }
}
