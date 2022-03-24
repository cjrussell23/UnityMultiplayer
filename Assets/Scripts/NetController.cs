using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetController : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsServer)
        {
            BallSpawn.Instance.Respawn();
            if (gameObject.name == "Net1")
            {
                UIManager.Instance.Score(2);
                Debug.Log("Player 2 Scored");
            }
            if (gameObject.name == "Net2")
            {
                UIManager.Instance.Score(1);
                Debug.Log("Player 1 Scored");
            }
        }        
    }
}
