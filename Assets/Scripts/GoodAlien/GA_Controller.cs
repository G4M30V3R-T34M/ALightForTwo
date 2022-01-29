using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Controller : MonoBehaviour
{
    [SerializeField] GoodAlienScriptable alien;
    Vector2 Destination;
    Coroutine activeDestinationCoroutine;

    void Start() {
        Destination = new Vector2(0,0);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Destination = hit.point;
            Debug.Log($"Destination ({Destination.x}, {Destination.y})");
            if (activeDestinationCoroutine == null) {
                activeDestinationCoroutine = StartCoroutine(DestinationCoroutine());
            }
        }
    }

    IEnumerator DestinationCoroutine()
    {
        while (Vector2.Distance(Destination, (Vector2)transform.position) >= alien.interactDistance)
        {
            Vector2 trans = Destination - (Vector2)transform.position;
            trans.Normalize();
            trans *= alien.Speed * Time.deltaTime;
            transform.Translate(trans);
            yield return null;
        }
        activeDestinationCoroutine = null;
    }
}
