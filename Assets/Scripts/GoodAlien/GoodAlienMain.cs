using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GAFree_Controller))]
[RequireComponent(typeof(GAHold_Controller))]
[RequireComponent(typeof(GA_Movement))]
[RequireComponent(typeof(VisibilityInteraction))]
public class GoodAlienMain : MonoBehaviour
{
    VisibilityInteraction visibility;
    public bool IsVisible { get { return visibility.IsVisible; } }

    [SerializeField] GoodAlienScriptable _alien ;
    public GoodAlienScriptable Alien { get { return _alien; } }

    GAFree_Controller freeController;
    GAHold_Controller holdController;

    private void Awake() {
        visibility = GetComponent<VisibilityInteraction>();
        freeController = GetComponent<GAFree_Controller>();
        holdController = GetComponent<GAHold_Controller>();
    }

    void Start() {
        freeController.enabled = true;
        holdController.enabled = false;

        visibility.InLight += EnterLight;
        visibility.OutLight += ExitLight;
    }

    private void EnterLight() {
        Debug.Log("EnterLight");
        // StartHealing
    }
    private void ExitLight()
    {
        Debug.Log("ExitLight");

    }

    // TODO :: THIS IS TEMPORARY
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SwitchController();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            visibility.EnterSpotlight();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            visibility.ExitSpotlight();
        }

    }

    public void SwitchController() {
        freeController.enabled = !freeController.isActiveAndEnabled;
        holdController.enabled = !holdController.isActiveAndEnabled;
    }

}
