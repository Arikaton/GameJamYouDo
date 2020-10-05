using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    [SerializeField] private GameObject deathAnim;
    [SerializeField] private QTEManager qteManger;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerController _playerController;

    private void Awake()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);
    }

    public void ResetGame()
    {
        _playerController.Reset();
        uiManager.Reset();
        deathAnim.SetActive(false);
    }

    public void LoseQTE()
    {
        print("Lose QTE");
        qteManger.Reset();
        _playerController.IsPlaying = false;
        deathAnim.SetActive(true);
        uiManager.LoseQTE();
    }

    
}

public enum GameState
{
    Walking,
    Climbing,
}
