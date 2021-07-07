using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseScene : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetButton();
    }
    public void OnPlayGame(int index)
    {
        SceneManager.LoadSceneAsync("BattleScene_" + index);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
