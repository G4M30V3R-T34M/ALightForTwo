using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareLauncher : MonoBehaviour
{
    private enum LauncherState {Waiting, ChosingDirection, ChosingPower, CoolDown};

    private LauncherState currentState = LauncherState.Waiting;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("FlareLaunch") > 0)
        {
            switch (currentState)
            {
                case LauncherState.Waiting:
                    break;
                case LauncherState.ChosingDirection:
                    break;
                case LauncherState.ChosingPower:
                    break;
                case LauncherState.CoolDown:
                    break;
            }
        }
        
    }

    public void Launch(Vector2 direction, float power) {

    }
}
