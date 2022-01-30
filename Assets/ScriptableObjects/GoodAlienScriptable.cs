using UnityEngine;

[CreateAssetMenu(fileName = "GoodAlienScriptable", menuName = "Scriptables/GoodAlienScriptable", order = 0)]
public class GoodAlienScriptable : ScriptableObject
{
    [Range(0,10)]
    public float Speed;

    [Range(1,5)]
    public int Health;

    [Range(0, 5)]
    public float interactDistance;

    [Range(0, 20)]
    public float shoutCoolDown;

    [Range(0, 20)]
    public float shoutInitialCooldown;

    [Range(0, 20)]
    public float shoutDistance;
}
