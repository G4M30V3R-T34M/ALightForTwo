using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] GameOverManager GO_Manager;
    [SerializeField] GameObject[] fuel = new GameObject[3];
    private int index;
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
            GO_Manager.GameOver();
        }
    }
}
