using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QTEButton : MonoBehaviour
{

    [SerializeField] private float startSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float growTime;
    [SerializeField] private float fadeTime = 1;
    [SerializeField] private Sprite[] sprites;

    [HideInInspector] public KeyCode keyCode;
    
    private float width = 40f;
    private float height = 40f;
    private float currentSize;
    private bool isGrowing = true;

    
    private void Start()
    {
        transform.localScale = new Vector2(startSize, startSize);
        currentSize = startSize;
        DefineRandomPos();
    }

    public void SetKeyAndDefineSprite(KeyCode key)
    {
        keyCode = key;
        GetComponent<UnityEngine.UI.Image>().sprite = sprites[DefineSpriteIndex()];
    }

    private void DefineRandomPos()
    {
        RectTransform _rectTransform = GetComponent<RectTransform>();
        RectTransform parentRect = transform.parent.GetComponent<RectTransform>();

        var newPos = new Vector2(Random.Range(0, parentRect.rect.width), Random.Range(0, parentRect.rect.width - width));
        _rectTransform.offsetMin = newPos;
        _rectTransform.sizeDelta = new Vector2(width, height);
    }

    private void Update()
    {
        if (!isGrowing) return;
        currentSize += ((maxSize - startSize) * Time.deltaTime) / growTime;
        transform.localScale = new Vector2(currentSize, currentSize);
        
        if (currentSize >= maxSize)
        {
            GameManager.main.LoseQTE();
            isGrowing = false;
        }
    }

    private int DefineSpriteIndex()
    {
        switch (keyCode)
        {
            case KeyCode.V:
                return 0;
            case KeyCode.T:
                return 1;
            case KeyCode.A:
                return 2;
            case KeyCode.L:
                return 3;
            case KeyCode.I:
                return 4;
            case KeyCode.D:
                return 5;
            case KeyCode.E:
                return 6;
            case KeyCode.B:
                return 7;
            case KeyCode.C:
                return 8;
            case KeyCode.U:
                return 9;
            case KeyCode.Q:
                return 10;
            case KeyCode.W:
                return 11;
            case KeyCode.P:
                return 12;
            default:
                return 0;
        }
    }

    public void FadeOut()
    {
        if (gameObject.activeSelf)
            StartCoroutine(FadeOutCor());
    }
    
    private IEnumerator FadeOutCor()
    {
        isGrowing = false;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        for (float i = 1; i > 0; i -= 0.01f)
        {
            canvasGroup.alpha = i;
            yield return new WaitForSeconds(fadeTime / 100);
        }
        Destroy(gameObject);
    }
}
