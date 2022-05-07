using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GoodAlienMain))]
[RequireComponent(typeof(GA_AnimationController))]
public class GA_Movement : MonoBehaviour
{
    enum Actions {None, Move, Eat, Attack};
    Actions currentAction;
    GameObject gameObjectDestination;
    Vector2 pointDestination;

    Coroutine movementCoroutine;
    GoodAlienScriptable alien;
    GoodAlienMain alienMain;
    GA_AnimationController animatorController;
    [SerializeField] AudioSource attackAudioSource;

    int obstacles;

    void Awake() {
        alienMain = GetComponent<GoodAlienMain>();
        animatorController = GetComponent<GA_AnimationController>();
        alien = alienMain.Alien;
    }

    void Start() {
        obstacles = 0;
    }

    public void MoveTowards(Vector2 point) {
        currentAction = Actions.Move;
        pointDestination = point;
        StartAction();
    }

    public void EatFlare(GameObject flare) {
        currentAction = Actions.Eat;
        gameObjectDestination = flare;
        StartAction();
    }

    public void AttackEnemy(GameObject enemy) {;
        currentAction = Actions.Attack;
        gameObjectDestination = enemy;
        StartAction();
    }

    private void StartAction() {
        if  (movementCoroutine == null) {
            movementCoroutine = StartCoroutine(MovementCoroutine());
        }
    }

    IEnumerator MovementCoroutine() {
        animatorController.Move();
        while (Vector2.Distance(GetDestination(), transform.position) >= alien.interactDistance) {
            Vector2 trans = GetDestination() - (Vector2)transform.position;
            trans.Normalize();
            trans *= GetSpeed() * Time.deltaTime;
            transform.Translate(trans);
            UpdateAnimator(trans);
            yield return null;
        }
        animatorController.Stop();
        DoAction();
        movementCoroutine = null;
    }

    private void UpdateAnimator(Vector2 trans) {
        
        animatorController.SetXSpeed(trans.x);
        animatorController.SetYSpeed(trans.y);

        if (Mathf.Abs(trans.x) > Mathf.Abs(trans.y)) {
            if (trans.x > 0) {
                animatorController.ChangeDirection(Directions.Right);
            } else {
                animatorController.ChangeDirection(Directions.Left);
            }
        } else {
            if (trans.y > 0) {
                animatorController.ChangeDirection(Directions.Up);
            } else {
                animatorController.ChangeDirection(Directions.Down);
            }
        }
    }


    private float GetSpeed() {
        float speed = alien.Speed;
        if (!alienMain.isFree) { speed *= alien.HoldSpeedModifier; }
        if (obstacles > 0) { speed *= alien.ObstacleSpeedModifier; }
        return speed;
    }

    private Vector2 GetDestination() {
        if (currentAction == Actions.Attack || currentAction == Actions.Eat) {
            return gameObjectDestination.transform.position;
        } else {
            return pointDestination;
        }
    }

    private void DoAction() {
        if (currentAction == Actions.Attack) { DoAttack(); }
        else if (currentAction == Actions.Eat) { DoEat(); }
    }

    private void DoAttack() {
        attackAudioSource.Play();
        animatorController.Attack();
        gameObjectDestination.GetComponent<HealthManager>().TakeDamage(alien.damageValue);
    }

    private void DoEat() {
        attackAudioSource.Play();
        animatorController.Attack();
        gameObjectDestination.GetComponent<HealthManager>().TakeDamage(alien.damageToFlare);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == (int)Layers.Obstacles) {
            obstacles++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == (int)Layers.Obstacles) {
            obstacles--;
        }
    }
}
