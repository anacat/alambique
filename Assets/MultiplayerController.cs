using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class MultiplayerController : MonoBehaviour
{
    public static MultiplayerController instance;
    
    public GameObject multiplayerGroup;
    public TextMeshProUGUI countdownText;

    public TextMeshProUGUI p1Text;
    public TextMeshProUGUI p2Text;

    [Header("Spawnpoints")]
    public Transform p1Spawn;
    public Transform p2Spawn;

    private int _nPlayers;
    private bool _gameStarted;
    
    private PlayerController _p1;
    private PlayerController _p2;

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        _nPlayers += 1;

        if(_nPlayers== 1)
        {
            p1Text.text = "Player 1 ready!";
            playerInput.transform.position = p1Spawn.position;

            _p1 = playerInput.GetComponent<PlayerController>();
        }
        else if(_nPlayers == 2)
        {
            p2Text.text = "Player 2 ready!";
            playerInput.transform.position = p2Spawn.position;

            _p1 = playerInput.GetComponent<PlayerController>();

            StartCoroutine(StartGameCountDown());
        }
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

        _p1.CanMove = true;
        _p2.CanMove = true;
    }

    public PlayerController Player1() => _p1;
    public PlayerController Player2() => _p2;
}
