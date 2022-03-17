using UnityEngine;

[CreateAssetMenu(fileName = "LifeBarScriptable", menuName = "Scriptables/LifeBarScriptable", order = 10)]
public class LifeBarScriptable : ScriptableObject
{
    [Range(0, 10)]
    public float speed;
    
    [Range(0, 1)]
    public float ammount;

    
}
