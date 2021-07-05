using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierArea : MonoBehaviour
{
    [SerializeField] private SoldierInfoDatabase soldierDatabase;
    [SerializeField] private SoldierSystem _soldierSystem;

    private void Start()
    {
        _soldierSystem = GameObject.FindGameObjectWithTag("SoldierSystem").GetComponent<SoldierSystem>();
        if(_soldierSystem == null)
        {
            Debug.LogError("没有找到兵营系统");
            return;
        }

        transform.GetChild(0).GetComponent<Image>().sprite = soldierDatabase.sprite;
    }

    public void ToLayoutState()
    {
        if (_soldierSystem.GetDeleteState())
            return;

        _soldierSystem.SetState(true, false);
        _soldierSystem.SetSoldierDatabase(soldierDatabase);
    }
}
