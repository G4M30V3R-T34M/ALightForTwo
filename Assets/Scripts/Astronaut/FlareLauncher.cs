using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareLauncher : MonoBehaviour
{
    private enum LauncherState {Waiting, ChosingDirection, ChosingPower, Cooldown};
    private LauncherState currentState = LauncherState.Waiting;
   
    [SerializeField] private bool hasCooldown;
    [SerializeField] private float cooldownTime;
    private float remainingCooldownTime;
    private Coroutine cooldownCoroutineReference;

    private ObjectPool op;
    void Start() {
        op = gameObject.GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("FlareLaunch") > 0)
        {
            switch (currentState)
            {
                case LauncherState.Waiting:
                    waitingActions();
                    break;
                case LauncherState.ChosingDirection:
                    ChosingDirectionActions();
                    break;
                case LauncherState.ChosingPower:
                    ChosingPowerActions();
                    break;
                case LauncherState.Cooldown:
                    break;
            }
        }
        
    }

    private void waitingActions() {
        currentState = LauncherState.ChosingDirection;
    }

    private void ChosingDirectionActions() {
        currentState = LauncherState.ChosingPower;
    }

    private void ChosingPowerActions() {
        // get power value from game object

        //Launch(new Vector2(dirX, dirY), power);
        if (hasCooldown) {
            currentState = LauncherState.Cooldown;
            if (cooldownCoroutineReference == null) {
                cooldownCoroutineReference = StartCoroutine(cooldownCoroutine());
            }
        } else {
            currentState = LauncherState.Waiting;
        }
    }

    private IEnumerator cooldownCoroutine() {
        remainingCooldownTime = cooldownTime;
        while (remainingCooldownTime > 0) {
            remainingCooldownTime -= Time.deltaTime;
            yield return null;
        }
        currentState = LauncherState.Waiting;
        cooldownCoroutineReference = null;
    }

    public float GetRemainingCooldownTime() {
        return remainingCooldownTime;
    }

    public void Launch(Vector2 direction, float power) {

    }
}