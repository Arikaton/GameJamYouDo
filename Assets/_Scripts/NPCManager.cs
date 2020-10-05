using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform valun;
    [SerializeField] private float maxHeight;

    private Vector2 startPoint;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isMoving = true;

    private void Start()
    {
        startPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (valun.position.y > transform.position.y + .4f)
        {
            animator.SetBool("Climb", true);
        }
        else
        {
            animator.SetBool("Climb", false);
        }

        if (transform.position.y - startPoint.y > maxHeight)
        {
            Reset();
        }

        if (Mathf.Abs(transform.position.x - PlayerController.main.transform.position.x) < 3)
        {
            Debug.Log("Hello Bro");
        }
    }

    private void Reset()
    {
        transform.position = startPoint;
        var valunRB = valun.GetComponent<Rigidbody2D>();
        valunRB.velocity = Vector2.zero;
        valunRB.angularVelocity = 0;
        valunRB.transform.position = startPoint + new Vector2(1, -0.4f);
    }
}
