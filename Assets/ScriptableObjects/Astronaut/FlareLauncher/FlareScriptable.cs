using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlareScriptable", menuName = "Scriptables/FlareScriptable", order = 6)]
public class FlareScriptable : ScriptableObject
{
    [Range(5, 10)]
    public float distanceCoefficient;

    [Range(1, 20)]
    public float velocity;

    [Range(0, 1)]
    public float distanceError;
}
