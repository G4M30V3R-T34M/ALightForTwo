using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptable", menuName = "Scriptables/ItemScriptable", order = 10)]
public class ItemScriptable : ScriptableObject
{
    [Range(1, 50)]
    public int heal;

}
