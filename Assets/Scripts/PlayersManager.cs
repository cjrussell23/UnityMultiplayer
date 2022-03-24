using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayersManager : Singleton<PlayersManager>
{
    private NetworkVariable<int> _playersInGame = new NetworkVariable<int>();
    public static List<PlayerControl> playerControls = new List<PlayerControl>();
    
    public int PlayersInGame
    {
        get 
        { 
            return _playersInGame.Value; 
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
}
