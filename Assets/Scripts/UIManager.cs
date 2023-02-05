using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public UIController player1UI;
    public UIController player2UI;

    private void Awake()
    {
        instance = this;
    }

    public void EnableUI()
    {
        player1UI.gameObject.SetActive(true);
        player2UI.gameObject.SetActive(true);
    }
}
