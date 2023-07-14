using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(EdgeCollider2D))]

public class beam_line : MonoBehaviour
{
    LineRenderer LineRenderer;
    EdgeCollider2D EdgeCollider;
    void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
        EdgeCollider = GetComponent<EdgeCollider2D>();
        //lineRenderer basic setting
        LineRenderer.positionCount = 2;
        LineRenderer.enabled = false;
        LineRenderer.startWidth = 1f;
        LineRenderer.endWidth = 1f;
        //boxCollider basic setting
        EdgeCollider.enabled = false;
    }
    public void Play(Vector2 from, Vector2 to)
    {
        //LineRenderer set Vector
        LineRenderer.enabled = true;
        LineRenderer.SetPosition(0, from);
        LineRenderer.SetPosition(1, to);
        //EdgeCollider set Vector
        SetColliderFromLine();
    }
    public void Stop()
    {
        LineRenderer.enabled = false;
        EdgeCollider.enabled = false;
    }
    private void SetColliderFromLine()
    {
        EdgeCollider.enabled = true;
        Vector2[] points = new Vector2[LineRenderer.positionCount];
        for (int i = 0; i < LineRenderer.positionCount; i++)
        {
            points[i] = LineRenderer.GetPosition(i);
        }
        EdgeCollider.points = points;
    }
}