using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HUD_Manager : MonoBehaviour
{
    [SerializeField] GameOverManager GO_Manager;
    [SerializeField] GameObject[] fuel = new GameObject[3];
    AudioSource audioSource;
    private int index;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        foreach (GameObject element in fuel) {
            element.SetActive(false);
        }
        index = 0;
    }

    public void FuelAdquired()
    {
        fuel[index].SetActive(true);
        index++;
        if (index == fuel.Length) {
            audioSource.Play();
            GO_Manager.Win();
        }
    }
}
