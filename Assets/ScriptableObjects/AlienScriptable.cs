using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlienScriptable", menuName = "Scriptables/AlienScriptable", order = 0)]
public class AlienScriptable : ScriptableObject
{
    [Range(0,10)]
    public float Speed;

    [Range(1,5)]
    public int Health;
}
