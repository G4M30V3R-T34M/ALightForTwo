using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{

    public override void DoAction(GameObject picker=null) {
        picker.GetComponent<HealthManager>().Heal(_item.heal);
        StartCoroutine(PlaySoundAndDestroy());
    }
}
