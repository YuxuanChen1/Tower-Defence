using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected Animator anim;
    [SerializeField] protected LayoutArea layoutArea;
    public virtual void Initialize(LayoutArea area) 
    {
        layoutArea = area;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
}
