using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : MonoBehaviour 
{
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
