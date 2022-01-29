using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AstronautScriptable", menuName = "Scriptables/AstronautScriptable", order = 1)]
public class AstronautScriptable : ScriptableObject
{
    [Range(0, 15)]
    public float velocity;
}
