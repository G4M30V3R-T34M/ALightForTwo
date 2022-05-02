using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMusic : MonoBehaviour
{
    [SerializeField] AudioClip MenuMusic;
    [SerializeField] AudioClip GameMusic;

    public enum BGMType { Menu, Game };
    [SerializeField] public BGMType type;

    AudioSource audioSource;

    static BGMusic instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            if (instance.type != this.type) {
                Destroy(instance.gameObject);
                instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = type == BGMType.Menu ? MenuMusic : GameMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}
