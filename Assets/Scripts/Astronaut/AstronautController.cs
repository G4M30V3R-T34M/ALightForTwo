using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautController : MonoBehaviour
{
    [SerializeField] AstronautScriptable _astronaut;
    [SerializeField] public bool IsVisible;

    void Update() {
        UpdatePosition();
    }

    private void UpdatePosition() {
        float positionX = this.transform.position.x + (Input.GetAxisRaw("Horizontal") * _astronaut.velocity * Time.deltaTime);
        float positionY = this.transform.position.y + (Input.GetAxisRaw("Vertical") * _astronaut.velocity * Time.deltaTime);
        this.transform.position = new Vector2(positionX, positionY);
    }
}
