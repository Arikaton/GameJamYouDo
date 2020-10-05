using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController main;
    
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D collider;
    [SerializeField] private Transform valun;
    
    private Rigidbody2D rb;
    private AudioSource _audioSource;
    private bool pushValun = false;

    private bool isPlaying = false;
    private bool isWalking = false;
    private bool autoPlayActive = false;

    public void FreezePlayer()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        isPlaying = false;
        animator.Rebind();
    }

    public void UnfreezePlayer()
    {
        rb.isKinematic = false;
        isPlaying = true;
    }
    
    private void Awake()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);
    }

    public bool IsPlaying
    {
        get => isPlaying;
        set => isPlaying = value;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void StartAutoPlay()
    {
        autoPlayActive = true;
    }

    public void StopAutoPlay()
    {
        autoPlayActive = false;
    }

    void Update()
    {
        if (!isPlaying) return;
        if (autoPlayActive)
        {
            rb.velocity = new Vector2(GameManager.main.currentLocationIsLeft ? speed : -speed, rb.velocity.y);
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float newPlayerSpeed = horizontal * speed;
        if (Mathf.Abs(newPlayerSpeed) > 0.1f && !isWalking)
        {
            isWalking = true;
            _audioSource.Play();
        } else if (Mathf.Abs(newPlayerSpeed) <= 0.1f && isWalking)
        {
            isWalking = false;
            _audioSource.Stop();
        }
        
        rb.velocity = new Vector2(newPlayerSpeed, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(newPlayerSpeed));
        
        Flip(newPlayerSpeed);

        if (pushValun)
        {
            if (valun.position.y > transform.position.y + .4f)
            {
                animator.SetBool("Climb", true);
                if (Mathf.Abs(newPlayerSpeed) <= 0.5f)
                {
                    animator.SetBool("Climb", false);
                    GameManager.main.LoseQTE();
                }
            }
            else
            {
                animator.SetBool("Climb", false);
            }
        }
    }

    public void Reset()
    {
        isPlaying = true;
        GetComponent<CheckPointController>().Reset();
        valun.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        valun.GetComponent<Rigidbody2D>().angularVelocity = 0;
        valun.transform.position = (Vector2)transform.position + new Vector2(GameManager.main.currentLocationIsLeft ? 3 : -3, -0.4f);
        animator.Rebind();
    }

    private void Flip(float newPlayerSpeed)
    {
        if (newPlayerSpeed != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(newPlayerSpeed), transform.localScale.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Enter trigger " + other.gameObject.layer  + " " + LayerMask.NameToLayer("Valun"));
        if (other.gameObject.layer == LayerMask.NameToLayer("Valun"))
        {
            print("Enter Valun");
            animator.SetBool("Push", true);
            pushValun = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Valun"))
        {
            animator.SetBool("Push", false);
            pushValun = false;
        }
    }
}
