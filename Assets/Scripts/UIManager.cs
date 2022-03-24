using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : NetworkBehaviour
{
    [SerializeField] private Button startServerButton;
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;
    [SerializeField] private Text playersInGameText;
    [SerializeField] private int _maxPlayers = 2;
    [SerializeField] private Text _p1ScoreText;
    [SerializeField] private Text _p2ScoreText;
    
    //private int _p1Score;
    //private int _p2Score;


    private void Awake()
    {
        Cursor.visible = true;
        //_p1Score = 0;
        //_p2Score = 0;
    }
    private void Update()
    {
        playersInGameText.text = $"Players in game: {PlayersManager.Instance.PlayersInGame}";
        _p1ScoreText.text = PlayersManager.Instance.Player1Score.ToString();
        _p2ScoreText.text = PlayersManager.Instance.Player2Score.ToString();
        if (Input.GetAxis("Cancel") > 0)
        {
            Disconnect();
        }
    }
    private void Start()
    {
        startServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer())
            {
                Debug.Log("Starting server...");
                ToggleUI(false);
            }
            else
            {
                Debug.Log("Error: could not start server...");
            }
        });
        startHostButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Starting host...");
                ToggleUI(false);
            }
            else
            {
                Debug.Log("Error: could not start host...");
            }
        });
        startClientButton.onClick.AddListener(() =>
        {
            if(PlayersManager.Instance.PlayersInGame < _maxPlayers)
            {
                if (NetworkManager.Singleton.StartClient())
                {
                    Debug.Log("Starting client...");
                    ToggleUI(false);

                }
                else
                {
                    Debug.Log("Error: could not start client...");
                }
            }         
        });
    }
    private void ToggleUI(bool active)
    {
        startClientButton.gameObject.SetActive(active);
        startServerButton.gameObject.SetActive(active);
        startHostButton.gameObject.SetActive(active);
    }
    public void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();
        ToggleUI(true);
    }
}
