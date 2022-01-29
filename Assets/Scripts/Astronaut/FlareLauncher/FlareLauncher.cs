using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareLauncher : MonoBehaviour
{
    private enum LauncherState {Waiting, ChosingDirection, ChosingPower, Cooldown};
    private LauncherState currentState = LauncherState.Waiting;

    [SerializeField] FlareLauncherScriptable _launcher;
    public float remainingCooldownTime { get; private set; }
    private Coroutine cooldownCoroutineReference;


    private Vector3 flareDireciton = new Vector3(0, 0, 0);
    private float power;

    private ObjectPool op;
    [SerializeField] GameObject directionBar;
    [SerializeField] GameObject powerBar;

    void Start() {
        op = gameObject.GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            switch (currentState) {
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
        Vector3 direction = directionBar.GetComponent<DirectionBarSelector>().GetNormalizedDirection();
        directionBar.SetActive(false);
        powerBar.SetActive(true);
        powerBar.GetComponent<PowerBarSelector>().ResetPower(flareRotation);
        currentState = LauncherState.ChosingPower;
        return direction;
    }

    private float ChosingPowerActions() {
        float power = powerBar.transform.localScale.y;
        powerBar.SetActive(false);
        if (_launcher.hasCooldown) {
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
        remainingCooldownTime = _launcher.cooldownTime;
        while (remainingCooldownTime > 0) {
            remainingCooldownTime -= Time.deltaTime;
            yield return null;
        }
        currentState = LauncherState.Waiting;
        cooldownCoroutineReference = null;
    }

    public void Launch(Vector3 direction, float power) {
        Flare flare = (Flare)op.getNext();
        flare.gameObject.SetActive(true);
        flare.transform.position = this.transform.position;
        flare.GetComponent<Flare>().Launch(direction, power);
    }
}
