using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public void EndAnim()
    {
        UIManager.main.ShowLosePopUp();
    }
}
