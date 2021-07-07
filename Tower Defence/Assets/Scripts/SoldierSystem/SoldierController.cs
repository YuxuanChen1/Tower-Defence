using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected Animator anim;
    [SerializeField] protected LayoutArea layoutArea;
    [SerializeField] protected MusicController musicController;
    public virtual void Initialize(LayoutArea area) 
    {
        layoutArea = area;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

        musicController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MusicController>();
        if (musicController == null)
        {
            Debug.LogError("û���ҵ����ֿ�����");
        }
    }

    protected virtual void SoundEffect() { }
}
