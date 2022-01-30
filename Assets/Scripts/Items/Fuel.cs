using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Item
{
    [SerializeField] HUD_Manager hudManager;
    public override void DoAction(GameObject picker = null)
    {
        Debug.Log("Fuel Getted");
        hudManager.FuelAdquired();
    }
}