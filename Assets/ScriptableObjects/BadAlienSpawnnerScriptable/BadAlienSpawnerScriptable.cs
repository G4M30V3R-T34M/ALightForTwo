using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BadAlienSpawnerScriptable", menuName = "Scriptables/BadAlienSpawnerScriptable", order = 2)]
public class BadAlienSpawnerScriptable : ScriptableObject
{
    public float StartingWaitTime;

    [Header("Formula = n/(x+1) + n/(x+m)")]
    [ContextMenuItem("Defalt Value", "DefaultValues")]
    public float n;
    [ContextMenuItem("Defalt Value", "DefaultValues")]
    public float m;

    private void DefaultValues() {
        n = 1.5f;
        m = 2f;
    }
}
