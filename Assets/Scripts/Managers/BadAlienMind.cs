using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadAlienMind : Singleton<BadAlienMind>
{
    List<GameObject> lights;
    public AstronautController astronaut { get; private set; }

    protected override void Awake() {
        base.Awake();
        lights = new List<GameObject>();
        astronaut = FindObjectOfType<AstronautController>();
    }

    public List<GameObject> getAllLights() {
        return lights;
    }

    public void addLight(GameObject go) {
        lights.Add(go);
    }

    public void removeLight(GameObject go) {
        lights.Remove(go);
    }
}
