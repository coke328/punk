using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class footBoard : MonoBehaviour
{
    public UnityEvent press;
    public UnityEvent pressDown;
    public UnityEvent pressOut;
    private bool lastcol = false;
    private BoxCollider2D col;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(col.IsTouchingLayers(LayerMask.GetMask("moveObject"))){
            press.Invoke();
            if(!lastcol){
                pressDown.Invoke();
                transform.localScale = new Vector3(1f,0.02f,1f);
            }
            lastcol = true;
        }else{
            if(lastcol){
                pressOut.Invoke();
                transform.localScale = new Vector3(1f,0.1f,1f);
            }
            lastcol = false;
        }
    }
}
