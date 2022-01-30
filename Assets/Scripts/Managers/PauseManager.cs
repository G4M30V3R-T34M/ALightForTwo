using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;
    public void Start()
    {
        if (pauseMenu) {
            pauseMenu.SetActive(false);
            isPaused = false;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (!isPaused) { 
                PauseGame();
                isPaused = true;
            } else {
                ResumeGame();
                isPaused = false;
            }
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
