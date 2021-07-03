using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Ƶ����ƶ��߼�
/// </summary>
/// ��Wave������path��ֵ
public class EnemyController : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private int blood;
    [SerializeField] private float speed;
    [SerializeField] private float gold;
    [Space]
    [Tooltip("�ƶ�·��")]
    public Path path;

    [Header("����")]
    [Tooltip("��ǰĿ��ڵ�")]
    [SerializeField] private int index = 1;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
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
}
