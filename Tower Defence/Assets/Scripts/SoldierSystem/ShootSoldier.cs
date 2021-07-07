using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootSoldier : SoldierController
{
    [SerializeField] float attackRange;
    [SerializeField] float minAttackRange_X;

    [SerializeField] GameObject target;
    [Header("��Ч")]
    
    [SerializeField] private AudioSource bowPull;
    [SerializeField] private AudioSource bowFire;


    private void Update()
    {
        Debug.DrawRay(transform.position, new Vector2(-1, 0) * attackRange, Color.red);
        Debug.DrawRay(transform.position, new Vector2(-1, 0) * minAttackRange_X, Color.yellow);

        if (target == null)
        {
            FindTarget();
        }
        else
        {
            if(transform.position.x - target.transform.position.x < minAttackRange_X || Vector2.Distance(transform.position, target.transform.position) > attackRange)
            {
                //Debug.Log("һ�����߳�������Χ");
                anim.SetBool("attack", false);
                target = null;
            }
            else
            {
                anim.SetBool("attack", true);
            }
        }

        SoundEffect();

    }

    private void FindTarget()
    {
        //Debug.Log("Ѱ�ҵ���");

        if (Wave.Instance.existEnemies == null || Wave.Instance.existEnemies.Count == 0)
            return;

        float minDistance = 10000f;
        GameObject _target = null;

        foreach(GameObject enemy in Wave.Instance.existEnemies)
        {
            float dis = Vector2.Distance(transform.position, enemy.transform.position);
            if (dis < attackRange)
            {
                if(dis < minDistance && transform.position.x - enemy.transform.position.x >= minAttackRange_X)
                {
                    minDistance = dis;
                    _target = enemy;
                }
            }
        }

        target = _target;
    }

    //֡��������
    public void Attack()
    {
        if (target == null)
        {
            anim.SetBool("attack", false);
            return;
        }

        if (target.GetComponent<EnemyController>().GetHurt(damage))
        {
            target = null;
            anim.SetBool("attack", false);
        }
    }

    protected override void SoundEffect()
    {
        bowFire.volume = musicController.masterVolume * musicController.effectVolume;
        bowPull.volume = musicController.masterVolume * musicController.effectVolume;
    }
    public void BowPullEffect()
    {
        bowPull.Play();
    }
    public void BowFireEffect()
    {
        bowFire.Play();
    }
}


