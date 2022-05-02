using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Item
{
    HUD_Manager hudManager;

    new protected void Awake() {
        base.Awake();
        hudManager = FindObjectOfType<HUD_Manager>();
    }

    public override void DoAction(GameObject picker = null) {
        Debug.Log("Fuel Getted");
        hudManager.FuelAdquired();
        StartCoroutine(PlaySoundAndDestroy());
    }
}