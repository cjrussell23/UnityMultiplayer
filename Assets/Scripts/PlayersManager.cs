using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayersManager : Singleton<PlayersManager>
{
    private NetworkVariable<int> _playersInGame = new NetworkVariable<int>();
    public static List<PlayerControl> playerControls = new List<PlayerControl>();
    private NetworkVariable<int> _player1Score = new NetworkVariable<int>();
    private NetworkVariable<int> _player2Score = new NetworkVariable<int>();

    public int PlayersInGame
    {
        get 
        { 
            return _playersInGame.Value; 
        }
    }
    public int Player1Score
    {
        get
        {
            return _player1Score.Value;
        }
    }
    public int Player2Score
    {
        get
        {
            return _player2Score.Value;
        }
    }
    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if (IsServer)
            {
                _playersInGame.Value++;
                Debug.Log($"Player {id} connected");
                
            }
        };
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (IsServer)
            {
                _playersInGame.Value--;
                Debug.Log($"Player {id} disconnected");
            }
        };
    }
    public void Score(int player)
    {
        switch (player)
        {
            case 1:
                _player1Score.Value++;
                Debug.Log("Player 1 score increased");
                break;
            case 2:
                _player2Score.Value++;
                Debug.Log("Player 2 score increased");
                break;
            default:
                Debug.Log("Invalid Player Number");
                break;
        }
    }
}
