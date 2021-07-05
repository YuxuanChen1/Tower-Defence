using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BattleScene脚本
/// </summary>
/// _enemyWaveDatabase 和_paths 都需要手动赋值
public class BattleScene : MonoBehaviour
{
    public float time;  //游戏时间
    [SerializeField] private List<EnemyWaveDatabase> _enemyWaveDatabases;
    [SerializeField] private List<Path> _paths;
    [SerializeField] private GameObject pausePlane;
    private void Start()
    {
        Wave.Instance.Initialize();
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
}
