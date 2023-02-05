using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class MultiplayerController : MonoBehaviour
{
    public static MultiplayerController instance;
    
    public int numberOfPlayers;
    public GameObject multiplayerGroup;
    public TextMeshProUGUI countdownText;

    public TextMeshProUGUI p1Text;
    public TextMeshProUGUI p2Text;

    [Header("Spawnpoints")]
    public Transform p1Spawn;
    public Transform p2Spawn;

    public Color p1Color;
    public Color p2Color;

    private int _nPlayers;
    private bool _gameStarted;
    
    private PlayerController _p1;
    private PlayerController _p2;
    //[SerializeField]
    //private GameObject _p1BarrelBar;
    //[SerializeField]
    //private GameObject _p2BarrelBar;
    [SerializeField]
    private Animator _p1GageAnimator;
    [SerializeField]
    private Animator _p2GageAnimator;
    [SerializeField]
    private Transform _p1Barrel;
    [SerializeField]
    private Transform _p2Barrel;

    [SerializeField]
    private BeerCounter counter1;
    [SerializeField]
    private BeerCounter counter2;


    [SerializeField]
    RootSpawner _rootSpawner;
    
    private PlayerInputManager _inputManager;

    private void Awake()
    {
        instance = this;
        _inputManager = GetComponent<PlayerInputManager>();
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        _nPlayers += 1;

        if(_nPlayers == 1)
        {
            p1Text.text = "Player 1 ready!";
            playerInput.transform.position = p1Spawn.position;

            _p1 = playerInput.GetComponent<PlayerController>();
            _p1.uiController = UIManager.instance.player1UI;
            _p1.SetColor(p1Color);
            _p2.uiController.counterbeer = counter1;

            playerInput.GetComponent<BarrelManager>().gageAnimator = _p1GageAnimator;
            playerInput.GetComponent<PickUpController>().ownedBarrel = _p1Barrel;
        }
        else if(_nPlayers == 2 && _p1 != playerInput.GetComponent<PlayerController>())
        {
            p2Text.text = "Player 2 ready!";
            playerInput.transform.position = p2Spawn.position;

            _p2 = playerInput.GetComponent<PlayerController>();
            _p2.uiController = UIManager.instance.player2UI;
            _p2.uiController.counterbeer = counter2;
            _p2.SetColor(p2Color);

            playerInput.GetComponent<BarrelManager>().gageAnimator = _p2GageAnimator;
            playerInput.GetComponent<PickUpController>().ownedBarrel = _p2Barrel;
        }

        if(_nPlayers == numberOfPlayers)
        {
            _inputManager.DisableJoining();

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

        if(_p2 != null) 
        {
            _p2.CanMove = true;
        }

        UIManager.instance.EnableUI();

        _rootSpawner.StartSpawner();
    }

    public PlayerController Player1() => _p1;
    public PlayerController Player2() => _p2;
}
