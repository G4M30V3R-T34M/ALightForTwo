using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAHold_Controller : MonoBehaviour
{
    GA_Movement gaMovement;

    void Awake() {
        gaMovement = GetComponent<GA_Movement>();
    }

    void Update() {
        Debug.Log("Hold");
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            gaMovement.MoveTowards(hit.point);
        }
    }

}
