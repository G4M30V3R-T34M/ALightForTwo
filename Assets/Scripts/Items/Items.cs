using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public bool isVisible { get; private set; }
    void Start() {
        isVisible = false;
    }

    public void MakeObjectVisible() {
        isVisible = true;
    }
}
