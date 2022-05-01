using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BA_AnimationController))]
[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(AudioSource))]
public class BadAlienController : PoolableObject
{
    enum TargetType {Light, Astronaut}
    [SerializeField] BadAlienScriptable alien;
    BA_AnimationController animator;
    AudioSource audioSource;
    
    HealthManager healthManager;

    private GameObject currentTarget;
    private TargetType targetType;

    int obstacles;
    bool isAttacking;

    float afraidTime;
    Vector2 fearPoint;

    Coroutine CooldownCoroutieneReference;

    private void Awake() {
        healthManager = GetComponent<HealthManager>();
        animator = GetComponent<BA_AnimationController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        InitHealthManager();
        isAttacking = false;
    }

    private void InitHealthManager() {
        healthManager.SetUp(alien.health);
        healthManager.NoHealth += Die;
    }

    private void Die() {
        gameObject.SetActive(false);
    }

    void OnEnable() {
        StartCoroutine(FetchEnvironmentCoroutine());
        afraidTime = 0;
        obstacles = 0;
    }

    override protected void OnDisable() {
        if (CooldownCoroutieneReference != null) {
            StopCoroutine(CooldownCoroutieneReference);
        }
        base.OnDisable();
        
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
        if (isAttacking) { return; }
        if (currentTarget == null) { return; }

        if (Vector2.Distance(currentTarget.transform.position, transform.position) < alien.interactionDistance) {
            InteractWithTarget();
        } else {
            MoveTowardsTarget();
        }
    }

    IEnumerator FetchEnvironmentCoroutine() {
        while(true) {
            yield return new WaitForSeconds(alien.lightFetchTime);
            currentTarget = GetCurrentTarget();
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
        animator.Move();
        Vector2 translate = (Vector2)transform.position - fearPoint;
        translate.Normalize();
        translate *= GetSpeed() * Time.deltaTime;
        transform.Translate(translate);
        UpdateAnimationDirection(translate);
    }

    private void UpdateAnimationDirection(Vector2 trans) {
        if (Mathf.Abs(trans.x) > Mathf.Abs(trans.y)) {
            if (trans.x > 0) {
                animator.SetDirection(Directions.Right);
            } else {
                animator.SetDirection(Directions.Left);
            }
        } else {
            if (trans.y > 0) {
                animator.SetDirection(Directions.Up);
            } else {
                animator.SetDirection(Directions.Down);
            }
        }
    }

    private void InteractWithTarget() {
        animator.Attack();
        if (CooldownCoroutieneReference == null) {
            audioSource.Play();
            isAttacking = true;
            currentTarget.GetComponent<HealthManager>().TakeDamage(alien.damageValue);
            CooldownCoroutieneReference = StartCoroutine(AtackCooldown());
        }
    }

    private void MoveTowardsTarget() {
        animator.Move();
        Vector2 translate = currentTarget.transform.position - transform.position;
        translate.Normalize();
        translate *= GetSpeed() * Time.deltaTime;
        transform.Translate(translate);
        UpdateAnimationDirection(translate);
    }

    private float GetSpeed() {
        float speed = (afraidTime > 0) ? alien.fearSpeed : alien.speed;
        if (obstacles > 0 ) { speed *= alien.obstacleSpeedModifier; }
        return speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == (int)Layers.GoodAlienShout) {
            fearPoint = collision.gameObject.transform.position;
            afraidTime = alien.loseControllTime;
        } else if (collision.gameObject.layer == (int)Layers.Obstacles) {
            obstacles++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == (int)Layers.Obstacles) {
            obstacles--;
        }
    }

    public void AttackEnd() {
        isAttacking = false;
        animator.Stop();
    }

    private IEnumerator AtackCooldown() {
        yield return new WaitForSeconds(alien.attackCooldown);
        CooldownCoroutieneReference = null;
    }
}
