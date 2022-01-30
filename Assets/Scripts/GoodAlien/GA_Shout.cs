using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GoodAlienMain))]
public class GA_Shout : MonoBehaviour
{
    GoodAlienMain alien;

    float coolDown;

    void Awake() {
        alien = GetComponent<GoodAlienMain>();
        coolDown = alien.Alien.shoutInitialCooldown;
    }

    void Update() {
        if (coolDown > 0) {
            coolDown -= Time.deltaTime;
        }

        if (alien.isFree && coolDown <= 0f) {
            if (Input.GetMouseButtonDown(1)) {
                DoShout();
            }
        }
    }

    private void DoShout() {
        coolDown = alien.Alien.shoutCoolDown;
    }

}
