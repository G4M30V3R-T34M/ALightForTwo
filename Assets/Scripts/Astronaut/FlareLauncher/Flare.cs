using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : PoolableObject
{
    [SerializeField] FlareScriptable _flare;

    private CircleCollider2D collider;
    // Start is called before the first frame update
    void Start() {
        BadAlienMind.Instance.addLight(gameObject);
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Launch(Vector3 direction, float power) {
        Vector3 target = direction * power * _flare.distanceCoefficient;
        target += transform.position;
        StartCoroutine(GoToPosition(target));
    }

    private IEnumerator GoToPosition(Vector3 target)
    {
        while(Vector2.Distance(transform.position, target) >= _flare.distanceError) {
            transform.position = Vector2.MoveTowards(transform.position, target, _flare.velocity * Time.deltaTime);
            yield return null;
            print(Vector2.Distance(transform.position, target));
            print(_flare.distanceError);
        }
        collider.enabled = true;
    }
    override protected void OnDisable() {
        if (!BadAlienMind.EmptyInstance()) {
            BadAlienMind.Instance.removeLight(gameObject);
        }
        base.OnDisable();
    }
}
