using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaseSector : MonoBehaviour
{
    public bool playerIn;
    private BoxCollider2D col;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    void Update(){
        playerIn = col.IsTouchingLayers(LayerMask.GetMask("player"));
    }
}
