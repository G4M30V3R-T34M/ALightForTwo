using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour {

    // General info about scenes
    // 0: MainMenu
    // 1: Gameplay
    // 2: History
    // 3: Controls
    // 4: Credits

    private void menuSound() {
        ClickSound.instance.Play();
    }

    public void MainMenu() {
        menuSound();
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        menuSound();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void History() {
        menuSound();
        SceneManager.LoadScene(2);
    }

    public void Controls() {
        menuSound();
        SceneManager.LoadScene(3);
    }
    
    public void Credits() {
        menuSound();
        SceneManager.LoadScene(4);
    }

}
