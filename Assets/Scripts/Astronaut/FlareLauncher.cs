using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareLauncher : MonoBehaviour
{
    private enum LauncherState {Waiting, ChosingDirection, ChosingPower, Cooldown};

    private LauncherState currentState = LauncherState.Waiting;
    [SerializeField] private bool hasCooldown;
    void Start() {
        
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
        currentState = hasCooldown ? LauncherState.Cooldown : LauncherState.Waiting;
    }

    private void CooldownActions() {
        currentState = LauncherState.Waiting;
    }

    public void Launch(Vector2 direction, float power) {

    }
}
