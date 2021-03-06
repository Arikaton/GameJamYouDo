﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class QTEManager : MonoBehaviour
{
    public static QTEManager main;

    [SerializeField] private Transform leftParent;
    [SerializeField] private Transform rightParent;
    [SerializeField] private QTEButton buttonPrefab;
    [SerializeField] private KeyCode[] allowedKeys;
    Queue<QTEButton> qteButtons;

    public bool IsPlaying = false;
    bool isQTEWave = false;

    private int qteButtonCount = 0;

    private void Awake()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);
    }

    public void StartQTE(QTEData qteData)
    {
        IsPlaying = true;
        StartCoroutine(SpawnQTEButtons(qteData));
    }

    IEnumerator SpawnQTEButtons(QTEData qteData)
    {
        var parent = GameManager.main.currentLocationIsLeft ? leftParent : rightParent;
        qteButtonCount = qteData.orderedKeys.Length;
        isQTEWave = true;
        qteButtons = new Queue<QTEButton>();
        int buttonCount = qteData.orderedKeys.Length;
        for (int i = 0; i < buttonCount; i++)
        {
            yield return new WaitForSeconds(Random.Range(qteData.minTimeBetweenSpawn, qteData.maxTimeBetweenSpawn));
            QTEButton newButton = Instantiate(buttonPrefab, parent, true);
            qteButtons.Enqueue(newButton);
            newButton.SetKeyAndDefineSprite(qteData.orderedKeys[i]);
        }

        isQTEWave = false;
    }

    public void Reset()
    {
        var parent = GameManager.main.currentLocationIsLeft ? leftParent : rightParent;
        Debug.Log("Reset QTE");
        IsPlaying = false;
        isQTEWave = false;
        StopAllCoroutines();
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
    
    private void Update()
    {
        if (!IsPlaying) return;
        KeyCode pressedKey = KeyCode.Numlock;
        foreach (var keyCode in allowedKeys)
        {
            if (Input.GetKeyDown(keyCode))
            {
                pressedKey = keyCode;
                break;
            }
        }

        if (pressedKey != KeyCode.Numlock)
        {
            if (qteButtons.Count == 0)
            {
                GameManager.main.LoseQTE();
                IsPlaying = false;

            }
            else
            {
                var firstButton = qteButtons.Dequeue();
                if (firstButton.keyCode == pressedKey)
                {
                    qteButtonCount--;
                    firstButton.FadeOut();
                    AudioManager.main.PlaySuccessSound();
                    if (qteButtonCount == 0 && !isQTEWave)
                    {
                        IsPlaying = false;
                    }
                }
                else
                {
                    AudioManager.main.PlayFailSound();
                    GameManager.main.LoseQTE();
                    IsPlaying = false;
                }
            }
        }
    }
}
