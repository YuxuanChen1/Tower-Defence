using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BattleScene脚本
/// </summary>
/// _enemyWaveDatabase 和_paths 都需要手动赋值
public class BattleScene : MonoBehaviour
{
    [Tooltip("关卡序号")]
    [SerializeField] private int levelNum;
    private float time; //游戏时间
    [SerializeField] private int gold;
    [SerializeField] public List<EnemyWaveDatabase> _enemyWaveDatabases;
    [SerializeField] private List<Path> _paths;
    [SerializeField] private GameObject pausePlane;
    private void Start()
    {
        Wave.Instance.Initialize(this);
        Wave.Instance.paths = _paths;
        Wave.Instance.enemyWaveDatabases = _enemyWaveDatabases;
    }
    void Update()
    {
        time += Time.deltaTime;
        Wave.Instance.WaveUpdate();
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

    public int GetGold()
    {
        return gold;
    }

    public void AddGold(int _gold)
    {
        gold += _gold;
    }

    public int GetLevelNum()
    {
        return levelNum;
    }
}
