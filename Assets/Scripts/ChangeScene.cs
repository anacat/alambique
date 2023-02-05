using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public float WaitTime;

    // Start is called before the first frame update
    void Start()    
    {
    
    }

    public void TriggerSceneChange()
    {
        Invoke("LoadScene", WaitTime);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("BreweryScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
