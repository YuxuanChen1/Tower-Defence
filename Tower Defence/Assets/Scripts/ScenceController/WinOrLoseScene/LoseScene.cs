using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScene : MonoBehaviour
{
    public void ToChooseScene()
    {
        SceneManager.LoadSceneAsync("ChooseScene");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Replay()
    {
        int index = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetSceneIndex();
        SceneManager.LoadSceneAsync(index);
    }
}
