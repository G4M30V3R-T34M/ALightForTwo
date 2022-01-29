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
    [SerializeField] GameObject directionBar;
    [SerializeField] GameObject powerBar;

    void Start() {
        op = gameObject.GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update() {
        Vector2 flareDireciton = new Vector2(0, 0);
        float power;
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (currentState)
            {
                case LauncherState.Waiting:
                    waitingActions();
                    break;
                case LauncherState.ChosingDirection:
                    flareDireciton = ChosingDirectionActions();
                    break;
                case LauncherState.ChosingPower:
                    power = ChosingPowerActions();
                    Launch(flareDireciton, power);
                    break;
                case LauncherState.Cooldown:
                    break;
            }
        }
        
    }

    private void waitingActions() {
        currentState = LauncherState.ChosingDirection;
        directionBar.SetActive(true);
        directionBar.GetComponent<DirectionBarSelector>().ResetRotation();
    }

    private Vector2 ChosingDirectionActions() {
        Quaternion flareRotation = directionBar.transform.rotation;
        directionBar.SetActive(false);
        powerBar.SetActive(true);
        powerBar.GetComponent<PowerBarSelector>().ResetPower(flareRotation);
        currentState = LauncherState.ChosingPower;
        return new Vector2(0, 0); // TODO change
    }

    private float ChosingPowerActions() {
        float power = powerBar.transform.localScale.y;
        powerBar.SetActive(false);
        if (hasCooldown) {
            currentState = LauncherState.Cooldown;
            if (cooldownCoroutineReference == null) {
                cooldownCoroutineReference = StartCoroutine(cooldownCoroutine());
            }
        } else {
            currentState = LauncherState.Waiting;
        }
        return power;
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
        Debug.Log("Launched");
    }
}
