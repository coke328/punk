using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_platform : MonoBehaviour
{
    public float fall_wait_time;
    public float recover_wait_time;
    private bool flag;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        flag = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && flag)
        {
            flag = false;
            sr.color = Color.gray;
            Debug.Log("감지");
            Invoke("falling", fall_wait_time);
            Invoke("recover", (recover_wait_time + fall_wait_time));
            flag = true;
        }
    }
    void falling()
    {
        Debug.Log("파괴");
        bc.enabled = false;
        sr.color = Color.black;
    }
    void recover()
    {
        Debug.Log("회복");
        bc.enabled = true;
        sr.color = Color.white;
    }
}
