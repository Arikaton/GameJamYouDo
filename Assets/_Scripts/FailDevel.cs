using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailDevel : MonoBehaviour
{
    [SerializeField] private GameObject devel;
    [SerializeField] private Animator develAnimator;
    [SerializeField] private GameObject develDialog;

    private bool wasActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerController.main.IsPlaying && !wasActive)
        {
            AudioManager.main.PlayDemonSound();
            devel.SetActive(true);
            StartCoroutine(StartPlayerFail());
            wasActive = true;
        }
    }

    IEnumerator StartPlayerFail()
    {
        PlayerController.main.StartAutoPlay();
        QTEManager.main.Reset();
        develAnimator.SetBool("HandsUp", true);
        yield return new WaitForSeconds(1f);
        develDialog.SetActive(true);
        AudioManager.main.PlayeDialogSound();
        yield return new WaitForSeconds(2f);
        PlayerController.main.StopAutoPlay();
        GameManager.main.LoseQTE();
        devel.SetActive(false);
    }
}
