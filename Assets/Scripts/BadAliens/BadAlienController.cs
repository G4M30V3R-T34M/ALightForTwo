using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HealthManager))]
public class BadAlienController : PoolableObject
{
    enum TargetType {Light, Astronaut}
    [SerializeField] BadAlienScriptable alien;
    
    HealthManager healthManager;

    private GameObject currentTarget;
    private TargetType targetType;

    float afraidTime;
    Vector2 fearPoint;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    private void Start()
    {
        InitHealthManager();
    }

    private void InitHealthManager()
    {
        healthManager.SetUp(alien.health);
        healthManager.NoHealth += Die;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    void OnEnable() {
        StartCoroutine(FetchEnvironmentCoroutine());
        afraidTime = 0;
    }

    void Update() {
        if (afraidTime > 0) {
            afraidTime -= Time.deltaTime;
            AfraidUpdate();
        } else {
            FreeUpdate();
        }
    }

    private void AfraidUpdate() {
        MoveAwayFromFear();
    }

    private void FreeUpdate() {
        if (currentTarget == null) { return; }

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
            BadAlienMind.Instance.astronaut.isVisible &&
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

    private void MoveAwayFromFear() {
        Vector2 translate = (Vector2)transform.position - fearPoint;
        translate.Normalize();
        translate *= alien.fearSpeed * Time.deltaTime;
        transform.Translate(translate);
    }

    private void InteractWithTarget() {
        currentTarget.GetComponent<HealthManager>().TakeDamage(alien.damageValue);
    }

    private void MoveTowardsTarget() {
        Vector2 translate = currentTarget.transform.position - transform.position;
        translate.Normalize();
        translate *= alien.speed * Time.deltaTime;
        transform.Translate(translate);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == (int)Layers.GoodAlienShout) {
            fearPoint = collision.gameObject.transform.position;
            afraidTime = alien.loseControllTime;
        }
    }
}
