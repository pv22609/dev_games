using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_Menu : MonoBehaviour
{
    public void LoadRetryGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
