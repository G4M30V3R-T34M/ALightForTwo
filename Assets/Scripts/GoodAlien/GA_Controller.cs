using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Controller : MonoBehaviour
{
    [SerializeField] AlienScriptable alien;
    Vector2 Destination;
    Coroutine activeDestinationCoroutine;

    void Start() {
        Destination = new Vector2(0,0);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null) {
                Destination = hit.point;
                if (activeDestinationCoroutine == null) {
                    activeDestinationCoroutine = StartCoroutine(DestinationCoroutine());
                }
                Debug.Log(hit.point);
            }
        }
    }

    IEnumerator DestinationCoroutine()
    {
        while (Vector2.Distance(Destination, new Vector2(transform.position.x, transform.position.y)) >= 1)
        {
            yield return null;
            Vector2 trans = Destination - new Vector2(transform.position.x, transform.position.y);
            trans = trans.normalized;
            transform.Translate(new Vector2(trans.x * alien.Speed * Time.deltaTime, trans.y * alien.Speed * Time.deltaTime));
        }
        activeDestinationCoroutine = null;
    }
}
