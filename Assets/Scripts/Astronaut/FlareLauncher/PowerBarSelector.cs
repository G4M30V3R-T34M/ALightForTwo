using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarSelector : MonoBehaviour
{
    [SerializeField] PowerBarScriptable _bar;

    private Vector2 defaultScale;

    [SerializeField] private GameObject powerBar;
    void Start() {
        defaultScale = new Vector2(1, _bar.minLength);
        transform.localScale = defaultScale;
    }

    // Update is called once per frame
    void Update() {
        transform.localScale += new Vector3(0, _bar.powerBarSpeed * Time.deltaTime, 0);
        if (transform.localScale.y >= _bar.maxLength || transform.localScale.y <= _bar.minLength)
        {
            _bar.powerBarSpeed *= -1;
        }
    }

    public void ResetPower(Quaternion rotation) {
        transform.localScale = defaultScale;
        transform.rotation = rotation;
    }
}
