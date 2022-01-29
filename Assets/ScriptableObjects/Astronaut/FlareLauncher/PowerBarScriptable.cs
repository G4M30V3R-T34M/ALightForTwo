using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerBarScriptable", menuName = "Scriptables/PowerBarScriptable", order = 5)]

public class PowerBarScriptable : ScriptableObject
{
    public float powerBarSpeed;

    [Range(1, 2)]
    public float maxLength;

    public float minLength;
}
