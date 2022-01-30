using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GoodAlienMain))]
public class GA_Movement : MonoBehaviour
{
    enum Actions {None, Move, Eat, Attack};
    Actions currentAction;
    GameObject gameObjectDestination;
    Vector2 pointDestination;

    Coroutine movementCoroutine;
    GoodAlienScriptable alien;
    GoodAlienMain alienMain;

    int obstacles;

    void Awake() {
        alienMain = GetComponent<GoodAlienMain>();
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
        while (Vector2.Distance(GetDestination(), transform.position) >= alien.interactDistance) {
            Vector2 trans = GetDestination() - (Vector2)transform.position;
            trans.Normalize();
            trans *= GetSpeed() * Time.deltaTime;
            transform.Translate(trans);
            yield return null;
        }
        DoAction();
        movementCoroutine = null;
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
        // TODO
        Debug.Log("PERFORM ATTACK");
        gameObjectDestination.GetComponent<HealthManager>().TakeDamage(alien.damageValue);
    }

    private void DoEat() {
        // TODO
        Debug.Log("EAT FLARE");
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
