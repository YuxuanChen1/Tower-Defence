using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : MonoBehaviour 
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetButton();
    }
    public void OnStartGame()
    {
        SceneManager.LoadSceneAsync("ChooseScene");
    }

    public void OnExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Setting()
    {
        SceneManager.LoadSceneAsync("SettingScene");
    }
}
