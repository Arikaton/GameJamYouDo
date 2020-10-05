using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;
    
    [SerializeField] private GameObject qteHolder;
    [SerializeField] private GameObject losePopUp;
    [SerializeField] private GameObject keyItem;
    [SerializeField] private GameObject paperItem;
    [SerializeField] private GameObject exitWindow;
    [SerializeField] private GameObject exitDaemon1;
    [SerializeField] private GameObject exitDaemon2;
    [SerializeField] private GameObject fadeInOutScreen;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject startAnim;
    [SerializeField] private GameObject endAnim;
    [SerializeField] private GameObject hintAnim;
    [SerializeField] private GameObject fader;
    [SerializeField] private GameObject subtitle;

    private void Awake()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
    }

    public void Reset()
    {
        qteHolder.SetActive(true);
        losePopUp.SetActive(false);
    }

    public void ShowStartAnim() => startAnim.SetActive(true);

    public void HideStartAnim() => startAnim.SetActive(false);

    public void ShowEndAnim() => endAnim.SetActive(true);

    public void ShowHintAnim() => hintAnim.SetActive(true);

    public void ShowFader() => fader.SetActive(true);

    public void HideFader()
    {
        fader.GetComponent<Animator>().Rebind();
        fader.SetActive(false);
    }

    public void HideHintAnim() => hintAnim.SetActive(false);
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!exitWindow.activeSelf)
            {
                exitWindow.SetActive(true);
            }
            else
            {
                CloseExitWindow();
            }
        }
    }

    public void CloseExitWindow()
    {
        exitDaemon1.SetActive(true);
        exitDaemon2.SetActive(false);
        exitWindow.SetActive(false);
    }

    public void ExitGame()
    {
        if (exitDaemon1.activeSelf)
        {
            exitDaemon2.SetActive(true);
            exitDaemon1.SetActive(false);
        }
        else
        {
            StartCoroutine(EndGameCor());
        }
    }

    IEnumerator EndGameCor()
    {
        GameManager.main.gameIsStarted = false;
        PlayerController.main.IsPlaying = false;
        PlayerController.main.FreezePlayer();
        ShowEndAnim();
        ShowFader();
        yield return new WaitForSeconds(4.5f);
        HideFader();
        subtitle.SetActive(true);
    }

    public void FadeInFadeOut()
    {
        StartCoroutine(FadeInOutCor());
    }

    IEnumerator FadeInOutCor()
    {
        fadeInOutScreen.SetActive(true);
        CanvasGroup alpha = fadeInOutScreen.GetComponent<CanvasGroup>();
        for (float i = 0; i < 1; i += 0.01f)
        {
            alpha.alpha = i;
            yield return new WaitForSeconds(1 / 100);
        }
        GameManager.main.ReachTop();
        yield return new WaitForSeconds(0.5f);
        for (float i = 1; i > 0; i -= 0.01f)
        {
            alpha.alpha = i;
            yield return new WaitForSeconds(1 / 100);
        }
        fadeInOutScreen.SetActive(false);
    }

    public void LoseQTE()
    {
        qteHolder.SetActive(false);
    }

    public void ShowLosePopUp()
    {
        losePopUp.SetActive(true);
    }

    public void AddItem(ItemType itemType)
    {
        if (itemType == ItemType.Key)
        {
            keyItem.SetActive(true);
        }
        else
        {
            paperItem.SetActive(true);
        }
    }
}
