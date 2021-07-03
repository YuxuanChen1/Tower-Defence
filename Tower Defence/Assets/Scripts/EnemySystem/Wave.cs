using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˲��ο���
/// </summary>
/// �������ض�ʱ�����ɵ���
public class Wave
{
    #region ����
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
    

    public List<Path> paths { get; set; }   //��ǰ������·��
    public List<EnemyWaveDatabase> enemyWaveDatabases { get; set; }    //��ǰ�������е�����Ϣ

    public bool allOver { get; set; }   //ȫ������������
    public List<GameObject> existEnemies = new List<GameObject>();  //��ǰ�����е���
    private bool waveOver;  //��ǰ������������
    private float time;         //��һ����������ʱ��(����ʱ)
    public int waveNum { get; set; }    //���α��
    private int enemyNum; //��ǰ���ε��˱�� 
    private int pathIndex;    //��ǰ������ʹ�õ�·��

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
            Debug.LogError("��·����Ϣ");
            return;
        }

        if(enemyWaveDatabases == null || enemyWaveDatabases.Count == 0)
        {
            Debug.LogError("�޵�����Ϣ");
            return;
        }

        if (allOver)    //���ȫ�����˶�������
            return;

        if (waveOver)   //�����ǰ���ε�����ȫ������
        {
            if (existEnemies.Count == 0)    //�����ǰ���ε���ȫ����ʧ��������һ��
            {
                enemyNum = 0;
                waveNum++;
                pathIndex = Random.Range(0,paths.Count);    //ָ���ò����˵�·��
                waveOver = false;

                if (waveNum >= enemyWaveDatabases.Count)  //���ȫ�����ζ�ȫ������
                {
                    allOver = true;
                    return;
                }
            }
            return;
        }

        time -= Time.deltaTime;

        if (time <= 0f) //�����������һ������
        {
            CreatEnemy(pathIndex);
            enemyNum++;

            if (enemyNum >= enemyWaveDatabases[waveNum].Enemies.Count) //�����ǰ���ε���ȫ������
            {
                waveOver = true;
            }

            //ˢ�µ�������ʱ��
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
