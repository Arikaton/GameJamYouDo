using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valun : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource _audioSource;

    private bool isMoving = false;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f && !isMoving)
        {
            isMoving = true;
            _audioSource.Play();
        } else if (Mathf.Abs(rb.velocity.x) <= 0.1f && isMoving)
        {
            isMoving = false;
            _audioSource.Stop();
        }
    }
}
