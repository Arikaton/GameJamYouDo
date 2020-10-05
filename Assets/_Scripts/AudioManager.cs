using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager main;
    
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip failSound;
    [SerializeField] private AudioClip dialogSound;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySuccessSound()
    {
        PlayOneShot(successSound);
    }

    public void PlayFailSound()
    {
        PlayOneShot(failSound);
    }

    public void PlayeDialogSound()
    {
        PlayOneShot(dialogSound);
    }

    void PlayOneShot(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
}
