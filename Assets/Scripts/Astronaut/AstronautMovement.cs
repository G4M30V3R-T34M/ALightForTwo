using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautMovement : MonoBehaviour
{
    [SerializeField] AstronautScriptable _astronaut;

    public float velocity { get;  set; }

    private void Start()
    {
        velocity = _astronaut.normalVelocity;
    }

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float positionX = this.transform.position.x + (Input.GetAxisRaw("Horizontal") * velocity * Time.deltaTime);
        float positionY = this.transform.position.y + (Input.GetAxisRaw("Vertical") * velocity * Time.deltaTime);
        this.transform.position = new Vector2(positionX, positionY);
    }
}
