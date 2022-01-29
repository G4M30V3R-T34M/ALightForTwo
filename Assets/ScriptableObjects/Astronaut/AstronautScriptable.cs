using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AstronautScriptable", menuName = "Scriptables/AstronautScriptable", order = 1)]
public class AstronautScriptable : ScriptableObject
{
    [Range(0, 20)]
    public float currentVelocity;

    [Range(0, 10)]
    public float pickUpTime;

    [Range(0, 10)]
    public float slowVelocity;

    [Range(0, 15)]
    public float normalVelocity;

    [Range(0, 20)]
    public float bostVelocity;
}
