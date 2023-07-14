using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    public Transform desPos1;
    public Transform desPos2;
    public Transform desPos3;
    public Transform desPos4;
    public Transform chasingpos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        chasingpos = desPos2;
        InvokeRepeating("check_pos", 0.1f, 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, chasingpos.position, Time.deltaTime*speed);   
    }
    void check_pos()
    {
        if (transform.position == desPos1.position)
            chasingpos = desPos2;
        if (transform.position == desPos2.position)
            chasingpos = desPos3;
        if (transform.position == desPos3.position)
            chasingpos = desPos4;
        if (transform.position == desPos4.position)
            chasingpos = desPos1;
    }
}
