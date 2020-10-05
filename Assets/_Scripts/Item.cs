using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private GameObject interactiveDialog;

    private bool isActive = false;

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ActionAfterInteract();
            }
        }
    }

    public virtual void ActionAfterInteract()
    {
        UIManager.main.AddItem(_itemType);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerController.main.IsPlaying)
        {
            interactiveDialog.SetActive(true);
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactiveDialog.SetActive(false);
            isActive = false;
        }
    }
}

public enum ItemType
{
    Key,
    Paper
}
