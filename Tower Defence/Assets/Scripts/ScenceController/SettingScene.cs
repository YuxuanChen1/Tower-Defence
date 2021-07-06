using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingScene : MonoBehaviour
{
    [SerializeField] private Slider mainVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider effectVolume;

    [SerializeField] private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if(gameController == null)
        {
            Debug.LogError("找不到游戏控制器");
            return;
        }

        {
            mainVolume.value = gameController.masterVolume;
            musicVolume.value = gameController.musicVolume;
            effectVolume.value = gameController.effectVolume;
        }
    }

    private void Update()
    {
        gameController.masterVolume = mainVolume.value;
        gameController.musicVolume = musicVolume.value;
        gameController.effectVolume = effectVolume.value;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void EasyMode()
    {
        gameController.difficulity = Difficulity.easy;
    }
    public void NormalMode()
    {
        gameController.difficulity = Difficulity.normal;
    }
    public void HardMode()
    {
        gameController.difficulity = Difficulity.hard;
    }
}
