using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScence : MonoBehaviour
{
    public float time;  //”Œœ∑ ±º‰
    [SerializeField] private List<EnemyWaveDatabase> _enemyWaveDatabases;
    [SerializeField] private List<Path> _paths;
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
}
