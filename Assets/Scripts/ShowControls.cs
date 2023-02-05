using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowControls : MonoBehaviour
{

    public Image ControlsImage;
    private Color initColor  = new Vector4(1,1,1,0),
                  finalColor = new Vector4(1,1,1,1);
    private float time;
    public float MaxTime;

    public void OnPress()
    {
        //Debug.Log(ControlsImage.color);
        if(time == 0f )
        {
            if (ControlsImage.color == initColor)
            {
                StartCoroutine(LerpColor(initColor, finalColor));
            }
            else
            {
                StartCoroutine(LerpColor(finalColor, initColor));
            }
        }
    }

    private IEnumerator LerpColor(Color initColor, Color finalColor)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            time += Time.deltaTime;

            ControlsImage.color = Color.Lerp(initColor, finalColor, time / MaxTime);

            if (time > MaxTime)
            {
                time = 0f;
                yield break;
            }

        }

    }

}
