using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEEvent : MonoBehaviour
{
    [SerializeField] private QTEData qteData;
    public void StartQTE()
    {
        if (!QTEManager.main.IsPlaying)
        {
            QTEManager.main.StartQTE(qteData);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && PlayerController.main.IsPlaying)
            StartQTE();
    }
}
