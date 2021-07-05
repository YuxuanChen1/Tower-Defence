using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BattleScene : MonoBehaviour
{
    private BattleScene battleScene;
    [Header("���")]
    [SerializeField] private Text goldText;
    [Header("�ؿ���Ϣ")]
    [SerializeField] private Text levelNum;
    [SerializeField] private Text waveNum;
    [SerializeField] private Text totalWaveNum;
    [SerializeField] private Text gameTime;

    private void Start()
    {
        battleScene = GetComponent<BattleScene>();
        levelNum.text = battleScene.GetLevelNum().ToString();
        totalWaveNum.text = battleScene._enemyWaveDatabases.Count.ToString();
    }
    private void Update()
    {
        goldText.text = battleScene.GetGold().ToString();
        waveNum.text = (Wave.Instance.waveNum + 1 <= battleScene._enemyWaveDatabases.Count ? Wave.Instance.waveNum + 1 : battleScene._enemyWaveDatabases.Count).ToString();
        gameTime.text = ((int)battleScene.GetTime()).ToString() + "s";
    }
}
