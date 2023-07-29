using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class press_zone : MonoBehaviour
{
    public UnityEvent press_event;
    private BoxCollider2D col;
    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            press_zone_disable();
            Debug.Log("프레스 감지");
            press_event.Invoke();
        }
    }
    public void press_zone_enable()
    {
        col.enabled = true;
    }
    public void press_zone_disable()
    {
        col.enabled = false;
    }
}
