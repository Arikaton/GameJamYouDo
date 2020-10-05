using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create new QTE Data", fileName = "QTE Data")]
public class QTEData : ScriptableObject
{
    public KeyCode[] orderedKeys;
    public float minTimeBetweenSpawn;
    public float maxTimeBetweenSpawn;
}
