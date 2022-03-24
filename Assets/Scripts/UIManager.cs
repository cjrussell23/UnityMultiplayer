using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Button startServerButton;
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;
    [SerializeField] private Text playersInGameText;
    [SerializeField] private int _maxPlayers = 2;
    [SerializeField] private Text _p1ScoreText;
    [SerializeField] private Text _p2ScoreText;
    private NetworkVariable<int> p1NetworkScore = new NetworkVariable<int>();
    private NetworkVariable<int> p2NetworkScore = new NetworkVariable<int>();


    private void Awake()
    {
        Cursor.visible = true;
        p1NetworkScore.Value = 0;
        p2NetworkScore.Value = 0;
    }
    private void Update()
    {
        playersInGameText.text = $"Players in game: {PlayersManager.Instance.PlayersInGame}";
    }
    private void Start()
    {
        startServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer())
            {
                Debug.Log("Starting server...");
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
                    
                }
                else
                {
                    Debug.Log("Error: could not start client...");
                }
            }         
        });
    }
    public void Score(int player)
    {
        if (player == 1)
        {
            p1NetworkScore.Value++;
            _p1ScoreText.text = p1NetworkScore.Value.ToString();
        }
        if (player == 2)
        {
            p2NetworkScore.Value++;
            _p2ScoreText.text = p2NetworkScore.Value.ToString();
        }
    }
}
