using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGoAlone : MonoBehaviour
{
    enum Status {Astronaut, Alien, TravelToAstronaut, TravelToAlien}

    [SerializeField] Transform flare;
    AstronautController astronaut;
    GoodAlienMain alien;
    Status currentStatus;

    [SerializeField] DontGoAloneScriptable dontGoAlone;
    Coroutine moveToDestinationCoroutine;

    private void Awake() {
        astronaut = FindObjectOfType<AstronautController>();
        alien = FindObjectOfType<GoodAlienMain>();

        transform.position = astronaut.transform.position;
        transform.parent = astronaut.transform;
        currentStatus = Status.Astronaut;
    }

    private void Update() {
        if (currentStatus == Status.Astronaut || currentStatus == Status.Alien) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                transform.parent = null; // TODO :: NOT SURE
                StartSwitch();
            }
        }
    }

    private void StartSwitch() {
        if (currentStatus == Status.Astronaut) {
            // TODO :: CHANGE THE ASTRONAUT SPRITES
            currentStatus = Status.TravelToAlien;
        } else {
            alien.SwitchController();
            flare.localScale = new Vector3(5, 5, 5);
            currentStatus = Status.TravelToAstronaut;
        }
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
        transform.parent = destination.transform;
        EndSwitch();
    }

    private void EndSwitch() {
        if (currentStatus == Status.TravelToAlien) {
            currentStatus = Status.Alien;
            alien.SwitchController();
            flare.localScale = new Vector3(1, 1, 1);
        } else {
            currentStatus = Status.Astronaut;
            // TODO :: ASTRONAUT -- Switch activate dontGoAloneSprites
        }
    }
}
