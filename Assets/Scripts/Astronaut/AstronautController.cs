using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautController : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private ObjectPool op;
    
    void Start() {
        op = gameObject.GetComponent<ObjectPool>();
    }

    void Update() {
        UpdatePosition();
    }

    private void UpdatePosition() {
        float positionX = this.transform.position.x + (Input.GetAxisRaw("Horizontal") * this.velocity * Time.deltaTime);
        float positionY = this.transform.position.y + (Input.GetAxisRaw("Vertical") * this.velocity * Time.deltaTime);
        this.transform.position = new Vector2(positionX, positionY);
    }
}
