using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Push_Object : MonoBehaviour
{
    public float ObjectDrag;
    private Rigidbody2D rb;
    private bool lastcol = false;
    private BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (col.IsTouchingLayers(LayerMask.GetMask("ground")))
        {
            if (!lastcol)
            {
                rb.drag = ObjectDrag;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            }
            lastcol = true;
        }
        else
        {
            if (lastcol)
            {
                rb.drag = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation & RigidbodyConstraints2D.FreezePositionX;
            }
            lastcol = false;
        }
    }
}