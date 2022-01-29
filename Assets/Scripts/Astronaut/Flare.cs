using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : PoolableObject
{
    // Start is called before the first frame update
    void Start() {

        BadAlienMind.Instance.addLight(gameObject);
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    override protected void OnDisable() {
        if (!BadAlienMind.EmptyInstance()) {
            BadAlienMind.Instance.removeLight(gameObject);
        }
        base.OnDisable();
    }

}
