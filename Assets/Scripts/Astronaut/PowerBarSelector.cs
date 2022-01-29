using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarSelector : MonoBehaviour
{
    [SerializeField] private float powerBarSpeed;
    [SerializeField] private float maxLength = 1f;
    [SerializeField] private float minLength = 0f;

    private Vector2 defaultScale;

    [SerializeField] private GameObject powerBar;
    void Start() {
        defaultScale = new Vector2(1, minLength);
        transform.localScale = defaultScale;
    }

    // Update is called once per frame
    void Update() {
        transform.localScale += new Vector3(0, powerBarSpeed * Time.deltaTime, 0);
        if (transform.localScale.y >= maxLength || transform.localScale.y <= minLength){
            powerBarSpeed *= -1;
            print(powerBarSpeed);
        }
    }

    public void ResetPower(Quaternion rotation) {
        transform.localScale = defaultScale;
        transform.rotation = rotation;
    }
}
