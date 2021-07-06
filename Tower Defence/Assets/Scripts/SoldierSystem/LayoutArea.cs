using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum KindOfSoldier
{
    close,
    shoot,
}

public class LayoutArea : MonoBehaviour
{
    [SerializeField] private KindOfSoldier kindOfSoldier;
    [SerializeField] private SoldierSystem soldierSystem;
    [SerializeField] private SoldierInfoDatabase soldierDatabase;
    [SerializeField] private GameObject soldier;
    [SerializeField] private bool isOccupied;
    [SerializeField] private SpriteRenderer sprite;
    [Header("调试")]
    [SerializeField] private float color_a;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        soldierSystem = GameObject.FindGameObjectWithTag("SoldierSystem").GetComponent<SoldierSystem>();
        if (soldierSystem == null)
        {
            Debug.LogError("没有找到兵营系统");
            return;
        }
    }

    private void Update()
    {
        if (!soldierSystem.GetLayoutState() || isOccupied || kindOfSoldier != soldierSystem.GetSoliderDatabase().kind)  //不在放置状态 或者 放置了角色
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
        }
        else
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        }
        color_a = sprite.color.a;

        if (!soldierSystem.GetLayoutState() || isOccupied || kindOfSoldier != soldierSystem.GetSoliderDatabase().kind)
            return;

        {
            var mouse = Mouse.current;
            if (mouse == null)
                return;

            if (mouse.leftButton.isPressed)
            {
                var onScreenPosition = mouse.position.ReadValue();
                var ray = Camera.main.ScreenPointToRay(onScreenPosition);

                var hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, Mathf.Infinity);
                if (hit.collider != null && hit.collider.CompareTag("LayoutArea") && hit.collider.Equals(GetComponent<Collider2D>())) 
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    //hit.collider.gameObject.transform.position = ray.origin;
                    Layout();
                }
            }
        }


    }

    public void Layout()
    {
        Debug.Log("Layout a soldier");
        soldierDatabase = soldierSystem.GetSoliderDatabase();
        soldier = GameObject.Instantiate(soldierDatabase.soldier, this.transform.position, Quaternion.identity);
        soldier.GetComponent<SoldierController>().Initialize(this);
        isOccupied = true;
        soldierSystem.CostGold(soldierDatabase.cost);
        soldierSystem.SetState(false, false);
    }

    public void ResetArea()
    {
        soldierDatabase = null;
        soldier = null;
        isOccupied = false;
    }
}
