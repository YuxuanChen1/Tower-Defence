using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���Ƶ����ƶ��߼�
/// </summary>
/// ��Wave������path��ֵ
public class EnemyController : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private int maxBlood;
    [SerializeField] private int blood;
    [SerializeField] private int damage;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float speed;
    [SerializeField] private int gold;
    [Header("���")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Slider bloodBar;
    [Space]
    [Tooltip("�ƶ�·��")]
    public Path path;
    [Header("�������")]
    [SerializeField] private float rayLength;
    [Tooltip("������Χ�ڵĵ�λ")]
    [SerializeField] private List<GameObject> soldiersInRange;

    [Header("����")]
    [Tooltip("��ǰĿ��ڵ�")]
    [SerializeField] private int index = 1;
    [SerializeField] private BattleScene battleScene;
    private bool hasDeath = false;

    public void E_Initialize(Path _path, BattleScene _battleScene)
    {
        //ʹ�õ��˲�ͬ��
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
            Debug.LogError("��Ѫ��");
        bloodBar.value = (float)blood / (float)maxBlood;

        if(anim == null)
        {
            Debug.LogWarning("һ������û�ж���");
            return;
        }

        AnimSwitch();
        

    }

    #region �ƶ�
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (blood == 0)
            speed = 0;

        //�ж��Ƿ���·��
        if(path == null || path.nodes.Count <= 1)
        {
            Debug.LogWarning("һ�������Ҳ���·����");
            return;
        }

        //�жϵ���ʱ�񵽴���һ�ڵ�
        float _distance = Vector3.Distance(transform.position, path.nodes[index].position);
        if(_distance <= 0.1f)
        {
            //�жϵ����Ƿ񵽴��յ�
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
        //�õ�����Wave�ű��г���
        Wave.Instance.existEnemies.Remove(this.gameObject);
        //���Ѫ������

        //���ٵ���
        Destroy(this.gameObject);
    }
    #endregion

    private void AnimSwitch()
    {
        //���߼��ǰ�����޵�λ��ֻ���ڶ����л���
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
        Debug.Log("һ����λ���뷶Χ");
        if (collision.CompareTag("Soldier"))
        {
            Debug.Log("һ����λ�����б�");
            soldiersInRange.Add(collision.gameObject);
        }

    }

    //��������֡�������¼�����
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
        Debug.Log("һ����������");
        //speed = 0f;
        Wave.Instance.existEnemies.Remove(this.gameObject);
        //Э�� ��ʧ
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
