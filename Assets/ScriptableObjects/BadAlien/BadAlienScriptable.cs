using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BadAlienScriptable", menuName = "Scriptables/BadAlienScriptable", order = 2)]
public class BadAlienScriptable : ScriptableObject
{
    [Range(1, 10)]
    public int health;

    [Range(0, 10)]
    public float speed;

    [Range(1, 10)]
    public int attack;

    [Range(0.1f, 2)]
    public float lightFetchTime;

    [Range(0.1f, 2f)]
    public float interactionDistance;

    [Range(0, 20)]
    public float astronautDetectionDistance;
}
