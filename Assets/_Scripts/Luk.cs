using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luk : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private UnityEngine.UI.Text text;

    private bool isActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerController.main.IsPlaying)
        {
            isActive = true;
            dialogPanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                text.text = "Press x to interact";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isActive = false;
        text.text = "This is wrong door.";
        dialogPanel.SetActive(false);
    }
}
