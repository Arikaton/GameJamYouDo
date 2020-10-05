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

    private void Awake()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);
    }

    public void Reset()
    {
        qteHolder.SetActive(true);
        losePopUp.SetActive(false);
    }
    
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
            Application.Quit();
        }
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
