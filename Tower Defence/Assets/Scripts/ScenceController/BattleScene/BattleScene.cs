using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BattleScene�ű�
/// </summary>
/// _enemyWaveDatabase ��_paths ����Ҫ�ֶ���ֵ
public class BattleScene : MonoBehaviour
{
    [Tooltip("�ؿ����")]
    [SerializeField] private int levelNum;
    private float time; //��Ϸʱ��
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
