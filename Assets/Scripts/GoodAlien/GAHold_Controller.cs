using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HealthManager))]
public class GAHold_Controller : MonoBehaviour
{
    GA_Movement gaMovement;

    HealthManager healthManager;
    GoodAlienScriptable _alien;

    void Awake() {
        gaMovement = GetComponent<GA_Movement>();
        healthManager = GetComponent<HealthManager>();
        _alien = GetComponent<GoodAlienMain>().Alien;
    }

    void Update() {
        Debug.Log("Hold");
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            gaMovement.MoveTowards(hit.point);
        }

        healthManager.TakeDamage(_alien.selfDamage);
    }

}
