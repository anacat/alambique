using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioFade : MonoBehaviour
{

    public bool OnStart;
    public float MaxTime;
    public float VolI, VolF;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        if(OnStart)
            StartCoroutine(LerpSound(VolI, VolF));
    }

    void OnClick()
    {
        StartCoroutine(LerpSound(VolI, VolF));
    }


    private IEnumerator LerpSound(float VolI, float VolF)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            time += Time.deltaTime;

            

            if (time > MaxTime)
            {
                time = 0f;
                yield break;
            }

        }

    }

}
