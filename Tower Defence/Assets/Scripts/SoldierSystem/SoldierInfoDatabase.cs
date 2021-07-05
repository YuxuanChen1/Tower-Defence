using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New SoldierInformation",menuName = "Database/Soldier/New SoldierInformation")]
public class SoldierInfoDatabase : ScriptableObject
{
    public string soldierName;
    public Sprite sprite;
    public Sprite background;
    public GameObject soldier;
    public int gold;
}
