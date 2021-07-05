using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制敌人移动逻辑
/// </summary>
/// 由Wave单例给path赋值
public class EnemyController : MonoBehaviour
{
    [Header("基本属性")]
    [SerializeField] private int maxBlood;
    [SerializeField] private int blood;
    [SerializeField] private int damage;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float speed;
    [SerializeField] private int gold;
    [Header("组件")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Slider bloodBar;
    [Space]
    [Tooltip("移动路径")]
    public Path path;
    [Header("攻击相关")]
    [SerializeField] private float rayLength;
    [Tooltip("攻击范围内的单位")]
    [SerializeField] private List<GameObject> soldiersInRange;

    [Header("调试")]
    [Tooltip("当前目标节点")]
    [SerializeField] private int index = 1;
    [SerializeField] private BattleScene battleScene;
    private bool hasDeath = false;

    public void E_Initialize(Path _path, BattleScene _battleScene)
    {
        //使得敌人不同步
        float random = UnityEngine.Random.Range(-0.2f, 0.2f);
        rayLength += random;

        blood = maxBlood;
        speed = defaultSpeed;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        path = _path;

        battleScene = _battleScene;
    }

    private void Update()
    {
        if (bloodBar == null)
            Debug.LogError("无血条");
        bloodBar.value = (float)blood / (float)maxBlood;

        if(anim == null)
        {
            Debug.LogWarning("一个敌人没有动画");
            return;
        }

        AnimSwitch();
        

    }

    #region 移动
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (blood == 0)
            speed = 0;

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
    #endregion

    private void AnimSwitch()
    {
        //射线检测前面有无单位（只用于动画切换）
        Vector2 rayDir = new Vector2(transform.localScale.x, 0).normalized;
        bool soldierBefore = Physics2D.Raycast(transform.position, rayDir, rayLength, 1 << LayerMask.NameToLayer("Soldier"));
        Debug.DrawRay(transform.position, rayDir * rayLength, Color.blue);
        if (soldierBefore)
        {
            anim.SetBool("attack", true);
            speed = 0f;
        }
        else
        {
            anim.SetBool("attack", false);
            speed = defaultSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("一个单位进入范围");
        if (collision.CompareTag("Soldier"))
        {
            Debug.Log("一个单位加入列表");
            soldiersInRange.Add(collision.gameObject);
        }

    }

    //攻击，由帧动画中事件调用
    public void Attack()
    {
        for(int i = 0; i < soldiersInRange.Count;i++)
        {
            if (soldiersInRange[i].GetComponent<CloseSoldier>().GetHurt(damage))
                soldiersInRange.Remove(soldiersInRange[i]);
        }
    }

    public bool GetHurt(int damage)
    {
        bool isDeath = false;
        if(blood <= damage)
        {
            anim.SetBool("death", true);
            
            blood = 0;
            if (!hasDeath)
            {
                battleScene.AddGold(gold);
                hasDeath = true;
            }
            
            Death();
            return !isDeath;
        }
        blood -= damage;
        return isDeath;
    }

    public void Death()
    {
        Debug.Log("一个敌人死亡");
        //speed = 0f;
        Wave.Instance.existEnemies.Remove(this.gameObject);
        //协程 消失
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        while (sprite.color.a >= 0)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        Destroy(this.gameObject);
    }

}
