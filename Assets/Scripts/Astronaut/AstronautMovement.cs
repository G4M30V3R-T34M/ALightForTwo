using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AstroAnimationController))]
public class AstronautMovement : MonoBehaviour
{
    [SerializeField] AstronautScriptable _astronaut;
    AstroAnimationController animator;

    public float velocity { get;  set; }

    private void Awake() {
        animator = GetComponent<AstroAnimationController>();
    }

    private void Start() {
        velocity = _astronaut.normalVelocity;
    }

    void Update() {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            animator.Move();
            UpdateAnimatorDirection();
            UpdatePosition();
        } else {
            animator.Stop();
        }
    }

    private void UpdateAnimatorDirection() {
        float hor = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        animator.SetXSpeed(hor);
        animator.SetYSpeed(vert);

        if (Mathf.Abs(hor) > Mathf.Abs(vert)) {
            if (hor > 0) {
                animator.ChangeDirection(Directions.Right);
            } else {
                animator.ChangeDirection(Directions.Left);
            }
        } else {
            if (vert > 0) {
                animator.ChangeDirection(Directions.Up);
            } else {
                animator.ChangeDirection(Directions.Down);
            }
        }
    }

    private void UpdatePosition()
    {
        float positionX = this.transform.position.x + (Input.GetAxisRaw("Horizontal") * velocity * Time.deltaTime);
        float positionY = this.transform.position.y + (Input.GetAxisRaw("Vertical") * velocity * Time.deltaTime);
        this.transform.position = new Vector2(positionX, positionY);
    }
}
