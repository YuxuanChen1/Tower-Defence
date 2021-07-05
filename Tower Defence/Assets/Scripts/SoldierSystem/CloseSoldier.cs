using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseSoldier : SoldierController
{
    [SerializeField] private int maxBlood;
    [SerializeField] private int blood;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Slider bloodBar;
    [Header("攻击相关")]
    [SerializeField] private float rayLength;
    [SerializeField] private List<GameObject> enemiesInRange;

    public override void Initialize(LayoutArea area)
    {
        blood = maxBlood;
        layoutArea = area;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (bloodBar == null)
            Debug.LogError("无血条");
        bloodBar.value = (float)blood / (float)maxBlood;

        AnimSwitch();
    }

    private void AnimSwitch()
    {
        //射线检测前面有无单位（只用于动画切换）
        Vector2 rayDir = new Vector2(transform.localScale.x, 0).normalized;
        bool soldierBefore = Physics2D.Raycast(transform.position, rayDir, rayLength, 1 << LayerMask.NameToLayer("Enemy"));
        Debug.DrawRay(transform.position, rayDir * rayLength, Color.blue);
        if (soldierBefore)
        {
            anim.SetBool("attack", true);
        }
        else
        {
            anim.SetBool("attack", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("一个单位进入范围");
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("一个单位加入列表");
            enemiesInRange.Add(collision.gameObject);
        }

    }

    //攻击，由帧动画中事件调用
    public void Attack()
    {
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (enemiesInRange[i].GetComponent<EnemyController>().GetHurt(damage))
                enemiesInRange.Remove(enemiesInRange[i]);
        }
    }

    public bool GetHurt(int damage)
    {
        Debug.Log("一个单位受到伤害");
        bool isDeath = false;
        if (blood <= damage)
        {
            anim.SetBool("attack", false);

            blood = 0;
            Death();
            return !isDeath;
        }
        blood -= damage;
        return isDeath;
    }
    private void Death()
    {
        Debug.Log("一个单位死亡");
        layoutArea.ResetArea();
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
