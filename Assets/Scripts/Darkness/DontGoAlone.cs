using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(AudioSource))]
public class DontGoAlone : MonoBehaviour
{
    enum Status {Astronaut, Alien, TravelToAstronaut, TravelToAlien}

    [SerializeField] Transform flare;
    AstronautController astronaut;
    GoodAlienMain alien;
    Status currentStatus;

    [SerializeField] DontGoAloneScriptable dontGoAlone;
    HealthManager health;
    AudioSource audioSource;
    Coroutine moveToDestinationCoroutine;

    private void Awake() {
        astronaut = FindObjectOfType<AstronautController>();
        alien = FindObjectOfType<GoodAlienMain>();
        health = GetComponent<HealthManager>();
        audioSource = GetComponent<AudioSource>();

        transform.position = astronaut.transform.position;
        currentStatus = Status.Astronaut;
    }

    private void Update() {
        UpdatePosition();
        if (currentStatus == Status.Astronaut || currentStatus == Status.Alien) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                StartSwitch();
            }
        }
    }

    private void UpdatePosition()
    {
        if (currentStatus == Status.Astronaut) {
            transform.position = astronaut.transform.position;
        } else if (currentStatus == Status.Alien) {
            transform.position = alien.transform.position;
        }

    }

    private void StartSwitch() {
        audioSource.Play();
        if (currentStatus == Status.Astronaut) {
            // TODO :: CHANGE THE ASTRONAUT SPRITES
            currentStatus = Status.TravelToAlien;
        } else {
            alien.SwitchController();
            flare.localScale = new Vector3(5, 5, 5);
            currentStatus = Status.TravelToAstronaut;
        }
        health.isProtected = true;
        moveToDestinationCoroutine = StartCoroutine(MoveToDestinationCoroutine());
    }

    IEnumerator MoveToDestinationCoroutine() {
        GameObject destination = (currentStatus == Status.TravelToAstronaut)
            ? astronaut.gameObject
            : alien.gameObject;

        while (Vector2.Distance(transform.position, destination.transform.position) >= dontGoAlone.interactionDistance) {
            Vector2 translate = destination.transform.position - transform.position;
            translate.Normalize();
            translate *= dontGoAlone.speed * Time.deltaTime;
            transform.Translate(translate);
            yield return null;
        }

        transform.position = destination.transform.position;
        EndSwitch();
    }

    private void EndSwitch() {
        if (currentStatus == Status.TravelToAlien) {
            currentStatus = Status.Alien;
            alien.SwitchController();
            flare.localScale = new Vector3(1, 1, 1);
        } else {
            currentStatus = Status.Astronaut;
            health.isProtected = false;
            // TODO :: ASTRONAUT -- Switch activate dontGoAloneSprites
        }
    }
}
