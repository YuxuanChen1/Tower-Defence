using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ToChooseMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("ChooseScene");
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
