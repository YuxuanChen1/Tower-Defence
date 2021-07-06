using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Slider mainVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider effectVolume;

    [SerializeField] private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gameController == null)
        {
            Debug.LogError("找不到游戏控制器");
            return;
        }
        {
            mainVolume.value = gameController.masterVolume;
            musicVolume.value = gameController.musicVolume;
            effectVolume.value = gameController.effectVolume;
        }


        gameObject.SetActive(false);
    }

    private void Update()
    {
        gameController.masterVolume = mainVolume.value;
        gameController.musicVolume = musicVolume.value;
        gameController.effectVolume = effectVolume.value;
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
