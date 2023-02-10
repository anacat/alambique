using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public List<CanvasGroup> texts = new List<CanvasGroup>();
    public AudioSource[] Vozes;
    private int nPresses = 0;
    private bool skipKey, Skipping = false;

    public void Update()
    {
        skipKey = Input.GetKeyDown(KeyCode.Escape);

        if( skipKey )
        {
            nPresses += 1;

            if (nPresses > 1)
            {
                Skipping = true;
            }
        }

    }

    private IEnumerator Start()
    {
        float time = 1f;
        float timer = 0f;
        float percentage = 0f;
        float[] Waiting = { 1.8f, 1.25f, 0.65f, 1.1f };

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < texts.Count; i++)
        {
            timer = 0f;
            percentage = 0f;

            Vozes[i].Play();

            while (percentage < 1f && !Skipping)
            {
                timer += Time.deltaTime;
                percentage = timer / time;

                texts[i].alpha = Mathf.Lerp(0f, 1f, percentage);

                yield return null;
            }

            if( !Skipping )
                yield return new WaitForSeconds(Waiting[i]);
            
            timer = 0f;
            percentage = 0f;

            while (percentage < 1f && !Skipping)
            {
                timer += Time.deltaTime;
                percentage = timer / time;

                if (Skipping)
                    percentage = 1f;

                texts[i].alpha = Mathf.Lerp(1f, 0f, percentage);

                yield return null;
            }

            if ( Skipping )
            {

                timer = 0f;
                percentage = 0f;
                float currAlpha = texts[i].alpha,
                      currVolum = Vozes[i].volume;

                while (percentage < 1f && !Skipping)
                {
                    timer += Time.deltaTime;
                    percentage = timer / time * 2f;

                    texts[i].alpha  = Mathf.Lerp(currAlpha, 0f, percentage);
                    Vozes[i].volume = Mathf.Lerp(currVolum, 0f, percentage);

                    yield return null;
                }

            }
            break;
        }

        SceneManager.LoadScene(2);

    }
}
