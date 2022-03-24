using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BallControl : NetworkBehaviour
{
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(GoBall), 2);
    }
    void Update()
    {
        
    }
    private void GoBall()
    {
        float player = Random.Range(0, 2);
        float angle = Random.Range(-15, 15);
        if (player < 1) // Left
        {
            _rb.AddForce(new Vector2(20, angle));
        }
        else // Right
        {
            _rb.AddForce(new Vector2(-20, angle));
        }
    }
}
