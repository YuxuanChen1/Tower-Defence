using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// BattleScene脚本
/// </summary>
/// _enemyWaveDatabase 和_paths 都需要手动赋值
public class BattleScene : MonoBehaviour
{
    
    [Tooltip("关卡序号")]
    [SerializeField] private int levelNum;
    private float time; //游戏时间
    [Header("关卡血量")]
    private int gameBlood;
    [Header("敌人波次信息")]
    [SerializeField] public List<EnemyWaveDatabase> _enemyWaveDatabases;
    [Header("本关路径信息")]
    [SerializeField] private List<Path> _paths;
    [Header("暂停弹窗")]
    [SerializeField] private GameObject pausePlane;
    [Header("游戏主控制器")]
    [SerializeField] private GameController gameController;

    private bool loseTheGame = false;
    private float loseTime = 0f;
    private bool winTheGame = false;
    private float winTime = 0f;
    private void Start()
    {
        Wave.Instance.Initialize(this);
        Wave.Instance.paths = _paths;
        Wave.Instance.enemyWaveDatabases = _enemyWaveDatabases;

        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            if(gameController == null)
            {
                Debug.LogError("找不到游戏控制器");
                return;
            }

            gameController.SetSceneIndex(SceneManager.GetActiveScene().buildIndex);

            switch (gameController.difficulity)
            {
                case Difficulity.easy:
                    gameBlood = 5;
                    break;
                
                case Difficulity.hard:
                    gameBlood = 1;
                    break;
                case Difficulity.normal:
                default:
                    gameBlood = 3;
                    break;
            }
        }
    }
    void Update()
    {
        time += Time.deltaTime;
        Wave.Instance.WaveUpdate();

        if (loseTheGame)
        {
            loseTime += Time.deltaTime;
            if (loseTime > 2)
            {
                Time.timeScale = 1f;
                SceneManager.LoadSceneAsync("LoseScene");
            }
                
        }

        if (winTheGame)
        {
            winTime += Time.deltaTime;
            if (winTime > 2)
            {
                Time.timeScale = 1f;
                SceneManager.LoadSceneAsync("WinScene");
            }        }

        if(Wave.Instance.waveNum == Wave.Instance.enemyWaveDatabases.Count && Wave.Instance.existEnemies.Count == 0)
            winTheGame = true;
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
        pausePlane.SetActive(true);
    }

    public float GetTime()
    {
        return time;
    }

    public int GetLevelNum()
    {
        return levelNum;
    }

    public void EnemyArrive()
    {
        gameBlood--;
        if(gameBlood == 0)
        {
            loseTheGame = true;
        }
    }
}
