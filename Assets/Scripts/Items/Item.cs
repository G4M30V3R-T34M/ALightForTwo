using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VisibilityInteraction))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public abstract class Item : MonoBehaviour
{
    private VisibilityInteraction visibility;
    public bool isVisible { get { return visibility.IsVisible; } }
    [SerializeField] protected ItemScriptable _item;
    AudioSource audioSource;
    SpriteRenderer sprite;
    Collider2D collider2d;

    protected void Awake() {
        visibility = GetComponent<VisibilityInteraction>();
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
    }

    public abstract void DoAction(GameObject picker=null);

    protected IEnumerator PlaySoundAndDestroy() {
        audioSource.Play();
        sprite.enabled = false;
        collider2d.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }


}
