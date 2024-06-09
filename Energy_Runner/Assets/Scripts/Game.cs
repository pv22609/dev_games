using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // M�todo para pausar o jogo
    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Ativa o menu de pausa
        Time.timeScale = 0f; // Pausa o tempo do jogo
        isPause = true; // Atualiza o estado para pausado
    }

    // M�todo para retomar o jogo
    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Desativa o menu de pausa
        Time.timeScale = 1f; // Retoma o tempo do jogo
        isPause = false; // Atualiza o estado para n�o pausado
    }

    // M�todo para carregar a cena "MainMenu"
    public void LoadMainMennu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
