using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDemon : MonoBehaviour
{
    [SerializeField] private bool left;
    [SerializeField] private GameObject demon;
    [SerializeField] private GameObject replicaPanel;

    private bool isActive = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.main.currentLocationIsLeft == left && isActive && PlayerController.main.IsPlaying)
        {
            AudioManager.main.PlayDemonSound();
            PlayerController.main.FreezePlayer();
            demon.SetActive(true);
            StartCoroutine(ShowDialogCor());
        }
    }

    IEnumerator ShowDialogCor()
    {
        isActive = false;
        yield return new WaitForSeconds(1f);
        replicaPanel.SetActive(true);
        AudioManager.main.PlayeDialogSound();
        yield return new WaitForSeconds(2f);
        UIManager.main.FadeInFadeOut();
        PlayerController.main.UnfreezePlayer();
        yield return new WaitForSeconds(10f);
        isActive = true;
        replicaPanel.SetActive(false);
        demon.SetActive(false);
        demon.GetComponent<Animator>().Rebind();
    }
    
    
}
