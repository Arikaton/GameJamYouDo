using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform valun;
    [SerializeField] private float maxHeight;
    [SerializeField] private UnityEngine.UI.Text replicaText;
    [SerializeField] private string replica;
    [SerializeField] private GameObject replicaPanel;

    private Vector2 startPoint;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isMoving = true;

    private bool textIsShowing = false;
    private bool stateUpdated = true;

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
            if (stateUpdated)
                StartDialog();
        }
        else
        {
            stateUpdated = true;
            StopDialog();
        }
    }

    IEnumerator AutoStopDialog()
    {
        yield return new WaitForSeconds(6f);
        StopDialog();
    }

    private void StopDialog()
    {
        if (textIsShowing)
        {
            replicaPanel.SetActive(false);
            textIsShowing = false;
        }
    }

    private void StartDialog()
    {
        if (!textIsShowing)
        {
            StartCoroutine(AutoStopDialog());
            replicaPanel.SetActive(true);
            replicaText.text = replica;
            textIsShowing = true;
            stateUpdated = false;
        }
    }

    private void Reset()
    {
        transform.position = startPoint;
        var valunRB = valun.GetComponent<Rigidbody2D>();
        valunRB.velocity = Vector2.zero;
        valunRB.angularVelocity = 0;
        valunRB.transform.position = startPoint + new Vector2(rb.velocity.x > 0 ? 1 : -1, -0.4f);
    }
}
