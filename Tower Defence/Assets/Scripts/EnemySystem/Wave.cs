using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人波次控制
/// </summary>
/// 负责在特定时间生成敌人
public class Wave
{
    #region 单例
    private static Wave _instance;
    public static Wave Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Wave();
            return _instance;
        }
    }
    #endregion
    

    public List<Path> paths { get; set; }   //当前场景的路径
    public List<EnemyWaveDatabase> enemyWaveDatabases { get; set; }    //当前场景所有敌人信息

    public bool allOver { get; set; }   //全部敌人已生成
    public List<GameObject> existEnemies = new List<GameObject>();  //当前场景中敌人
    private bool waveOver;  //当前波敌人以生成
    private float time;         //下一个敌人生成时间(倒计时)
    public int waveNum { get; set; }    //波次编号
    private int enemyNum; //当前波次敌人编号 
    private int pathIndex;    //当前波次所使用的路径

    public void Initialize()
    {
        allOver = false;
        waveOver = false;
        enemyNum = 0;
        waveNum = 0;
        existEnemies.Clear();
        this.paths = null;
        this.enemyWaveDatabases = null;
    }

    public void WaveUpdate()
    {
        if(paths == null)
        {
            Debug.LogError("无路径信息");
            return;
        }

        if(enemyWaveDatabases == null || enemyWaveDatabases.Count == 0)
        {
            Debug.LogError("无敌人信息");
            return;
        }

        if (allOver)    //如果全部敌人都已生成
            return;

        if (waveOver)   //如果当前波次敌人已全部生成
        {
            if (existEnemies.Count == 0)    //如果当前波次敌人全部消失，进入下一波
            {
                enemyNum = 0;
                waveNum++;
                pathIndex = Random.Range(0,paths.Count);    //指定该波敌人的路径
                waveOver = false;

                if (waveNum >= enemyWaveDatabases.Count)  //如果全部波次都全部生成
                {
                    allOver = true;
                    return;
                }
            }
            return;
        }

        time -= Time.deltaTime;

        if (time <= 0f) //如果可生成下一个敌人
        {
            CreatEnemy(pathIndex);
            enemyNum++;

            if (enemyNum >= enemyWaveDatabases[waveNum].Enemies.Count) //如果当前波次敌人全部生成
            {
                waveOver = true;
            }

            //刷新敌人生成时间
            time = enemyWaveDatabases[waveNum].showTime;
        }
        
    }

    private void CreatEnemy(int _pathIndex)
    {
        Vector3 _position = paths[pathIndex].nodes[0].position;
        GameObject _enemy = GameObject.Instantiate(enemyWaveDatabases[waveNum].Enemies[enemyNum], _position, Quaternion.identity);
        _enemy.GetComponent<EnemyController>().path = paths[_pathIndex];
        existEnemies.Add(_enemy);
    }
}
