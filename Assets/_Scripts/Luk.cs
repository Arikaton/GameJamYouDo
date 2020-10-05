using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luk : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerController.main.IsPlaying)
        {
            dialogPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        dialogPanel.SetActive(false);
    }
}
