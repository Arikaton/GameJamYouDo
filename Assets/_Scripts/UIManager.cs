using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;
    
    [SerializeField] private GameObject qteHolder;
    [SerializeField] private GameObject losePopUp;

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

    public void LoseQTE()
    {
        qteHolder.SetActive(false);
    }

    public void ShowLosePopUp()
    {
        losePopUp.SetActive(true);
    }
}
