using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : PoolableObject
{
    [SerializeField] FlareScriptable _flare;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Launch(Vector3 direction, float power) {
        Vector3 target = direction * power * _flare.distanceCoefficient;
        StartCoroutine(GoToPosition(target));
    }

    private IEnumerator GoToPosition(Vector3 target)
    {
        while(Vector2.Distance(transform.position, target) >= _flare.distanceError) {
            print(target);
            print(_flare.velocity * Time.deltaTime);
            Vector2.MoveTowards(transform.position, target, _flare.velocity * Time.deltaTime);
            yield return null;
        }
    }
}
