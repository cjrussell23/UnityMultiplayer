using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BallControl : NetworkBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private AudioSource _audioSource;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
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
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Debug.Log(coll.collider.name);
            _audioSource.Play();
            Vector2 vel;
            vel.x = _rb.velocity.x;
            ContactPoint2D contact = coll.GetContact(0);
            Vector3 pos = contact.point - coll.collider.attachedRigidbody.position;
            if (_rb.velocity.y > 0) // Make y directions match
            {
                pos.y = Mathf.Abs(pos.y);
            }
            else
            {
                pos.y = -Mathf.Abs(pos.y);
            }
            if (Mathf.Abs(vel.x) < 2f) // Make sure ball cant get too slow, makes game boring.
            {
                if (vel.x > 0)
                {
                    vel.x = 2f;
                }
                else
                {
                    vel.x = -2f;
                }
            }
            vel.y = (_rb.velocity.y / 2) + (10 * pos.y);
            // Set a cap for velocity
            if (vel.x > 6f)
            {
                vel.x = 6f;
            }
            if (vel.y > 6f)
            {
                vel.y = 6f;
            }
            _rb.velocity = vel;
        }
    }
}
