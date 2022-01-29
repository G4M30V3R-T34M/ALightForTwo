using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityInteraction : MonoBehaviour
{
    private int _spotlights;

    public bool IsVisible { get { return _spotlights > 0; } }

    public void EnterSpotlight() {
        _spotlights++;
    }

    public void ExitSpotlight() {
        _spotlights--;
    }
}
