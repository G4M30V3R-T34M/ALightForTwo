using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GoodAlienMain))]
[RequireComponent(typeof(GA_AnimationController))]
public class GA_Shout : MonoBehaviour
{
    [SerializeField] GameObject shout;
    GoodAlienMain alien;

    float coolDown;
    Coroutine stopShoutCoroutine;
    GA_AnimationController animatorController;

    void Awake() {
        alien = GetComponent<GoodAlienMain>();
        coolDown = alien.Alien.shoutInitialCooldown;
        animatorController = GetComponent<GA_AnimationController>();
        shout.SetActive(false);
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
        animatorController.Shout();
        coolDown = alien.Alien.shoutCoolDown;
        shout.SetActive(true);
        if (stopShoutCoroutine == null) {
            stopShoutCoroutine = StartCoroutine(StopShoutCoroutine());
        }
    }

    IEnumerator StopShoutCoroutine() {
        yield return new WaitForSeconds(1f);
        shout.SetActive(false);
        stopShoutCoroutine = null;
    }

}
