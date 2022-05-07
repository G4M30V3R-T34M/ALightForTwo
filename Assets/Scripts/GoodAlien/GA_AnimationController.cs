using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GA_AnimationController : MonoBehaviour
{
    Animator animator;

    const string KEY_STATE = "State";
    const string KEY_DIRECTION = "Direction";

    GA_AnimStates currentState, nextState;

    public enum GA_AnimStates
    {
        Iddle = 0,
        Movement = 1,
        Atk = 2,
        Shout = 3,
        Death = 4
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetInteger(KEY_STATE, (int)GA_AnimStates.Iddle);
        animator.SetInteger(KEY_DIRECTION, (int)Directions.Down);
        currentState = GA_AnimStates.Iddle;
        nextState = GA_AnimStates.Iddle;
    }

    public void Stop()
    {
        animator.SetInteger(KEY_STATE, (int)GA_AnimStates.Iddle);
    }

    public void Move()
    {
        if (
            currentState == GA_AnimStates.Iddle ||
            currentState == GA_AnimStates.Movement
        )
        {
            animator.SetInteger(KEY_STATE, (int)GA_AnimStates.Movement);
            currentState = GA_AnimStates.Movement;
        }
        else
        {
            nextState = GA_AnimStates.Movement;
        }
    }

    public void ChangeDirection(Directions dir)
    {
        animator.SetInteger(KEY_DIRECTION, (int)dir);
    }

    public void Attack()
    {
        animator.SetInteger(KEY_STATE, (int)GA_AnimStates.Atk);
        currentState = GA_AnimStates.Atk;
    }

    public void Shout()
    {
        nextState = (GA_AnimStates)animator.GetInteger(KEY_STATE);
        animator.SetInteger(KEY_STATE, (int)GA_AnimStates.Shout);
        currentState = GA_AnimStates.Shout;
    }

    public void Die()
    {
        animator.SetInteger(KEY_STATE, (int)GA_AnimStates.Death);
        currentState = GA_AnimStates.Death;
    }

    public void SetNextState()
    {
        animator.SetInteger(KEY_STATE, (int)nextState);
        currentState = nextState;
        nextState = GA_AnimStates.Iddle;
    }

    public void SetXSpeed(float x)
    {
        animator.SetFloat("xSpeed", x);
    }

    public void SetYSpeed(float y)
    {
        animator.SetFloat("ySpeed", y);
    }

    public void RecieveLight()
    {
        animator.SetTrigger("RecieveLight");
    }

    public void ThrowLight()
    {
        animator.SetTrigger("ThrowLight");

    }

}