using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DirectionBarScriptable", menuName = "Scriptables/DirectionBarScriptable", order = 3)]
public class DirectionBarScriptable : ScriptableObject
{
    [Range(50, 150)]
    public float angularVelocity;
}
