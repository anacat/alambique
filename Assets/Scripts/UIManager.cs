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
}
