using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTakeThis : MonoBehaviour
{
    DontGoAlone takeThis;
    private void Awake() {
        takeThis = FindObjectOfType<DontGoAlone>();
    }

    void Update() {
        transform.position = new Vector3(
            takeThis.transform.position.x,
            takeThis.transform.position.y,
            transform.position.z
        );
    }
}
