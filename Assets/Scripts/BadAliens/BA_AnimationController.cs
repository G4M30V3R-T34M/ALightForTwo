using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BA_AnimationController : MonoBehaviour
{
    Animator animator;

    const string KEY_STATE = "State";
    const string KEY_DIRECTION = "Direction";

    BA_AnimStates currentState, nextState;

    public enum BA_AnimStates {
        Iddle = 0,
        Movement = 1,
        Attack = 2,
        Death = 3,
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        animator.SetInteger(KEY_STATE, (int)BA_AnimStates.Iddle);
        animator.SetInteger(KEY_DIRECTION, (int)Directions.Down);
    }
    public void Stop() {
        animator.SetInteger(KEY_STATE, (int)BA_AnimStates.Iddle);
    }

    public void Move() {
        animator.SetInteger(KEY_STATE, (int)BA_AnimStates.Movement);
    }

    public void SetDirection(Directions dir) {
        animator.SetInteger(KEY_DIRECTION, (int)dir);
    }

    public void Attack() {
        animator.SetInteger(KEY_STATE, (int)BA_AnimStates.Attack);
    }

    public void Death() {
        animator.SetInteger(KEY_STATE, (int)BA_AnimStates.Death);
    }

}
