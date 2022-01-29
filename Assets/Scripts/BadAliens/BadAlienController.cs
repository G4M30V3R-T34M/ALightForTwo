using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadAlienController : PoolableObject
{
    enum TargetType {Light, Astronaut}
    [SerializeField] BadAlienScriptable alien;

    private GameObject currentTarget;
    private TargetType targetType;

    void Start() {
        StartCoroutine(FetchEnvironmentCoroutine());
    }

    void Update() {
        if (Vector2.Distance(currentTarget.transform.position, transform.position) < alien.interactionDistance) {
            InteractWithTarget();
        } else {
            MoveTowardsTarget();
        }
    }

    IEnumerator FetchEnvironmentCoroutine() {
        while(true) {
            currentTarget = GetCurrentTarget();
            yield return new WaitForSeconds(alien.lightFetchTime);
        }
    }

    private GameObject GetCurrentTarget() {
        if (AstronautVisibleAndInRange()) {
            targetType = TargetType.Astronaut;
            return BadAlienMind.Instance.astronaut.gameObject;
        } else {
            targetType = TargetType.Light;
            return GetClosestLight();
        }
    }

    private bool AstronautVisibleAndInRange() {
        return
            BadAlienMind.Instance.astronaut.IsVisible &&
            Vector2.Distance(BadAlienMind.Instance.astronaut.transform.position, transform.position)
                <= alien.astronautDetectionDistance;
    }

    private GameObject GetClosestLight() {
        float MinDistance = Mathf.Infinity;
        GameObject closestLight = null; 

        foreach (GameObject light in BadAlienMind.Instance.getAllLights()) {
            float dist = Vector2.Distance(light.transform.position, transform.position);
            if (dist < MinDistance) {
                closestLight = light;
                MinDistance = dist;
            }
        }

        return closestLight;
    }

    private void InteractWithTarget() {
        // TODO
        Debug.Log("ALIEN ATTACK / DETROY LIGHT");
    }

    private void MoveTowardsTarget() {
        Vector2 translate = currentTarget.transform.position - transform.position;
        translate.Normalize();
        translate *= alien.speed * Time.deltaTime;
        transform.Translate(translate);
    }
}
