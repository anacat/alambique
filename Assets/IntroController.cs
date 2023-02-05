using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public List<CanvasGroup> texts = new List<CanvasGroup>();

    private IEnumerator Start()
    {
        float time = 1f;
        float timer = 0f;
        float percentage = 0f;

        for(int i = 0; i < texts.Count; i++)
        {
            timer = 0f;
            percentage = 0f;

            while (percentage < 1f)
            {
                timer += Time.deltaTime;
                percentage = timer / time;

                texts[i].alpha = Mathf.Lerp(0f, 1f, percentage);

                yield return null;
            }

            yield return new WaitForSeconds(3f);
            
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

        SceneManager.LoadScene(1);

    }
}
