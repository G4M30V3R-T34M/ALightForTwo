using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    // General info about scenes
    // 0: MainMenu
    // 1: Gameplay
    // 2: History
    // 3: Controls
    // 4: Credits

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void History() {
        SceneManager.LoadScene(2);
    }

    public void Controls() {
        SceneManager.LoadScene(3);
    }
    
    public void Credits() {
        SceneManager.LoadScene(4);
    }

}
