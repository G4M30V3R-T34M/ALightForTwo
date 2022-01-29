using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VisibilityInteraction))]
public class Items : MonoBehaviour
{
    private VisibilityInteraction visibility;
    public bool isVisible { get { return visibility.IsVisible; } }

    private void Awake()
    {
        visibility = GetComponent<VisibilityInteraction>();
    }

}
