using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    [SerializeField] private GameObject deathAnim;
    [SerializeField] private GameObject blackRectangle;
    [SerializeField] private QTEManager qteManger;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform leftStart;
    [SerializeField] private Transform rightStart;

    public bool currentLocationIsLeft = true;
    public bool gameIsStarted = false;

    private void Awake()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);
    }

    public void ReachTop()
    {
        if (currentLocationIsLeft)
        {
            currentLocationIsLeft = false;
            PlayerController.main.transform.position = rightStart.position;
        }
        else
        {
            currentLocationIsLeft = true;
            PlayerController.main.transform.position = leftStart.position;
        }
        PlayerController.main.GetComponent<CheckPointController>().ResetStartPos();
        ResetGame();
    }

    public void ResetGame()
    {
        _playerController.Reset();
        uiManager.Reset();
        deathAnim.SetActive(false);
        blackRectangle.SetActive(false);
    }

    public void LoseQTE()
    {
        print("Lose QTE");
        qteManger.Reset();
        _playerController.IsPlaying = false;
        blackRectangle.SetActive(true);
        deathAnim.SetActive(true);
        if (currentLocationIsLeft)
            deathAnim.transform.localScale = new Vector2(Mathf.Abs(deathAnim.transform.localScale.x), deathAnim.transform.localScale.y);
        else
            deathAnim.transform.localScale = new Vector2(Mathf.Abs(deathAnim.transform.localScale.x) * -1, deathAnim.transform.localScale.y);

        uiManager.LoseQTE();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCor());
    }

    IEnumerator StartGameCor()
    {
        uiManager.ShowStartAnim();
        uiManager.ShowFader();
        yield return new WaitForSeconds(4.5f);
        uiManager.HideStartAnim();
        uiManager.HideFader();
        PlayerController.main.IsPlaying = true;
        uiManager.StartGame();
        gameIsStarted = true;
        yield return new WaitForSeconds(1);
        uiManager.ShowHintAnim();
    }
}

public enum GameState
{
    Walking,
    Climbing,
}
