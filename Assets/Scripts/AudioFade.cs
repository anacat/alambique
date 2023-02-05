using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioFade : MonoBehaviour
{
    public AudioSource TheSource;
    private bool OnStart;
    public float MaxTime;
    private float VolI, VolF;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        OnStart = true;
        StartCoroutine(LerpSound());
    }

    public void OnClick()
    {
        StartCoroutine(LerpSound());
    }

    private IEnumerator LerpSound()
    {
        while (true)
        {

            if (OnStart)
            {
                VolI = TheSource.volume;
                VolF = 1f;
            }
            else
            {
                VolI = 1f;
                VolF = 0f;
            }   

            yield return new WaitForEndOfFrame();

            time += Time.deltaTime;

            TheSource.volume = VolI + (VolF-VolI) * Mathf.Pow(time/MaxTime,1);

            if (time > MaxTime)
            {
                time = 0f;
                OnStart = false;
                yield break;
            }

        }

    }

}
