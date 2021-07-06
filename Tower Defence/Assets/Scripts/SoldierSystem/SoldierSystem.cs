using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSystem : MonoBehaviour
{
    [SerializeField] private int gold;
    [SerializeField] private bool layoutState;
    [SerializeField] private bool deleteState;
    [SerializeField] private SoldierInfoDatabase soldierDatabase;

    public void SetState(bool _layoutState, bool _deleteState)
    {
        CheckState( _layoutState,_deleteState);
        layoutState = _layoutState;
        deleteState = _deleteState;
    }
    public bool GetLayoutState()
    {
        return layoutState;
    }
    public bool GetDeleteState()
    {
        return deleteState;
    }

    public void SetSoldierDatabase(SoldierInfoDatabase _soldierInfoDatabase)
    {
        soldierDatabase = _soldierInfoDatabase;
    }

    public SoldierInfoDatabase GetSoliderDatabase()
    {
        return soldierDatabase;
    }

    private void Update()
    {
        if (layoutState == false && deleteState == false)
            return;
        
        if (layoutState)
            LayoutState();
        else if (deleteState)
            DeleteState();
    }

    private void LayoutState()
    {

    }

    private void DeleteState()
    {

    }

    private void CheckState(bool layout,bool delete)
    {
        if (layout && delete)
        {
            Debug.LogError("兵营系统：状态设置错误，两种状态不可同时使用");
            return;
        }
    }

    public void AddGold(int _gold)
    {
        gold += _gold;
    }

    public int GetGold()
    {
        return gold;
    }

    public void CostGold(int cost)
    {
        gold -= cost;
        if (gold < 0)
        {
            Debug.LogError("金币花费错误");
        }
    }
}
