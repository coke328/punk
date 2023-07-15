using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(EdgeCollider2D))]

public class beam_line : MonoBehaviour
{
    LineRenderer LR;
    EdgeCollider2D EdgeCollider;
    void Awake()
    {
        LR = GetComponent<LineRenderer>();
        EdgeCollider = GetComponent<EdgeCollider2D>();
        //lineRenderer basic setting
        LR.positionCount = 2;
        LR.enabled = false;
        LR.startWidth = 1f;
        LR.endWidth = 1f;
        //boxCollider basic setting
        EdgeCollider.enabled = false;
    }
    public void Play(Vector2 from, Vector2 to)
    {
        //LineRenderer set Vector
        LR.enabled = true;
        LR.SetPosition(0, from);
        LR.SetPosition(1, to);
        //EdgeCollider set Vector
        SetColliderFromLine();
    }
    public void Stop()
    {
        LR.enabled = false;
        EdgeCollider.enabled = false;
    }
    private void SetColliderFromLine()
    {
        EdgeCollider.enabled = true;
        Vector2[] points = new Vector2[LR.positionCount];
        for (int i = 0; i < LR.positionCount; i++)
        {
            points[i] = LR.GetPosition(i);
        }
        EdgeCollider.points = points;
    }
}