using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AstronautMovement))]
[RequireComponent(typeof(VisibilityInteraction))]
[RequireComponent(typeof(HealthManager))]
public class AstronautController : MonoBehaviour
{
    [SerializeField] AstronautScriptable _astronaut;
    private VisibilityInteraction visibility;
    private HealthManager healthManager;
    private AstronautMovement astronautMovement;

    public bool isVisible { get { return visibility.IsVisible; } }

    private Coroutine pickUpCoroutineReference;
    private float remainingPickUpTime;

    private bool pickableObject;
    GameObject objectToPick;

    private void Awake() {
        visibility = GetComponent<VisibilityInteraction>();
        healthManager = GetComponent<HealthManager>();
        astronautMovement = GetComponent<AstronautMovement>();
    }

    private void Start() {
        visibility.InLight += RestoreVelocity;
        visibility.OutLight += GoSlower;
        InitHealthManager();
    }

    private void OnDestroy(){
        visibility.InLight -= RestoreVelocity;
        visibility.OutLight -= GoSlower;
        healthManager.NoHealth -= Die;
    }

    private void InitHealthManager() {
        healthManager.SetUp(_astronaut.health);
        healthManager.NoHealth += Die;
    }

    public void Update() {
        //Debug.Log($"Astronaut is visible {isVisible}");
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
            if (pickUpCoroutineReference != null) {
                StopCoroutine(pickUpCoroutineReference);
                pickUpCoroutineReference = null;
            }

        }
    }

    private bool IsItem(Collider2D collision) {
        return collision.gameObject.layer == (int)Layers.Items;
    }

    private bool IsVisible(Collider2D collision) {
        return collision.gameObject.GetComponent<Item>().isVisible;
    }

    private bool IsTryingToPickUp() {
        return Input.GetKeyDown(KeyCode.Q);
    }

    private IEnumerator PickUpItem() {
        remainingPickUpTime = _astronaut.pickUpTime;
        while (remainingPickUpTime > 0) {
            remainingPickUpTime -= Time.deltaTime;
            yield return null;
        }
        pickUpCoroutineReference = null;
        objectToPick.gameObject.GetComponent<Item>().DoAction(gameObject);
        Destroy(objectToPick);
        objectToPick = null;
    }

    public void RestoreVelocity() {
        astronautMovement.velocity = _astronaut.normalVelocity;
    }

    public void GoSlower() {
        astronautMovement.velocity = _astronaut.slowVelocity;
    }

    public void TakeDamage(int damage) {
        healthManager.TakeDamage(damage);
    }

    private void Die() {
        Debug.Log("Astronaut Dies");
    }

}
