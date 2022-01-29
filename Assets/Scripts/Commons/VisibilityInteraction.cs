using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityInteraction : MonoBehaviour
{
    private int _spotlights;
 
    public delegate void LightChange();
    public event LightChange InLight, OutLight;

    public bool IsVisible { get { return _spotlights > 0; } }

    public void EnterSpotlight() {
        _spotlights++;
        if (_spotlights == 1) {
            if (InLight != null) {
                InLight.Invoke();
            }
        }
    }

    public void ExitSpotlight() {
        _spotlights--;
        if (_spotlights == 0) {
            if (OutLight != null) {
                OutLight.Invoke();
            }
        }
    }
}
