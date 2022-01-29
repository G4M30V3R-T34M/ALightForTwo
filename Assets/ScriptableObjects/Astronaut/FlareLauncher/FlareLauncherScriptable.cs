using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlareLauncherScriptable", menuName = "Scriptables/FlareLauncherScriptable", order = 2)]

public class FlareLauncherScriptable : ScriptableObject
{
    public bool hasCooldown;

    [Range(0, 10)]
    public float cooldownTime;
}
