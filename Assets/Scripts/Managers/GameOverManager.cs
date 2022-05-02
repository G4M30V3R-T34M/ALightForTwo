using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject win;
    public void Start()
    {
        if (gameOver)
        {
            gameOver.SetActive(false);
        }
        if (win)
        {
            win.SetActive(false);
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void Win()
    {
        win.SetActive(true);
        Time.timeScale = 0;
    }

}
