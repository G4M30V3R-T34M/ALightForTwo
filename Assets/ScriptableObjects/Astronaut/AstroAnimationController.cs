using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AstroAnimationController : MonoBehaviour
{
    Animator animator;

    const string KEY_STATE = "State";
    const string KEY_DIRECTION = "Direction";

    AstroAnimStates currentState, nextState;

    public enum AstroAnimStates {
        Iddle = 0,
        Movement = 1,
        Pick = 2,
        Throw = 3,
        Hurt = 4,
        Death = 5,
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        animator.SetInteger(KEY_STATE, (int)AstroAnimStates.Iddle);
        animator.SetInteger(KEY_DIRECTION, (int)Directions.Down);
    }

    public void SetXSpeed(float x) {
        animator.SetFloat("xSpeed", x);
    }

    public void SetYSpeed(float y) {
        animator.SetFloat("ySpeed", y);
    }

    public void RecieveLight() {
        animator.SetTrigger("RecieveLight");
    }

    public void ThrowLight() {
        animator.SetTrigger("ThrowLight");
    }

    public void Stop() {
        if (
            currentState == AstroAnimStates.Iddle ||
            currentState == AstroAnimStates.Movement
        ) {
            animator.SetInteger(KEY_STATE, (int)AstroAnimStates.Iddle);
            currentState = AstroAnimStates.Iddle;
        } else {
            nextState = AstroAnimStates.Iddle;
        }
    }

    public void Move() {
        if (
            currentState == AstroAnimStates.Iddle ||
            currentState == AstroAnimStates.Movement ||
            currentState == AstroAnimStates.Pick 
        ) {
            animator.SetInteger(KEY_STATE, (int)AstroAnimStates.Movement);
            currentState = AstroAnimStates.Movement;
        } else {
            nextState = AstroAnimStates.Movement;
        }
    }

    public void Throw() {
        animator.SetInteger(KEY_STATE, (int)AstroAnimStates.Throw);
        currentState = AstroAnimStates.Throw;
    }

    public void Pick() {
        animator.SetInteger(KEY_STATE, (int)AstroAnimStates.Pick);
        currentState = AstroAnimStates.Pick;
    }

    public void ChangeDirection(Directions dir) {
        animator.SetInteger(KEY_DIRECTION, (int)dir);
    }

    public void Hurt() {
        animator.SetInteger(KEY_STATE, (int)AstroAnimStates.Hurt);
        currentState = AstroAnimStates.Hurt;
    }

    public void Die() {
        animator.SetInteger(KEY_STATE, (int)AstroAnimStates.Death);
    }

    public void SetNextState() {
        animator.SetInteger(KEY_STATE, (int)nextState);
        currentState = nextState;
        nextState = AstroAnimStates.Iddle;
    }

}
