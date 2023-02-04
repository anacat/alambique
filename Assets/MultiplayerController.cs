using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class MultiplayerController : MonoBehaviour
{
    public GameObject multiplayerGroup;
    public TextMeshProUGUI countdownText;

    public TextMeshProUGUI p1Text;
    public TextMeshProUGUI p2Text;

    private int _nPlayers;

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        _nPlayers += 1;

        if(_nPlayers== 1)
        {
            p1Text.text = "Player 1 ready!";
        }
        else if(_nPlayers == 2)
        {
            p2Text.text = "Player 2 ready!";

            StartCoroutine(StartGameCountDown());
        }

        Debug.Log(_nPlayers);
    } 

    private void OnPlayerLeft(PlayerInput playerInput)
    {
        _nPlayers -= 1;
    }

    private IEnumerator StartGameCountDown()
    {
        float percentage = 0f;
        float time = 1f;
        float timer = 0f;

        yield return new WaitForSeconds(0.1f);

        multiplayerGroup.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.gameObject.SetActive(false);
    }
}
