using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BallSpawn : Singleton<BallSpawn>
{
    private bool _ballSpawned = false;
    [SerializeField] private GameObject ballPrefab;
    private GameObject ball;
    void Start()
    {
        
    }
    void Update()
    {
        if (IsServer && PlayersManager.Instance.PlayersInGame == 2)
        {
            if (!_ballSpawned)
            {
                _ballSpawned = true;
                SpawnBall();
            }
            
        }
    }
    private void SpawnBall()
    {   
        ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        ball.GetComponent<NetworkObject>().Spawn();
    }
    public void Respawn()
    {
        ball.GetComponent<NetworkObject>().Despawn();
        _ballSpawned=false;
    }
}
