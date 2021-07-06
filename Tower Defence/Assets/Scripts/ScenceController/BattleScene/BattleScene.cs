using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// BattleScene�ű�
/// </summary>
/// _enemyWaveDatabase ��_paths ����Ҫ�ֶ���ֵ
public class BattleScene : MonoBehaviour
{
    
    [Tooltip("�ؿ����")]
    [SerializeField] private int levelNum;
    private float time; //��Ϸʱ��
    [Header("�ؿ�Ѫ��")]
    private int gameBlood;
    [Header("���˲�����Ϣ")]
    [SerializeField] public List<EnemyWaveDatabase> _enemyWaveDatabases;
    [Header("����·����Ϣ")]
    [SerializeField] private List<Path> _paths;
    [Header("��ͣ����")]
    [SerializeField] private GameObject pausePlane;
    [Header("��Ϸ��������")]
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
                Debug.LogError("�Ҳ�����Ϸ������");
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
