using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DontGoAloneScriptable", menuName = "Scriptables/DontGoAloneScriptable", order = 0)]
public class DontGoAloneScriptable : ScriptableObject
{
    [Range(0, 2)]
    public float interactionDistance;

    [Range(1, 10)]
    public float speed;
}
