using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AstronautMovement))]
[RequireComponent(typeof(VisibilityInteraction))]
public class AstronautController : MonoBehaviour
{
    [SerializeField] AstronautScriptable _astronaut;
    private VisibilityInteraction visibility;
    public bool isVisible { get { return visibility.IsVisible; } }

    private Coroutine pickUpCoroutineReference;
    private float remainingPickUpTime;

    private bool pickableObject;
    GameObject objectToPick;

    public void Awake()
    {
        visibility = GetComponent<VisibilityInteraction>();
    }

    private void Start()
    {
        visibility.InLight += RestoreVelocity;
        visibility.OutLight += GoSlower;
        _astronaut.currentVelocity = _astronaut.normalVelocity;
    }

    public void Update() {
        if (pickableObject && IsTryingToPickUp()) {
            pickUpCoroutineReference = StartCoroutine(PickUpItem());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (IsItem(other) && IsVisible(other) && pickUpCoroutineReference == null) {
            objectToPick = other.gameObject;
            pickableObject = true;
        }  
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (IsItem(other)) {
            pickableObject = false;
            objectToPick = null;
            if (pickUpCoroutineReference != null)
            {
                StopCoroutine(pickUpCoroutineReference);
                pickUpCoroutineReference = null;
            }

        }
    }

    private bool IsItem(Collider2D collision) {
        return collision.gameObject.layer == (int)Layers.Items;
    }

    private bool IsVisible(Collider2D collision) {
        return collision.gameObject.GetComponent<Items>().isVisible;
    }

    private bool IsTryingToPickUp() {
        return Input.GetKeyDown(KeyCode.Q);
    }

    private IEnumerator PickUpItem() {
        remainingPickUpTime = _astronaut.pickUpTime;
        while (remainingPickUpTime > 0)
        {
            remainingPickUpTime -= Time.deltaTime;
            yield return null;
        }
        pickUpCoroutineReference = null;
        Destroy(objectToPick);
        objectToPick = null;
    }

    public void RestoreVelocity() {
        _astronaut.currentVelocity = _astronaut.normalVelocity;
    }

    public void GoSlower() {
        _astronaut.currentVelocity= _astronaut.slowVelocity;
    }


}
