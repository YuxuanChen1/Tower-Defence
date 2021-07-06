using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : LoseScene
{
    [SerializeField] private int lastBattleSceneIndex;
    public void NextGame()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        if(gameController == null)
        {
            Debug.LogError("û���ҵ���Ϸ������");
            return;
        }
        lastBattleSceneIndex = gameController.GetComponent<GameController>().GetSceneIndex();
        Debug.Log("1");
        SceneManager.LoadSceneAsync(lastBattleSceneIndex + 1);
    }
}
