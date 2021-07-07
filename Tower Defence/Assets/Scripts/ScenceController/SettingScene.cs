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
    [SerializeField] private MusicController musicController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if(gameController == null)
        {
            Debug.LogError("找不到游戏控制器");
            return;
        }
        gameController.GetButton();

        musicController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MusicController>();
        {
            mainVolume.value = musicController.masterVolume;
            musicVolume.value = musicController.musicVolume;
            effectVolume.value = musicController.effectVolume;
        }
    }

    private void Update()
    {
        musicController.masterVolume = mainVolume.value;
        musicController.musicVolume = musicVolume.value;
        musicController.effectVolume = effectVolume.value;
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
