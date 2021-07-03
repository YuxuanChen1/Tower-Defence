using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] public List<Transform> nodes;

    void Update()
    {
        for (int i = 0; i < nodes.Count - 1; i++)
            Debug.DrawLine(nodes[i].position, nodes[i + 1].position, Color.red);
    }
}
