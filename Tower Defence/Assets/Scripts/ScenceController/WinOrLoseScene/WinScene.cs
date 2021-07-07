using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : LoseScene
{
    [SerializeField] private int lastBattleSceneIndex;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GetButton();
    }
    public void NextGame()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        if(gameController == null)
        {
            Debug.LogError("没有找到游戏控制器");
            return;
        }
        lastBattleSceneIndex = gameController.GetComponent<GameController>().GetSceneIndex();
        Debug.Log("1");
        SceneManager.LoadSceneAsync(lastBattleSceneIndex + 1);
    }
}
