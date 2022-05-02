using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ClickSound : MonoBehaviour
{
    AudioSource audioSource;
    public static ClickSound instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play() {
        audioSource.Play();
    }
}
