using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VisibilityInteraction))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Item : MonoBehaviour
{
    private VisibilityInteraction visibility;
    public bool isVisible { get { return visibility.IsVisible; } }
    [SerializeField] protected ItemScriptable _item;

    private void Awake()
    {
        visibility = GetComponent<VisibilityInteraction>();
    }

    // It's important to call Destroy at the end of this function
    public abstract void DoAction(GameObject picker=null);


}
