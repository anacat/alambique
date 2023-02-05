using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public List<CanvasGroup> texts = new List<CanvasGroup>();
    public AudioSource[] Vozes;

    private IEnumerator Start()
    {
        float time = 1f;
        float timer = 0f;
        float percentage = 0f;
        float[] Waiting = { 1.8f, 1.5f, 0.8f, 1.1f };

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < texts.Count; i++)
        {
            timer = 0f;
            percentage = 0f;

            Vozes[i].Play();

            while (percentage < 1f)
            {
                timer += Time.deltaTime;
                percentage = timer / time;

                texts[i].alpha = Mathf.Lerp(0f, 1f, percentage);

                if(percentage >= 0.1f)
                    

                yield return null;
            }

            yield return new WaitForSeconds(Waiting[i]);
            
            timer = 0f;
            percentage = 0f;

            while (percentage < 1f)
            {
                timer += Time.deltaTime;
                percentage = timer / time;

                texts[i].alpha = Mathf.Lerp(1f, 0f, percentage);

                yield return null;
            }
        }

        SceneManager.LoadScene(2);

    }
}
