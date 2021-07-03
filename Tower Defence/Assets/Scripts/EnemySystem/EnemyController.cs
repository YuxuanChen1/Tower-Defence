using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制敌人移动逻辑
/// </summary>
/// 由Wave单例给path赋值
public class EnemyController : MonoBehaviour
{
    [Header("基本属性")]
    [SerializeField] private int blood;
    [SerializeField] private float speed;
    [SerializeField] private float gold;
    [Space]
    [Tooltip("移动路径")]
    public Path path;

    [Header("调试")]
    [Tooltip("当前目标节点")]
    [SerializeField] private int index = 1;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //判断是否有路径
        if(path == null || path.nodes.Count <= 1)
        {
            Debug.LogWarning("一个敌人找不到路径。");
            return;
        }

        //判断敌人时否到达下一节点
        float _distance = Vector3.Distance(transform.position, path.nodes[index].position);
        if(_distance <= 0.1f)
        {
            //判断敌人是否到达终点
            if (index >= path.nodes.Count - 1)
            {
                Arrive();
                return;
            }
                
            index++;
        }

        Vector3 dir = (path.nodes[index].position - transform.position).normalized;
        transform.Translate(dir * speed * Time.fixedDeltaTime);

    }

    private void Arrive()
    {
        //该敌人在Wave脚本中出队
        Wave.Instance.existEnemies.Remove(this.gameObject);
        //玩家血量减少

        //销毁敌人
        Destroy(this.gameObject);
    }
}
