using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private NetworkVariable<float> _pos = new NetworkVariable<float>();
    [SerializeField] private float _oldPos;
    [SerializeField] private Rigidbody2D _rb;
    
    private void Awake()
    {
        PlayersManager.playerControls.Add(this);
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (this.Equals(PlayersManager.playerControls[0]))
        {
            transform.position = new Vector2(-7, 0);
        }
        else
        {
            transform.position = new Vector2(7, 0);
        }
        
    }
    private void Update()
    {
        if (IsServer)
        {
            UpdateServer();
        }
        if (IsClient && IsOwner)
        {
            UpdateClient();
        }
    }
    private void UpdateServer()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + _pos.Value);
        _rb.velocity = Vector2.zero;
    }
    private void UpdateClient()
    {
        float cyPos = 0;
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            cyPos += _speed;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            cyPos -= _speed;
        }
        if (_oldPos != cyPos)
        {
            _oldPos = cyPos;
            UpdateClientPositionServerRpc(cyPos);
        }
        UpdateClientPositionServerRpc(cyPos);
    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(float position)
    {
        _pos.Value = position;
    }
}
