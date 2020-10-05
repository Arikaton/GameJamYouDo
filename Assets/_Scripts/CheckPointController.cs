using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    private Vector2 lastCheckpoint;

    private void Start()
    {
        ResetStartPos();
    }

    public void ResetStartPos()
    {
        lastCheckpoint = transform.position;
    }

    public void SetCheckPoint(Transform checkTransform)
    {
        lastCheckpoint = checkTransform.position;
    }

    public void Reset()
    {
        transform.position = lastCheckpoint;
    }
}
