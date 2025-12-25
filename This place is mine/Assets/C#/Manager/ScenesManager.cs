using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoSingleton<ScenesManager>
{
    public void StartGame()
    {
        SceneManager.LoadScene("StartGame");
    }
    public void Game()
    {
        SceneManager.LoadScene("Game");
    }
    public void GameExit()
    {
       Application.Quit();
    }
}
