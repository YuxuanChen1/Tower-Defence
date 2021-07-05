using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BattleScene�ű�
/// </summary>
/// _enemyWaveDatabase ��_paths ����Ҫ�ֶ���ֵ
public class BattleScene : MonoBehaviour
{
    public float time;  //��Ϸʱ��
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
