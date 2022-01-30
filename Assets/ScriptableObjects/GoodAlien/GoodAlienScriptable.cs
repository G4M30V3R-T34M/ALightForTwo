using UnityEngine;

[CreateAssetMenu(fileName = "GoodAlienScriptable", menuName = "Scriptables/GoodAlienScriptable", order = 0)]
public class GoodAlienScriptable : ScriptableObject
{
    [Range(0,10)]
    public float Speed;

    [Range(0, 2)]
    public float HoldSpeedModifier;

    [Range(0, 2)]
    public float ObstacleSpeedModifier;

    [Range(1,300)]
    public int Health;

    [Range(0, 5)]
    public float interactDistance;

    [Range(0, 20)]
    public float shoutCoolDown;

    [Range(0, 20)]
    public float shoutInitialCooldown;

    [Range(0, 20)]
    public float shoutDistance;

    [Range(0, 10)]
    public int health;

    [Range(0, 5)]
    public int damageValue;

    [Range(0, 30)]
    public int HealValue;

    [Range(0, 30)]
    public int selfDamage;

    [Range(0, 5)]
    public int damageToFlare;

    [Range(0, 5)]
    public float waitBetweenHeal;
}
