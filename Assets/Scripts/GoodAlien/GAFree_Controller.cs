using UnityEngine;

public class GAFree_Controller : MonoBehaviour
{
    GA_Movement gaMovement;

    void Awake() {
        gaMovement = GetComponent<GA_Movement>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            RaycastHit2D hit = GetMainHit(hits);
            StartAction(hit);
        }
    }

    private RaycastHit2D GetMainHit(RaycastHit2D[] hits)
    {
        RaycastHit2D currentHit;

        if (TryGetLayerHit(hits, Layers.BadAlien, out currentHit)) { return currentHit; }
        if (TryGetLayerHit(hits, Layers.Flare, out currentHit)) { return currentHit; }
        if (TryGetLayerHit(hits, Layers.Scenario, out currentHit)) { return currentHit; }

        Debug.LogError("Didn't find any objective for Good Alien Action");
        return currentHit; // first hit in the array
    }

    private bool TryGetLayerHit(RaycastHit2D[] hits, Layers layer, out RaycastHit2D outHit)
    {
        foreach (RaycastHit2D hit in hits) {
            if (hit.collider.gameObject.layer == (int)layer) {
                outHit = hit;
                return true;
            }
        }
        outHit = hits[0];
        return false;
    }

    private void StartAction(RaycastHit2D hit) {
        if (hit.collider.gameObject.layer == (int)Layers.BadAlien) {
            gaMovement.AttackEnemy(hit.collider.gameObject);
        } else if (hit.collider.gameObject.layer == (int)Layers.Flare) {
            gaMovement.EatFlare(hit.collider.gameObject);
        } else {
            gaMovement.MoveTowards(hit.point);
        }
    }
}
