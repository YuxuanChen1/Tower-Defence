using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New EnemyWaveDatabase",menuName ="Database/Enemy/New EnemyWaveDatabase")]
[Serializable]

public class EnemyWaveDatabase : ScriptableObject
{
    public List<GameObject> Enemies;
    public float showTime;
}
